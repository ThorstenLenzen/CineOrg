import { Injectable } from '@angular/core';
import {IDBPDatabase, openDB} from 'idb';
import {from, Observable} from 'rxjs';

const DB_NAME = 'CineOrg';
export const LOG_STORE_NAME = 'logStore';

@Injectable({
  providedIn: 'root'
})
export class PersistenceService {

  private dbInstance: IDBPDatabase;

  constructor() { }

  public async connect(): Promise<void> {
    this.dbInstance = await openDB(DB_NAME, 1, {
      upgrade(db) {
        db.createObjectStore(LOG_STORE_NAME, { autoIncrement: true });
      }
    });

    this.dbInstance.onerror = (error: any) => console.error(`LOG SERVICE ERROR:\n${JSON.stringify(error)}`);
  }

  public getAll(storeName: string): Observable<any> {
    return from(this.dbInstance.getAll(storeName));
  }

  public save(storeName: string, item: any): Observable<any> {
    return from(this.dbInstance.put(storeName, item));
  }
}
