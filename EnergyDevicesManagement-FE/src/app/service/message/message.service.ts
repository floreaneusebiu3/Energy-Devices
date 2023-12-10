import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  ChatManagementClient,
  MessageDto,
  MessageDtoListResponse,
  UserMessageDto,
  UserMessageDtoResponse,
} from 'src/app/api-management/chat-management';
import { environment } from 'src/app/enviroments/enviroment';
import { AuthService } from '../auth/auth.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class MessageService {
  private apiClient;
  private apiUrl = environment.chatManagementUrl;

  constructor(
    private httpClient: HttpClient,
    private authService: AuthService
  ) {
    this.apiClient = new ChatManagementClient(this.httpClient, this.apiUrl);
  }

  sendMessageToUser(
    destinationUserId: string,
    messageText: string
  ): Observable<UserMessageDtoResponse> {
    var messageDto = new UserMessageDto();
    messageDto.senderUserId = this.authService.getIdFromToken();
    messageDto.destionationUserId = destinationUserId;
    messageDto.messageText = messageText;
    return this.apiClient.userMessage(messageDto);
  }

  getUserMessages(userId: string): Observable<MessageDtoListResponse> {
    return this.apiClient.userGET2(userId);
  }
}
