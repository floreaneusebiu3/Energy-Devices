import { Injectable } from '@angular/core';
import { HttpClient } from '@microsoft/signalr';
import { environment } from 'src/app/enviroments/enviroment';
import { AuthService } from '../auth/auth.service';
import { ChatManagementClient } from 'src/app/api-management/chat-management';

@Injectable({
  providedIn: 'root',
})
export class GroupService {
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
