import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {catchError, shareReplay, tap} from 'rxjs/operators';

import { environment } from '../../../environments/environment';
import {LogService} from 'basic-services';

@Injectable({
  providedIn: 'root'
})
export class GenreService {

  public genres$ = this.httpClient
    .get<string[]>(`${environment.baseAddress}/genre`)
    .pipe(
      shareReplay(1),
      catchError( error => {
        this.logService.logError(JSON.stringify(error));
        throw error;
      }),
      tap( data => {
        this.logService.logDebug(`${(data as string[]).length} genres were retrieved.`);
      })
    );

  constructor(private httpClient: HttpClient, private logService: LogService) { }
}
