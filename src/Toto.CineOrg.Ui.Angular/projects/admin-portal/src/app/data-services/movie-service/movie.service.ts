import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';

import { environment } from '../../../environments/environment';
import {Movie, MovieForCreate, MovieForUpdate} from '../../models';
import {catchError, tap} from 'rxjs/operators';
import {LogService} from 'basic-services';

@Injectable({
  providedIn: 'root'
})
export class MovieService {

  public newMovies$ = this.httpClient
    .get<Movie[]>(`${environment.baseAddress}/movie?take=2&orderBy=created`)
    .pipe(
      catchError( error => {
        this.logService.logError(JSON.stringify(error));
        throw error;
      }),
      tap( movies => {
        this.logService.logDebug(`${movies.length} movies were retrieved.`);
      })
    );

  public movies$  = this.httpClient
    .get<Movie[]>(`${environment.baseAddress}/movie`)
    .pipe(
      catchError( error => {
        this.logService.logError(JSON.stringify(error));
        throw error;
      }),
      tap( movies => {
        this.logService.logDebug(`${movies.length} movies were retrieved.`);
      })
    );

  constructor(private httpClient: HttpClient, private logService: LogService) { }

  public getMovie(id: string): Observable<Movie> {
    return this
      .httpClient
      .get<Movie>(`${environment.baseAddress}/movie?id=${id}`);
  }

  public createMovie(movie: MovieForCreate): Observable<Movie> {
    return this
      .httpClient
      .post<Movie>(`${environment.baseAddress}/movie`, movie);
  }

  public updateMovie(id: string, movie: MovieForUpdate): Observable<void> {
    return this
      .httpClient
      .put<void>(`${environment.baseAddress}/movie/${id}`, movie);
  }

  public deleteMovie(id: string): Observable<void> {
    return this
      .httpClient
      .delete<void>(`${environment.baseAddress}/movie/${id}`);
  }
}
