import { Component, OnInit } from '@angular/core';
import { UserDeviceService } from 'src/app/service/userDevice/user-device.service';

@Component({
  selector: 'app-user-devices',
  templateUrl: './user-devices.component.html',
  styleUrls: ['./user-devices.component.css'],
})
export class UserDevicesComponent implements OnInit {
  public showButtons: boolean = false;
  public displayedColumns: { [key: string]: string } = {
    userName: 'User Name',
    deviceName: 'Device Name',
    description: 'Description',
    address: 'Address',
    maximuHourlyEnergyConsumption: 'Energy Consumption',
  };

  public dataSource: any[] = [];

  constructor(private readonly userDeviceService: UserDeviceService) {}

  ngOnInit(): void {
    this.userDeviceService
      .getUserDevices()
      .subscribe((userDevices) => (this.dataSource = userDevices));
  }

  deleteUserDevice(userDevice: any) {
    this.userDeviceService.deleteUserDevice(userDevice);
    window.location.reload();
  }
}
