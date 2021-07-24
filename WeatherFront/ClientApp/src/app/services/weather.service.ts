import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { BehaviorSubject, of, Subject } from 'rxjs';
import { catchError, finalize, tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { WeatherRequest } from '../models/weather-request';
import { WeatherState } from '../models/weather-state';
import { NotificationService } from './notification.service';

@Injectable({
  providedIn: 'root',
})
export class WeatherService {
  private readonly baseUrl = `${environment.apiUrl}/weather`;

  private stateSubject = new Subject<WeatherState>();
  private busySubject = new BehaviorSubject<boolean>(false);

  public state$ = this.stateSubject.asObservable();
  public busy$ = this.busySubject.asObservable();

  constructor(private httpClient: HttpClient, private notificationService: NotificationService) {}

  getWeather(body: WeatherRequest) {
    this.busySubject.next(true);
    this.stateSubject.next(undefined);
    this.httpClient
      .post<WeatherState>(this.baseUrl, body)
      .pipe(
        tap((state) => {
          this.stateSubject.next(state);
        }),
        catchError((err: HttpErrorResponse) => {
          this.notificationService.showNotificaton(err.error);
          this.stateSubject.next(undefined);
          return of(undefined);
        }),
        finalize(() => {
          this.busySubject.next(false);
        })
      )
      .subscribe();
  }

  reset() {
    this.busySubject.next(false);
    this.stateSubject.next(undefined);
  }
}
