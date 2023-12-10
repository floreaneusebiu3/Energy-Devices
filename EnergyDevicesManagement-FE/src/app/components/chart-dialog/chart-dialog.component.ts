import { Component, Inject, OnChanges, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DeviceDto } from 'src/app/api-management/client-devices-management';
import { AuthService } from 'src/app/service/auth/auth.service';
import { MonitoringService } from 'src/app/service/monitoring/monitoring.service';

@Component({
  selector: 'app-chart-dialog',
  templateUrl: './chart-dialog.component.html',
  styleUrls: ['./chart-dialog.component.css'],
})
export class ChartDialogComponent implements OnInit {
  chart: any;
  noData = true;

  constructor(
    private readonly monitoringService: MonitoringService,
    private readonly authService: AuthService,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {}

  ngOnInit(): void {}

  handleSelectedDate(selectedDate: Date) {
    this.drawChartForSelectedDate(selectedDate);
  }

  private drawChartForSelectedDate(selectedDate: Date) {
    selectedDate.setHours(-selectedDate.getTimezoneOffset() / 60);
    console.log(this.getTimeStamp(selectedDate));
    this.monitoringService
      .getUserDeviceMeasurements(
        this.authService.getIdFromToken(),
        this.data.selectedDevice.id,
        this.getTimeStamp(selectedDate)
      )
      .subscribe({
        next: (response) => {
          this.chart.options.data[0].dataPoints = [];
          if (response.value?.length) {
            this.noData = false;
            response.value.forEach((value) =>
              this.chart.options.data[0].dataPoints.push({
                x: value.hour,
                y: value.consumption,
              })
            );
          } else {
            this.noData = true;
          }
          this.chart.render();
        },
        error: (err) => console.log(err),
      });
  }

  getChartInstance(chart: object) {
    this.chart = chart;
  }

  chartOptions = {
    theme: 'light2',
    animationEnabled: true,
    zoomEnabled: true,
    data: [
      {
        type: 'line',
        dataPoints: [],
      },
    ],
  };

  private getTimeStamp(selectedDate: Date) {
    return (selectedDate.getTime() - new Date(1970, 0, 1).getTime()) / 1000;
  }
}
