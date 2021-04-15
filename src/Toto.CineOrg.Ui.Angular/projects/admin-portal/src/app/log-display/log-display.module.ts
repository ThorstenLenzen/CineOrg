import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {MaterialModule} from '../material.module';

import { LogDisplayRoutingModule } from './log-display-routing.module';
import { LogDisplayComponent } from './log-display/log-display.component';
import { ListLogEntriesComponent } from './list-log-entries/list-log-entries.component';


@NgModule({
  declarations: [LogDisplayComponent, ListLogEntriesComponent],
  imports: [
    CommonModule,
    MaterialModule,
    LogDisplayRoutingModule
  ]
})
export class LogDisplayModule { }
