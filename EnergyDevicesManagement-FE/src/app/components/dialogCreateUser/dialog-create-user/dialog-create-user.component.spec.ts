import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DialogCreateUserComponent } from './dialog-create-user.component';

describe('DialogCreateUserComponent', () => {
  let component: DialogCreateUserComponent;
  let fixture: ComponentFixture<DialogCreateUserComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DialogCreateUserComponent]
    });
    fixture = TestBed.createComponent(DialogCreateUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
