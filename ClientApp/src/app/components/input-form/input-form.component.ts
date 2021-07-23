import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-input-form',
  templateUrl: './input-form.component.html',
  styleUrls: ['./input-form.component.scss'],
})
export class InputFormComponent implements OnInit {
  formGroup: FormGroup;

  constructor() {
    this.formGroup = new FormGroup({
      city: new FormControl('Melbourne', [Validators.required]),
      country: new FormControl('au', [Validators.required, Validators.minLength(2), Validators.maxLength(2)])
    })
    this.formGroup.markAsTouched();
  }

  ngOnInit(): void {}

  onSubmit() {
    console.log(this.formGroup.value);
  }
}
