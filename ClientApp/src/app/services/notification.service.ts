import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root',
})
export class NotificationService {
  private timer: any;

  constructor(private snackbar: MatSnackBar) {}

  showNotificaton(message: string, timeout = 5000) {
    this.snackbar.open(message, 'OK');
    clearTimeout(this.timer);

    this.timer = setTimeout(() => {
      this.snackbar.dismiss();
    }, timeout);
  }
}
