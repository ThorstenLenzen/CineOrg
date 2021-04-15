import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {TheatersComponent} from './theaters/theaters.component';


const routes: Routes = [
  { path: 'home', loadChildren: () => import('./home/home.module').then(m => m.HomeModule) },
  { path: 'movies', loadChildren: () => import('./movies/movies.module').then(m => m.MoviesModule) },
  { path: 'logs', loadChildren: () => import('./log-display/log-display.module').then(m => m.LogDisplayModule) },
  { path: 'theaters', component: TheatersComponent },
  { path: '', redirectTo: '/home', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
