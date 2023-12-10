import {
  Component,
  ElementRef,
  OnInit,
  Renderer2,
  ViewChild,
} from '@angular/core';
import { MessageDto } from 'src/app/api-management/chat-management';
import { UserDto } from 'src/app/api-management/client-user-management';
import { MessageService } from 'src/app/service/message/message.service';
import { UsersService } from 'src/app/service/users/users.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss'],
})
export class ChatComponent implements OnInit {
  users: UserDto[] = [];
  selectedUser: UserDto | undefined;
  messageText: string = '';
  messages: MessageDto[] = [];

  public constructor(
    private readonly userService: UsersService,
    private readonly messageService: MessageService,
    private renderer: Renderer2
  ) {}

  ngOnInit(): void {
    this.userService.getUsers().subscribe({
      next: (response) => {
        this.users = response;
      },
    });
  }

  updateSelectedUser(i: number): void {
    this.selectedUser = this.users.at(i);
    this.getUserMessages();
  }

  sendMessage() {
    this.messageService
      .sendMessageToUser(this.selectedUser?.id!, this.messageText)
      .subscribe({
        next: () => this.getUserMessages(),
      });
    this.messageText = '';
  }

  getUserMessages() {
    this.messageService.getUserMessages(this.selectedUser?.id!).subscribe({
      next: (response) => {
        this.messages = response.value !== undefined ? response.value : [];
      },
    });
  }
}
