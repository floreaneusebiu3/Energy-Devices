import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DialogEditDeviceComponent } from './dialog-edit-device.component';

describe('DialogEditDeviceComponent', () => {
  let component: DialogEditDeviceComponent;
  let fixture: ComponentFixture<DialogEditDeviceComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DialogEditDeviceComponent]
    });
    fixture = TestBed.createComponent(DialogEditDeviceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
