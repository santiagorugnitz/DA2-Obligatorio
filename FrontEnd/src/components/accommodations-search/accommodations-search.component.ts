import { Component, Inject, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
import { dateInputsHaveChanged } from '@angular/material/datepicker/datepicker-input-base';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute } from '@angular/router';
import { Accommodation } from 'src/models/accommodation';
import { SelectedImage } from 'src/models/selected-image';
import { TouristSpot } from 'src/models/tourist-spot';
import { AccommodationService } from 'src/services/accommodation.service';
import { SpotService } from 'src/services/spot.service';
import { AccommodationCommentsComponent } from '../accommodation-comments/accommodation-comments.component';

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
  selectedImages: SelectedImage[]

  constructor(private fb: FormBuilder, private currentRoute: ActivatedRoute, private spotService: SpotService, 
    private accommodationService:AccommodationService, private _snackBar: MatSnackBar, public dialog: MatDialog) {
    this.adultQuantity = 1
    this.retiredQuantity = 0
    this.childrenQuantity = 0
    this.babyQuantity = 0
    this.hasSearched = false 
    this.accommodations = []
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

  search(){
    if(!this.hasSearched){
      if(this.startingDate >= new Date() && this.startingDate < this.finishingDate){
        this.accommodations = this.accommodationService.getAccommodationByTouristSpot(this.spot.Id)
        this.selectedImages = []
        for (var _i = 0; _i < this.accommodations.length; _i++) {
          this.selectedImages.push({accommodationId:this.accommodations[_i].Id, imageNumber:0})
        }
        this.hasSearched = !this.hasSearched
      }else{
        alert('The dates are incorrect');
      }
    }else{
      this.hasSearched = !this.hasSearched
      this.accommodations = []
      this.selectedImages = []
    }  
  }

  getSelectedImage(accomodationId:number){
    var selectedImage: number
    for (let image of this.selectedImages){
      if(image.accommodationId == accomodationId){
        selectedImage = image.imageNumber
      }
    }
    return selectedImage
  }

  nextImage(accomodationId:number){
    if(this.moreImagesLeft(accomodationId)){
      for (let image of this.selectedImages){
        if(image.accommodationId == accomodationId){
          image.imageNumber++
        }
      }
    }else{
      alert('There is no more images')
    }
  }

  moreImagesLeft(accomodationId:number): boolean{
    var selectedImage: number
    for (let image of this.selectedImages){
      if(image.accommodationId == accomodationId){
        selectedImage = image.imageNumber
      }
    }
    return selectedImage < this.getAccommodationById(accomodationId).Images.length-1
  }

  getAccommodationById(id:number){
    var accomodation: Accommodation
    for (var _i = 0; _i < this.accommodations.length; _i++) {
      if(this.accommodations[_i].Id == id){
        accomodation = this.accommodations[_i]
      }
    }
    return accomodation
  }

  getAccommodationComments(id: number, name: string): void{
    const commentList = this.accommodationService.getAccommodationComments(id)  
    const dialogRef = this.dialog.open(AccommodationCommentsComponent, {
        width: '250px',
        data: {comments: commentList, accommodationName: name}
      });
  }
}
