import {LogPublisher} from '../log-publisher';
import {Observable, of} from 'rxjs';
import {LogEntry} from '../log-entry';
import {PersistenceService, LOG_STORE_NAME} from '../../persistence-service/persistence.service';
import {LogLevel} from '../log-level';
import {LogDbEntry} from './log-db-entry';

export class IndexedDbLogPublisher extends LogPublisher {

  constructor(private persistenceService: PersistenceService) {
    super();
  }

  clear(): Observable<boolean> {
    // TODO
    return of(false);
  }

  write(entry: LogEntry): Observable<boolean> {
    let result = true;

    const entryToPersist: LogDbEntry = {
      message: entry.message,
      logLevel: LogLevel[entry.logLevel],
      extraInfo: entry.extraInfoToString(),
      dateTime: entry.dateTime
    };

    this.persistenceService.save(LOG_STORE_NAME, entryToPersist)
      .subscribe(null, error => {
        console.error(`LOG SERVICE ERROR:\n${JSON.stringify(error)}`);
        result = false;
      });

    return of(result);
  }
}
