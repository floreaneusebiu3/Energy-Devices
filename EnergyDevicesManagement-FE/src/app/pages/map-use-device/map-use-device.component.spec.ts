import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MapUseDeviceComponent } from './map-use-device.component';

describe('MapUseDeviceComponent', () => {
  let component: MapUseDeviceComponent;
  let fixture: ComponentFixture<MapUseDeviceComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MapUseDeviceComponent]
    });
    fixture = TestBed.createComponent(MapUseDeviceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
