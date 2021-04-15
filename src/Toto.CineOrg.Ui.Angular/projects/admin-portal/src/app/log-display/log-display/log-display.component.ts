import {Component, Input, OnInit} from '@angular/core';
import {PersistenceService} from 'basic-services';
import {LogEntry} from '../log-entry';

@Component({
  selector: 'app-log-display',
  templateUrl: './log-display.component.html',
  styleUrls: ['./log-display.component.scss']
})
export class LogDisplayComponent implements OnInit {

  public logEntries: LogEntry[] = [];

  constructor(private persistenceService: PersistenceService) { }

  ngOnInit(): void {
    const storeName = 'logStore';

    this.persistenceService
      .getAll(storeName)
      .subscribe(data => this.logEntries = data);
  }
}
