import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import {
  DeviceDto,
  DeviceManagementClient,
} from 'src/app/api-management/client-devices-management';
import { UserDto } from 'src/app/api-management/client-user-management';
import { AuthService } from '../auth/auth.service';
import { environment } from 'src/app/enviroments/enviroment';

@Injectable({
  providedIn: 'root',
})
export class DevicesService {
  private apiClient;
  private apiUrl = environment.devicesManagementUrl;

  constructor(
    private httpClient: HttpClient,
    private authService: AuthService
  ) {
    this.apiClient = new DeviceManagementClient(this.httpClient, this.apiUrl);
  }

  getDevices(): Observable<DeviceDto[]> {
    return this.apiClient
      .devicesGET()
      .pipe(map((response) => response.value || []));
  }

  insertDevice(deviceDto: DeviceDto) {
    this.apiClient.devicesPOST(deviceDto).subscribe(
      () => {},
      (_error) => alert('Provided informations are not valid')
    );
  }

  updateDevice(id: string, deviceDto: DeviceDto) {
    this.apiClient.devicesPUT(id, deviceDto).subscribe(
      () => {},
      (_error) => alert('Provided informations are not valid')
    );
  }

  deleteDevice(id: string) {
    this.apiClient.devicesDELETE(id).subscribe(
      () => {},
      (_error) => alert('device can not be deleted')
    );
  }

  getDevicesForAuthenticatedUser(): Observable<DeviceDto[]> {
    var id = this.authService.getIdFromToken(this.authService.getToken());
    return this.apiClient.all(id).pipe(map((response) => response.value || []));
  }
}
