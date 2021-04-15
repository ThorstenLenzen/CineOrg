import {Inject, Injectable} from '@angular/core';
import {LogPublisher} from './log-publisher';
import {ConsoleLogPublisher} from './publishers/console-log-publisher';
import {PersistenceService} from '../persistence-service/persistence.service';
import {IndexedDbLogPublisher} from './publishers/indexed-db-log-publisher';

@Injectable({
  providedIn: 'root'
})
export class LogPublisherService {

  public publishers: LogPublisher[] = [];

  constructor(
    @Inject('environment') private environment,
    private persistenceService: PersistenceService) {
    this.buildPublishers();
  }

  private buildPublishers(): void {
    // TODO: More publishers...

    const publishers = this.environment.logPublishers;

    if (publishers.find(pub => pub === 'console')) {
      const consolePublisher = new ConsoleLogPublisher();
      this.publishers.push(consolePublisher);
    }

    if (publishers.find(pub => pub === 'indexed-db')) {
      const indexedDBPublisher = new IndexedDbLogPublisher(this.persistenceService);
      this.publishers.push(indexedDBPublisher);
    }
  }
}
