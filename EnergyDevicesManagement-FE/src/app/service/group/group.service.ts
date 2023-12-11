import { Injectable } from '@angular/core';
import { environment } from 'src/app/enviroments/enviroment';
import {
  ChatManagementClient,
  CreateGroupDto,
  CreateGroupDtoResponse,
  GroupDtoListResponse,
  GroupMessageDto,
  GroupMessageDtoResponse,
  MessageDtoListResponse,
} from 'src/app/api-management/chat-management';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class GroupService {
  private apiClient;
  private apiUrl = environment.chatManagementUrl;

  constructor(private httpClient: HttpClient) {
    this.apiClient = new ChatManagementClient(this.httpClient, this.apiUrl);
  }

  createGroup(
    groupName: string,
    usersId: string[]
  ): Observable<CreateGroupDtoResponse> {
    var groupDto = new CreateGroupDto();
    groupDto.name = groupName;
    groupDto.usersId = usersId;
    return this.apiClient.createGroup(groupDto);
  }

  getGroups(): Observable<GroupDtoListResponse> {
    return this.apiClient.admin();
  }

  getGroupMessages(groupId: string): Observable<MessageDtoListResponse> {
    return this.apiClient.group(groupId);
  }

  sendMessageToGroup(
    groupId: string,
    messageText: string
  ): Observable<GroupMessageDtoResponse> {
    var message = new GroupMessageDto();
    message.groupId = groupId;
    message.messageText = messageText;
    return this.apiClient.groupMessage(message);
  }
}
