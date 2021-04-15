import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {MaterialModule} from '../material.module';

import {AddMovieDialogComponent} from './add-movie-dialog/add-movie-dialog.component';
import {DeleteMovieDialogComponent} from './delete-movie-dialog/delete-movie-dialog.component';
import {EditMovieDialogComponent} from './edit-movie-dialog/edit-movie-dialog.component';
import {FormsModule} from '@angular/forms';
import {MoviesRoutingModule} from './movies-routing.module';
import {MoviesComponent} from './movies/movies.component';
import {ListMoviesComponent} from './list-movies/list-movies.component';



@NgModule({
  declarations: [
    AddMovieDialogComponent,
    DeleteMovieDialogComponent,
    EditMovieDialogComponent,
    ListMoviesComponent,
    MoviesComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    FormsModule,
    MoviesRoutingModule
  ],
  exports: [
    MoviesComponent
  ]
})
export class MoviesModule { }
