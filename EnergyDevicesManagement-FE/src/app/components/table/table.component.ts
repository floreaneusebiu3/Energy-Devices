import {
  AfterViewInit,
  Component,
  EventEmitter,
  Input,
  Output,
  ViewChild,
} from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { UserDto } from 'src/app/api-management/client-user-management';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css'],
})
export class TableComponent {
  @Input() dataSource: any[] | undefined;
  @Input() displayedColumns: { [key: string]: string } | undefined;
  @Input() showOperationButtons: boolean = false;
  @Input() hasChartButton = false;
  @Output() deleteEvent = new EventEmitter<any>();
  @Output() editEvent = new EventEmitter<any>();
  @Output() chartEvent = new EventEmitter<any>();
  columnNames: string[] = [];
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  public matTableDataSource = new MatTableDataSource<any>();

  ngOnChanges() {
    this.matTableDataSource = new MatTableDataSource<any>(this.dataSource);
    this.matTableDataSource.paginator = this.paginator;
    this.getColumnNames();
  }

  getColumnNames() {
    if (this.columnNames.length == 0) {
      for (let key in this.displayedColumns!) {
        this.columnNames?.push(key.valueOf());
      }
      this.columnNames.push('editIcon');
    }
  }

  emitDeleteEvent(dto: any): void {
    this.deleteEvent.emit(dto);
  }

  emitUpdateEvent(dto: any): void {
    this.editEvent.emit(dto);
  }

  emitChartEvent(dto: any) {
    this.chartEvent.emit(dto);
  }
}
