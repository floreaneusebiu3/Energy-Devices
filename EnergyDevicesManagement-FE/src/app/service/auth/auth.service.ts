import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  LoginDto,
  RegisterUserDto,
  UserManagementClient,
} from 'src/app/api-management/client-user-management';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';
import { environment } from 'src/app/enviroments/enviroment';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiClient;
  private apiUrl = environment.usersManagementUrl;

  constructor(private httpClient: HttpClient, private router: Router) {
    this.apiClient = new UserManagementClient(this.httpClient, this.apiUrl);
  }

  authenticateUser(loginDto: LoginDto) {
    this.apiClient.login(loginDto).subscribe(
      (response) => {
        sessionStorage.setItem('auth-token', response.value!);
        if (this.getRoleFromToken(response.value!) === 'ADMIN') {
          this.router.navigateByUrl('/admin/users');
        } else if (this.getRoleFromToken(response.value!) === 'CLIENT') {
          this.router.navigateByUrl('/client/devices');
        }
      },
      (_error) => {
        alert('Provided credentials are not valid');
        return '';
      }
    );
  }

  registerUser(registerDto: RegisterUserDto) {
    this.apiClient.register(registerDto).subscribe(
      () => {},
      (_error) => 'Provided informations are not valid'
    );
    this.router.navigateByUrl('/login');
  }

  public getToken(): string {
    return sessionStorage.getItem('auth-token') || '';
  }

  public isAuthenticated(): boolean {
    const token = this.getToken();
    return true;
  }

  getRoleFromToken(token: string = this.getToken()): string {
    const helper = new JwtHelperService();
    const decodedToken = helper.decodeToken(token!);
    return decodedToken[
      'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
    ];
  }

  getIdFromToken(token: string = this.getToken()): string {
    const helper = new JwtHelperService();
    const decodedToken = helper.decodeToken(token!);
    return decodedToken[
      'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'
    ];
  }
}
