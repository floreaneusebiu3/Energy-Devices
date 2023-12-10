import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { DeviceDto } from 'src/app/api-management/client-devices-management';
import { ChartDialogComponent } from 'src/app/components/chart-dialog/chart-dialog.component';
import { environment } from 'src/app/enviroments/enviroment';
import { AuthService } from 'src/app/service/auth/auth.service';
import { DevicesService } from 'src/app/service/devices/devices.service';

@Component({
  selector: 'app-client',
  templateUrl: './client.component.html',
  styleUrls: ['./client.component.scss'],
})
export class ClientComponent {
  public notifications: Warning[] = [];
  private connection!: HubConnection;
  public displayedColumns: { [key: string]: string } = {
    name: 'Name',
    description: 'Description',
    maximuHourlyEnergyConsumption: 'Energy Consumption',
  };

  public dataSource: any[] = [];

  constructor(
    private readonly devicesService: DevicesService,
    private readonly authService: AuthService,
    private readonly dialog: MatDialog
  ) {}

  ngOnInit(): void {
    console.log(this.authService.getIdFromToken());
    this.devicesService
      .getDevicesForAuthenticatedUser()
      .subscribe((devices) => {
        this.dataSource = devices;
      });
    this.initWebSocket();
    this.connection.start();
  }

  initWebSocket() {
    this.connection = new HubConnectionBuilder()
      .withUrl(environment.monitoringNotificationWebSocketUrl)
      .build();
    this.connection.on(
      this.authService.getIdFromToken(),
      (message: Warning) => {
        message.deviceId = this.getDeviceName(message.deviceId);
        if (this.notifications.length == 4) {
          this.notifications = [];
        }
        this.notifications.push(message);
      }
    );
  }

  getDeviceName(deviceId: string): string {
    return this.dataSource.filter((ds) => ds.id === deviceId).at(0).name;
  }

  showChart(deviceDto: DeviceDto): void {
    this.dialog.open(ChartDialogComponent, {
      data: { selectedDevice: deviceDto },
    });
  }
}

export class Warning {
  deviceId: string;
  maximumConsumption: string;
  currentConsumption: string;

  constructor(
    deviceId: string,
    maximumConsumption: string,
    currentConsumption: string
  ) {
    this.deviceId = deviceId;
    this.maximumConsumption = maximumConsumption;
    this.currentConsumption = currentConsumption;
  }
}
