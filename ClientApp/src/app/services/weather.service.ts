import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of, Subject } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { WeatherRequest } from '../models/weather-request';
import { WeatherState } from '../models/weather-state';

@Injectable({
  providedIn: 'root',
})
export class WeatherService {
  private readonly baseUrl = `${environment.apiUrl}/weather`;

  constructor(private httpClient: HttpClient) {}

  private stateSubject = new Subject<WeatherState>();
  public state$ = this.stateSubject.asObservable();

  getWeather(body: WeatherRequest) {
    this.httpClient
      .post<WeatherState>(this.baseUrl, body)
      .pipe(
        tap((state) => {
          this.stateSubject.next(state);
        }),
        catchError((err) => {
          console.log(err);
          this.stateSubject.next(undefined);
          return of(undefined);
        })
      )
      .subscribe();
  }
}
