import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Movie} from '../../models';
import {environment} from '../../../environments/environment';
import {catchError, map, tap} from 'rxjs/operators';
import {LogService} from '../../shared-services';
import {EMPTY, Observable, of, Subject} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ExMovieService {

  private innerMovies$ = this.httpClient
    .get<Movie[]>(`${environment.baseAddress}/movie`);

  private deletedMovie$;
  private deleteMovieSubject = new Subject<string>();

  constructor(private httpClient: HttpClient, private logService: LogService) {

    (this.deleteMovieSubject as Observable<string>)
      .pipe(
        map(id => this.deletedMovie$ = this.httpClient.delete<void>(`${environment.baseAddress}/movie/${id}`).pipe(
          map(() => id)
        ))
      );
  }

  public getMovieById = (id: string): Observable<Movie> => this.innerMovies$
      .pipe(
        map(movies => movies.find(movie => movie.id === id))
      )

  public deleteMovie(id: string): Observable<void> {
    this.deleteMovieSubject.next(id);

    return EMPTY;
  }
}
