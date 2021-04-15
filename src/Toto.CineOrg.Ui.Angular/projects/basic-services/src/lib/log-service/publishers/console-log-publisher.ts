import {LogPublisher} from '../log-publisher';
import {LogEntry} from '../log-entry';
import { Observable, of } from 'rxjs';

export class ConsoleLogPublisher extends LogPublisher {

  public write(entry: LogEntry): Observable<boolean> {
    console.log(entry.toString());
    return of(true);
  }

  public clear(): Observable<boolean> {
    console.clear();
    return of(true);
  }
}
