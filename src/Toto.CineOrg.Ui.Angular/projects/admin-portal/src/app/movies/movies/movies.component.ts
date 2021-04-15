import {Component, OnInit} from '@angular/core';
import {MatDialog} from '@angular/material/dialog';
import {EMPTY} from 'rxjs';
import {catchError} from 'rxjs/operators';

import {LogService, NotificationService, NotificationSeverity} from 'basic-services';

import {Movie, MovieForCreate, MovieForUpdate} from '../../models';
import {AddMovieDialogComponent} from '../add-movie-dialog/add-movie-dialog.component';
import {DeleteMovieDialogComponent} from '../delete-movie-dialog/delete-movie-dialog.component';
import {MovieService, GenreService} from '../../data-services';
import {EditMovieDialogComponent} from '../edit-movie-dialog/edit-movie-dialog.component';


@Component({
  selector: 'app-movies',
  templateUrl: './movies.component.html',
  styleUrls: ['./movies.component.scss']
})
export class MoviesComponent implements OnInit {

  public genres$ = this.genreService.genres$
    .pipe(
      catchError( error => {
        this.notificationService.notify(NotificationSeverity.error, JSON.stringify(error), 'Error getting genres');
        return EMPTY;
      })
    );

  public movies: Movie[] = [];

  constructor(private notificationService: NotificationService,
              private logService: LogService,
              private movieService: MovieService,
              private genreService: GenreService,
              private dialog: MatDialog) { }

  public ngOnInit(): void {
    this.getMovies();
  }

  public onAdd(): void {
    const dialogRef = this.dialog.open(AddMovieDialogComponent, {
      width: '450px',
      disableClose: true,
      data: { genres$: this.genres$ }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result == null) { return; }

      const movie = result as MovieForCreate;

      this.movieService
        .createMovie(movie)
        .subscribe(
          createdMovie => {
            this.logService.logDebug(`${createdMovie.title} was saved.`, [createdMovie]);
            this.notificationService.notify(NotificationSeverity.success, `${createdMovie.title} was saved.`, 'Saving success');
          },
          error => {
            this.logService.logError(JSON.stringify(error));
            this.notificationService.notify(NotificationSeverity.error, JSON.stringify(error), 'Error creating movie');
          }
        );
    });
  }

  public onEdit(id: string): void {
    const movie = this.movies.find(mov => mov.id === id);

    const dialogRef = this.dialog.open(EditMovieDialogComponent, {
      width: '450px',
      disableClose: true,
      data: { movie, genres$: this.genres$ }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result == null) { return; }

      const movieForUpdate = result as MovieForUpdate;

      this.movieService
        .updateMovie(id, movieForUpdate)
        .subscribe(
          data => {
            this.notificationService.notify(NotificationSeverity.success, `${movie.title} was updated.`, 'Update success');
            this.getMovies();
            console.log(data);
          },
          error => this.notificationService.notify(NotificationSeverity.error, JSON.stringify(error), 'Error updating movie')
        );
    });
  }

  public onDelete(id: string): void {
    const movie = this.movies.find(mov => mov.id === id);

    const dialogRef = this.dialog.open(DeleteMovieDialogComponent, {
      width: '450px',
      disableClose: true,
      data: { movie }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result == null) { return; }

      this.movieService
        .deleteMovie((result as Movie).id)
        .subscribe(
          data => {
            this.notificationService.notify(NotificationSeverity.success, `${movie.title} was deleted.`, 'Deletion success');
            this.getMovies();
            console.log(data);
          },
          error => console.error(error)
        );
    });
  }

  private getMovies(): void {
    this.movieService.movies$
      .subscribe(
        movies => this.movies = movies,
        error => this.notificationService.notify(NotificationSeverity.error, JSON.stringify(error), 'Error getting movies'));
  }
}
