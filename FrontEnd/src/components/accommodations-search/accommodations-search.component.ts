import { Component, Input } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
import { TouristSpot } from 'src/models/tourist-spot';

@Component({
  selector: 'app-accommodations-search',
  templateUrl: './accommodations-search.component.html',
  styleUrls: ['./accommodations-search.component.css']
})
export class AccommodationsSearchComponent {
  
  startingDate = new Date();
  finishingDate = new Date();
  @Input() spot: TouristSpot;

  constructor(private fb: FormBuilder) {}

  changeStartingDate(event: MatDatepickerInputEvent<Date>){
    this.startingDate = event.value
  }

  changeFinishingDate(event: MatDatepickerInputEvent<Date>){
    this.finishingDate = event.value
  }
  
  onSubmit() {
    alert('Thanks!');
  }
}
