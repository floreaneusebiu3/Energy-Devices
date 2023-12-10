import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DeviceDto } from 'src/app/api-management/client-devices-management';
import { DevicesService } from 'src/app/service/devices/devices.service';

@Component({
  selector: 'app-dialog-edit-device',
  templateUrl: './dialog-edit-device.component.html',
  styleUrls: ['./dialog-edit-device.component.css']
})
export class DialogEditDeviceComponent implements OnInit {
  public editDeviceForm!: FormGroup;

  constructor(private readonly formBuilder: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data: DeviceDto,
    private readonly deviceService: DevicesService
  ) { }

  ngOnInit(): void {
    this.editDeviceForm = this.formBuilder.group({
      'name': [this.data.name, Validators.required],
      'description': [this.data.description, Validators.required],
      'maximuHourlyEnergyConsumption': [this.data.maximuHourlyEnergyConsumption, Validators.required],
    });
  }

  editDevice() {
    if (!this.editDeviceForm.valid) {
      alert("Provided informations are not valid");
    }
    var device = this.editDeviceForm.value;
    this.deviceService.updateDevice(this.data.id!, device);
  }

}
