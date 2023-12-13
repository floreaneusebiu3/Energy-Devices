import { Component, OnInit } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { GroupDto, MessageDto } from 'src/app/api-management/chat-management';
import { UserDto } from 'src/app/api-management/client-user-management';
import { environment } from 'src/app/enviroments/enviroment';
import { AuthService } from 'src/app/service/auth/auth.service';
import { GroupService } from 'src/app/service/group/group.service';
import { MessageService } from 'src/app/service/message/message.service';
import { UsersService } from 'src/app/service/users/users.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss'],
})
export class ChatComponent implements OnInit {
  private connection!: HubConnection;
  loggedUserRole: string;
  users: UserDto[] = [];
  groups: GroupDto[] = [];
  isUserSelected = true;
  selected: any | undefined;
  messageText: string = '';
  messages: MessageDto[] = [];
  userIsTyping = false;
  typingMessage = '';

  public constructor(
    private readonly userService: UsersService,
    private readonly messageService: MessageService,
    private readonly authService: AuthService,
    private readonly groupService: GroupService
  ) {
    this.loggedUserRole = this.authService.getRoleFromToken();
  }

  ngOnInit(): void {
    this.userService.getUsers().subscribe({
      next: (response) => {
        this.users = response.filter(
          (u) => u.id !== this.authService.getIdFromToken()
        );
      },
    });
    this.groupService.getGroups().subscribe({
      next: (response) => (this.groups = response.value!),
    });
    this.initWebSocket();
    this.connection.start();
  }

  initWebSocket() {
    this.connection = new HubConnectionBuilder()
      .withUrl(environment.chatWebSocketUrl)
      .build();
    this.connection.on(this.authService.getIdFromToken(), (message: string) => {
      if (message === 'newMessage') {
        this.getMessages();
      }

      if (message === 'newSeen') {
        this.getMessages();
      }

      if (
        message.includes('typing in group...') &&
        message.includes(this.selected.id)
      ) {
        this.userIsTyping = true;
        this.typingMessage = `${message.split(' ')[0]} is typing...`;
      } else if (
        message.includes('typing') &&
        message.includes(this.selected.id)
      ) {
        this.userIsTyping = true;
        this.typingMessage = `${this.selected.name} is typing...`;
      } else if (message.includes('stopped')) {
        this.userIsTyping = false;
        this.typingMessage = '';
      }
    });
  }

  updateSelectedUser(i: number): void {
    this.isUserSelected = true;
    this.selected = this.users.at(i);
    this.getUserMessages();
  }

  updateSelectedGroup(i: number): void {
    this.isUserSelected = false;
    this.selected = this.groups.at(i);
    this.getGroupMessages();
  }

  sendMessage() {
    if (this.isUserSelected) {
      this.messageService
        .sendMessageToUser(this.selected?.id!, this.messageText)
        .subscribe({
          next: () => {
            this.getUserMessages();
          },
        });
    } else {
      this.groupService
        .sendMessageToGroup(this.selected?.id!, this.messageText)
        .subscribe({
          next: () => {
            this.getGroupMessages();
          },
        });
    }

    this.messageText = '';
  }

  getMessages(): void {
    if (this.isUserSelected) {
      this.getUserMessages();
    } else {
      this.getGroupMessages();
    }
  }

  getUserMessages() {
    this.messageService.getUserMessages(this.selected?.id!).subscribe({
      next: (response) => {
        this.userIsTyping = false;
        this.messages = response.value !== undefined ? response.value : [];
      },
    });
  }

  getGroupMessages() {
    this.groupService.getGroupMessages(this.selected?.id!).subscribe({
      next: (response) => {
        this.userIsTyping = false;
        this.messages = response.value !== undefined ? response.value : [];
      },
    });
  }

  emitUserTyping() {
    this.messageService.userIsTyping(this.selected.id, this.messageText.length);
  }
}
