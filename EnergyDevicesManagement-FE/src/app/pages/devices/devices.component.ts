import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { DeviceDto } from 'src/app/api-management/client-devices-management';
import { DialogCreateDeviceComponent } from 'src/app/components/dialogCreateDevice/dialog-create-device/dialog-create-device.component';
import { DialogEditDeviceComponent } from 'src/app/components/dialogEditDevice/dialog-edit-device/dialog-edit-device.component';
import { DevicesService } from 'src/app/service/devices/devices.service';

@Component({
  selector: 'app-devices',
  templateUrl: './devices.component.html',
  styleUrls: ['./devices.component.css']
})
export class DevicesComponent {
  public createDeviceText: string = "Create new device";
  public displayedColumns: { [key: string]: string } =
    {
      name: "Name",
      description: "Description",
      maximuHourlyEnergyConsumption: "Energy Consumption"
    };

  public dataSource: any[] = [];

  constructor(private readonly devicesService: DevicesService, private dialog: MatDialog) {
  }

  ngOnInit(): void {
    this.devicesService.getDevices().subscribe((devices => {
      this.dataSource = devices;
      console.log(devices)
    }
    ));
  }

  openCreateDialog() {
    var dialogRef = this.dialog.open(DialogCreateDeviceComponent);
    dialogRef.afterClosed()
      .subscribe(() => { this.refresh(); })
  }

  deleteDevice(deviceDto: DeviceDto) {
    this.devicesService.deleteDevice(deviceDto.id!);
    this.refresh();
  }

  editDevice(deviceDto: DeviceDto) {
    var dialogRef = this.dialog.open(DialogEditDeviceComponent, {
      data: deviceDto
    });
    dialogRef.afterClosed()
      .subscribe(() => { this.refresh(); })
  }

  refresh() {
    window.location.reload();
  }
}