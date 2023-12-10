import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChartDialogComponent } from './chart-dialog.component';

describe('ChartDialogComponent', () => {
  let component: ChartDialogComponent;
  let fixture: ComponentFixture<ChartDialogComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ChartDialogComponent]
    });
    fixture = TestBed.createComponent(ChartDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
