import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {
  HourlyMeasurementIEnumerableResponse,
  MonitoringClient,
} from 'src/app/api-management/monitoring-management';
import { Observable } from 'rxjs';
import { environment } from 'src/app/enviroments/enviroment';

@Injectable({
  providedIn: 'root',
})
export class MonitoringService {
  private apiClient;
  private apiUrl = environment.monitoringManagementUrl;

  constructor(private httpClient: HttpClient) {
    this.apiClient = new MonitoringClient(this.httpClient, this.apiUrl);
  }

  getUserDeviceMeasurements(
    userId: string,
    deviceId: string,
    timestamp: number
  ): Observable<HourlyMeasurementIEnumerableResponse> {
    return this.apiClient.monitoring(userId, deviceId, timestamp);
  }
}
