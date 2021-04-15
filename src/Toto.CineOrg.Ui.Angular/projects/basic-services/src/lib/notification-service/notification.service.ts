import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import {NotificationSeverity} from './notification-severity.enum';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor(private toastr: ToastrService) { }

  public notify(severity: NotificationSeverity, message: string, title: string): void {
    if (message == null || message === undefined) {
      return;
    }

    const toastrOptions = {
      positionClass: 'toast-bottom-right'
    };

    switch (severity) {
      case NotificationSeverity.error:
        if (title == null || title === undefined) {
          title = 'Error';
        }
        this.toastr.error(message, title, toastrOptions);
        break;
      case NotificationSeverity.info:
        if (title == null || title === undefined) {
          title = 'Info';
        }
        this.toastr.info(message, title, toastrOptions);
        break;
      case NotificationSeverity.success:
        if (title == null || title === undefined) {
          title = 'Success';
        }
        this.toastr.success(message, title, toastrOptions);
        break;
      case NotificationSeverity.warning:
        if (title == null || title === undefined) {
          title = 'Warning';
        }
        this.toastr.warning(message, title, toastrOptions);
        break;
    }
  }
}
