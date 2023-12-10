import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DialogCreateDeviceComponent } from './dialog-create-device.component';

describe('DialogCreateDeviceComponent', () => {
  let component: DialogCreateDeviceComponent;
  let fixture: ComponentFixture<DialogCreateDeviceComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DialogCreateDeviceComponent]
    });
    fixture = TestBed.createComponent(DialogCreateDeviceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
