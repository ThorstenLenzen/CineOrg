import {LogLevel} from './log-level';

export class LogEntry {
  message: string;
  logLevel: LogLevel;
  extraInfo: any[];
  logWithDate: boolean;

  public get dateTime(): string {
    const now = new Date();
    const date = now.getFullYear() + '-' + (now.getMonth() + 1) + '-' + now.getDate();
    const time = now.getHours() + ':' + now.getMinutes() + ':' + now.getSeconds();

    return this.logWithDate ? date + ' ' + time : date
  }

  public toString(): string {
    const entry = `(${this.dateTime}) [${LogLevel[this.logLevel]}]: ${this.message}; ${this.extraInfoToString()}`;
    return entry;
  }

  public extraInfoToString(): string {
    if (this.extraInfo == null || this.extraInfo === undefined) {
      return '';
    }

    let ret: string = this.extraInfo.join(', ');

    // Is there at least one object in the array?
    if (this.extraInfo.some(p => typeof p === 'object')) {
      ret = '';

      // Build comma-delimited string
      for (const item of this.extraInfo) {
        ret += JSON.stringify(item) + ', ';
      }
    }

    return ret;
  }
}
