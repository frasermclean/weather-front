import { Component, OnInit } from '@angular/core';
import { WeatherState } from 'src/app/models/weather-state.model';

@Component({
  selector: 'app-result-view',
  templateUrl: './result-view.component.html',
  styleUrls: ['./result-view.component.scss']
})
export class ResultViewComponent implements OnInit {
  data: WeatherState = {
    city: 'Melbourne',
    country: 'AU',
    temperature: 8.89,
    description: 'light intensity drizzle'
  }

  constructor() { }

  ngOnInit(): void {
  }

}
