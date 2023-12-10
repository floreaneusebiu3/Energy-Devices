import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  LoginDto,
  UserDto,
  UserManagementClient,
} from '../../api-management/client-user-management';
import { Observable, map } from 'rxjs';
import { environment } from 'src/app/enviroments/enviroment';

@Injectable({
  providedIn: 'root',
})
export class UsersService {
  private apiClient;
  private apiUrl = environment.usersManagementUrl;

  constructor(private httpClient: HttpClient) {
    this.apiClient = new UserManagementClient(this.httpClient, this.apiUrl);
  }

  getUsers(): Observable<UserDto[]> {
    console.log(this.apiUrl);
    return this.apiClient
      .usersGET()
      .pipe(map((response) => response.value || []));
  }

  insertUser(userDto: UserDto): void {
    this.apiClient.usersPOST(userDto).subscribe(
      () => {},
      (_error) => alert('Provided informations are not valid')
    );
  }

  updateUser(id: string, userDto: UserDto): void {
    this.apiClient.usersPUT(id, userDto).subscribe(
      () => {},
      (_error) => alert('Provided informations are not valid')
    );
  }

  deleteUser(id: string) {
    this.apiClient.usersDELETE(id).subscribe(
      () => {},
      (_error) => alert('User can not be deleted')
    );
  }
}
