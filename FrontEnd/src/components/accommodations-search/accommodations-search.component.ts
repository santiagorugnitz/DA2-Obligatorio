import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
import { ActivatedRoute } from '@angular/router';
import { TouristSpot } from 'src/models/tourist-spot';
import { SpotService } from 'src/services/spot.service';

@Component({
  selector: 'app-accommodations-search',
  templateUrl: './accommodations-search.component.html',
  styleUrls: ['./accommodations-search.component.css']
})
export class AccommodationsSearchComponent implements OnInit {
  
  startingDate = new Date();
  finishingDate = new Date();
  spot: TouristSpot;
  adultQuantity: number
  retiredQuantity: number
  childrenQuantity: number
  babyQuantity: number

  constructor(private fb: FormBuilder, private currentRoute: ActivatedRoute, private spotService: SpotService) {
    this.adultQuantity = 1
    this.retiredQuantity = 0
    this.childrenQuantity = 0
    this.babyQuantity = 0
  }
  
  ngOnInit(): void {
    let id = +this.currentRoute.snapshot.params['spotId'];
    this.spot = this.spotService.getSpotById(id);
  }

  changeStartingDate(event: MatDatepickerInputEvent<Date>){
    this.startingDate = event.value
  }

  changeFinishingDate(event: MatDatepickerInputEvent<Date>){
    this.finishingDate = event.value
  }
  
  onSubmit() {
    alert('Thanks!');
  }

  addAdult(){
    this.adultQuantity++;
  }

  addRetired(){
    this.retiredQuantity++;
  }

  addChild(){
    this.childrenQuantity++;
  }

  addBaby(){
    this.babyQuantity++;
  }

  removeAdult(){
    if ((this.adultQuantity > 1 || this.retiredQuantity >= 1) && this.adultQuantity >0){
      this.adultQuantity--;
    }
  }

  removeRetired(){
    if ((this.adultQuantity >= 1 || this.retiredQuantity > 1) && this.retiredQuantity >0){
      this.retiredQuantity--;
    }
  }

  removeChild(){
    if (this.childrenQuantity >0){
      this.childrenQuantity--;
    }
  }

  removeBaby(){
    if (this.babyQuantity >0){
      this.babyQuantity--;
    }
  }


}
