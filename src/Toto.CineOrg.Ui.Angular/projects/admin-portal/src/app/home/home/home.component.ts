import {Component, OnInit} from '@angular/core';
import {MovieService} from '../../data-services';
import {LogService, NotificationService, NotificationSeverity} from 'basic-services';
import {catchError, tap} from 'rxjs/operators';
import {EMPTY} from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {

  public newMovies$ = this.movieService
    .newMovies$
    .pipe(
      catchError( error => {
        this.notificationService.notify(NotificationSeverity.error, JSON.stringify(error), 'Error getting new movies');
        return EMPTY;
      }),
      tap( movies => this.logService.logInfo(`${movies.length} movies were returned from the Backend`))
    );

  constructor(private movieService: MovieService, private notificationService: NotificationService, private logService: LogService) { }
}
