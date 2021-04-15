import {Inject, Injectable} from '@angular/core';
import {LogLevel} from './log-level';
import {LogEntry} from './log-entry';
import {LogPublisher} from './log-publisher';
import {LogPublisherService} from './log-publisher.service';


@Injectable({
  providedIn: 'root'
})
export class LogService {

  private level: LogLevel = this.environment.logLevel;
  private logWithDate: boolean = this.environment.logWithDate;
  private readonly publishers: LogPublisher[] = [];

  constructor(
    @Inject('environment') private environment,
    private logPublisherService: LogPublisherService) {
    this.publishers = this.logPublisherService.publishers;
  }

  public logDebug(msg: string, params?: any[]): void {
    this.writeToLog(msg, LogLevel.Debug, params);
  }

  public logInfo(msg: string, params?: any[]): void {
    this.writeToLog(msg, LogLevel.Info, params);
  }

  public logWarn(msg: string, params?: any[]): void {
    this.writeToLog(msg, LogLevel.Warn, params);
  }

  public logError(msg: string, params?: any[]): void {
    this.writeToLog(msg, LogLevel.Error, params);
  }

  public logFatal(msg: string, params?: any[]): void {
    this.writeToLog(msg, LogLevel.Fatal, params);
  }

  private writeToLog(msg: string, level: LogLevel, params?: any[]) {
    if (!this.shouldLog(level)) {
      return;
    }

    const entry: LogEntry = new LogEntry();
    entry.message = msg;
    entry.logLevel = level;
    entry.extraInfo = params;
    entry.logWithDate = this.logWithDate;

    for (const publisher of this.publishers) {
      publisher.write(entry).subscribe(console.log);
    }
  }

  private shouldLog(level: LogLevel): boolean {
    return (level >= this.level && level !== LogLevel.Off) || this.level === LogLevel.All;
  }
}
