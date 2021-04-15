import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {LogDisplayComponent} from './log-display/log-display.component';


const routes: Routes = [
  {
    path: '',
    component: LogDisplayComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LogDisplayRoutingModule { }
