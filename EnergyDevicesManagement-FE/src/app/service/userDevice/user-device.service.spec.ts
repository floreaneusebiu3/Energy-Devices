import { TestBed } from '@angular/core/testing';

import { UserDeviceService } from './user-device.service';

describe('UserDeviceService', () => {
  let service: UserDeviceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UserDeviceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
