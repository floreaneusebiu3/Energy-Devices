import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, map, throwError } from 'rxjs';
import {
  DeviceManagementClient,
  UserDeviceDto,
  UserDevicePostDto,
} from 'src/app/api-management/client-devices-management';
import { environment } from 'src/app/enviroments/enviroment';

@Injectable({
  providedIn: 'root',
})
export class UserDeviceService {
  private apiClient;
  private apiUrl = environment.devicesManagementUrl;

  constructor(private httpClient: HttpClient) {
    this.apiClient = new DeviceManagementClient(this.httpClient, this.apiUrl);
  }

  getUserDevices(): Observable<UserDeviceDto[]> {
    return this.apiClient
      .devicesUserDevicesGET()
      .pipe(map((response) => response.value || []));
  }

  insertUserDevice(userDeviceDto: UserDevicePostDto) {
    this.apiClient
      .devicesUserDevicesPOST(userDeviceDto)
      .pipe(
        catchError((error) => {
          console.error('An error occurred:', error);
          // Handle the error here, e.g., show a user-friendly message.
          return throwError(
            'An unexpected error occurred. Please try again later.'
          );
        })
      )
      .subscribe((result) => {
        console.log(result);
      });
  }

  deleteUserDevice(userDeviceDto: UserDeviceDto) {
    this.apiClient
      .devicesUserDevicesDELETE(userDeviceDto.userId!, userDeviceDto.deviceId!)
      .subscribe();
  }
}
