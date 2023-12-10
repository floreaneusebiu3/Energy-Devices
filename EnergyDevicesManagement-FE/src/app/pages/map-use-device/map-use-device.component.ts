import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DevicesService } from 'src/app/service/devices/devices.service';
import { UserDeviceService } from 'src/app/service/userDevice/user-device.service';
import { UsersService } from 'src/app/service/users/users.service';

@Component({
  selector: 'app-map-use-device',
  templateUrl: './map-use-device.component.html',
  styleUrls: ['./map-use-device.component.css'],
})
export class MapUseDeviceComponent implements OnInit {
  public createUserDeviceForm!: FormGroup;
  public users:
    | { id: string | undefined; name: string | undefined }[]
    | undefined;
  public devices:
    | { id: string | undefined; name: string | undefined }[]
    | undefined;

  constructor(
    private readonly userService: UsersService,
    private readonly deviceService: DevicesService,
    private formBuilder: FormBuilder,
    private readonly userDeviceService: UserDeviceService
  ) {}

  ngOnInit(): void {
    this.createUserDeviceForm = this.formBuilder.group({
      userId: ['', Validators.required],
      deviceId: ['', Validators.required],
      address: ['', Validators.required],
    });
    this.userService
      .getUsers()
      .subscribe(
        (users) =>
          (this.users = users.map((user) => ({ id: user.id, name: user.name })))
      );
    this.deviceService.getDevices().subscribe(
      (devices) =>
        (this.devices = devices.map((device) => ({
          id: device.id,
          name: device.name,
        })))
    );
  }

  insertNewUserDevice(): void {
    if (!this.createUserDeviceForm.valid) {
      window.alert('Provided informations are not valid');
      return;
    }
    const userDeviceDto = this.createUserDeviceForm.value;
    this.userDeviceService.insertUserDevice(userDeviceDto);
    this.createUserDeviceForm.reset();
  }
}
