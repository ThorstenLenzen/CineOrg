import {Component, Input, ViewChild} from '@angular/core';
import {MatTableDataSource} from '@angular/material/table';
import {MatPaginator} from '@angular/material/paginator';
import {LogEntry} from '../log-entry';

@Component({
  selector: 'app-list-log-entries',
  templateUrl: './list-log-entries.component.html',
  styleUrls: ['./list-log-entries.component.scss']
})
export class ListLogEntriesComponent {

  public displayedColumns: string[] = ['date', 'time', 'logLevel', 'message', 'extraInfo'];

  public dataSource: MatTableDataSource<LogEntry>;

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;

  @Input() set logEntries(entries: LogEntry[]) {
    this.dataSource = new MatTableDataSource<LogEntry>(entries);
    this.dataSource.paginator = this.paginator;
  }

  public applyLogLevelFilter(value: string): void {
    this.dataSource.filter = value.trim().toLowerCase();
  }
}
