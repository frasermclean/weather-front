import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { WeatherState } from 'src/app/models/weather-state';
import { WeatherService } from 'src/app/services/weather.service';

@Component({
  selector: 'app-result-view',
  templateUrl: './result-view.component.html',
  styleUrls: ['./result-view.component.scss'],
})
export class ResultViewComponent implements OnInit {
  state$: Observable<WeatherState>;
  busy$: Observable<boolean>;

  constructor(private weatherService: WeatherService) {
    this.state$ = this.weatherService.state$;
    this.busy$ = this.weatherService.busy$;
  }

  ngOnInit(): void {}
}
