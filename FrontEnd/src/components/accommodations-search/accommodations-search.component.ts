import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
import { dateInputsHaveChanged } from '@angular/material/datepicker/datepicker-input-base';
import { ActivatedRoute } from '@angular/router';
import { Accommodation } from 'src/models/accommodation';
import { TouristSpot } from 'src/models/tourist-spot';
import { AccommodationService } from 'src/services/accommodation.service';
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
  hasSearched: boolean
  accommodations: Accommodation[]

  constructor(private fb: FormBuilder, private currentRoute: ActivatedRoute, private spotService: SpotService, 
    private accommodationService:AccommodationService) {
    this.adultQuantity = 1
    this.retiredQuantity = 0
    this.childrenQuantity = 0
    this.babyQuantity = 0
    this.hasSearched = false 
  }
  
  ngOnInit(): void {
    let id = +this.currentRoute.snapshot.params['spotId'];
    this.spot = this.spotService.getSpotById(id);
    this.accommodations = this.accommodationService.getAccommodationByTouristSpot(this.spot.Id)
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

  search(){
    error:Boolean;
    if(!this.hasSearched){
      if(this.startingDate >= new Date() && this.startingDate < this.finishingDate){
        this.hasSearched = !this.hasSearched
      }else{
        alert('The dates are incorrect');
      }
    }else{
      this.hasSearched = !this.hasSearched
    }  
  }
}
