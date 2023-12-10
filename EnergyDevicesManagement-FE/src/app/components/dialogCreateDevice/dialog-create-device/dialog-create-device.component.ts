import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DevicesService } from 'src/app/service/devices/devices.service';

@Component({
  selector: 'app-dialog-create-device',
  templateUrl: './dialog-create-device.component.html',
  styleUrls: ['./dialog-create-device.component.css'],
})
export class DialogCreateDeviceComponent implements OnInit {
  public createDeviceForm!: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private deviceService: DevicesService
  ) {}

  ngOnInit(): void {
    this.createDeviceForm = this.formBuilder.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      maximuHourlyEnergyConsumption: ['', Validators.required],
    });
  }

  insertDevice() {
    if (!this.createDeviceForm.valid) {
      alert('Provided informations are not valid');
    }
    var device = this.createDeviceForm.value;
    this.deviceService.insertDevice(device);
    this.createDeviceForm.reset();
  }
}
