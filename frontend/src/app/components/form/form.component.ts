import {Component, OnInit} from '@angular/core';
import {AbstractControl, FormBuilder, Validators} from "@angular/forms";
import {DataProviderService} from "../../services/data-provider.service";
import Registration from "../../models/Registration";
import Vaccination from "../../models/Vaccination";

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss']
})
export class FormComponent implements OnInit {
  registrationForm = this.formBuilder.group({
    ssn: ['', [Validators.required, Validators.min(999999999), Validators.max(9999999999)]],
    pin: ['', [Validators.required, Validators.min(99999), Validators.max(999999)]]
  });
  dateForm = this.formBuilder.group({
    date: ['2021-01-01', [Validators.required]]
  });

  step = 1;
  registration: Registration | null = null;
  timeslots!: string[];
  vaccination: Vaccination | null = null;
  selectedTimeslot = '';

  constructor(private formBuilder: FormBuilder, private dataProvider: DataProviderService) {
  }

  ngOnInit(): void {
  }

  nextStep() {
    if (!this.registrationForm.valid) {
      this.registrationForm.markAllAsTouched();
    }

    if (this.step === 1 && this.registrationForm.valid) {

      this.dataProvider.validateRegistration(this.ssn.value, this.pin.value).subscribe(resolve => {
        this.registration = resolve;
        this.step++;
      });

    } else if (this.step === 2) {

      this.dataProvider.getTimeslots(this.date.value).subscribe(resolve => {
        this.timeslots = resolve.map(timeslot => timeslot.substr(11));
        this.step++;
      });

    } else if (this.step === 3) {

      const vaccination: Vaccination = {
        registrationId: this.registration!.id,
        vaccinationDate: `${this.date.value}T${this.selectedTimeslot}`
      };
      this.dataProvider.addVaccination(vaccination).subscribe(resolve => {
        this.vaccination = resolve;
        console.log(this.vaccination);
        this.step++;
      });

    }
  }

  get ssn(): AbstractControl {
    return <AbstractControl>this.registrationForm.get('ssn');
  }

  get pin(): AbstractControl {
    return <AbstractControl>this.registrationForm.get('pin');
  }

  get date(): AbstractControl {
    return <AbstractControl>this.dateForm.get('date');
  }
}
