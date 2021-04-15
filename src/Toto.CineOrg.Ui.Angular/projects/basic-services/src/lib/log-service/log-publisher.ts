import { LogEntry } from './log-entry';
import { Observable } from 'rxjs';

export abstract class LogPublisher {
  location: string;

  abstract write(entry: LogEntry): Observable<boolean>;
  abstract clear(): Observable<boolean>;
}
