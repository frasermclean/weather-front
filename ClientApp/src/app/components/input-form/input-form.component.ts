import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { WeatherRequest } from 'src/app/models/weather-request';
import { WeatherService } from 'src/app/services/weather.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-input-form',
  templateUrl: './input-form.component.html',
  styleUrls: ['./input-form.component.scss'],
})
export class InputFormComponent implements OnInit {
  formGroup: FormGroup;

  constructor(private weatherService: WeatherService) {
    this.formGroup = new FormGroup({
      city: new FormControl('Melbourne', [Validators.required]),
      country: new FormControl('au', [
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(2),
      ]),
    });
    this.formGroup.markAsTouched();
  }

  ngOnInit(): void {}

  onSubmit() {
    const req: WeatherRequest = {
      city: this.formGroup.value.city,
      country: this.formGroup.value.country,
      apiKey: environment.apiKey,
    };
    this.weatherService.getWeather(req);
  }

  onReset() {
    this.weatherService.reset();
  }
}
