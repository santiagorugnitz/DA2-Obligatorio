import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Accommodation } from 'src/models/accommodation';
import { TouristSpot } from 'src/models/tourist-spot';
import { AccommodationService } from 'src/services/accommodation.service';
import { TouristSpotService } from 'src/services/tourist-spot.service';

@Component({
  selector: 'accommodation-management',
  templateUrl: './accommodation-management.component.html',
  styleUrls: ['./accommodation-management.component.css']
})
export class AccommodationManagementComponent implements OnInit {

  accommodations: Accommodation[]

  constructor(private accommodationService: AccommodationService, public addDialog: MatDialog) {
    this.accommodations = this.accommodationService.getAccommodations();
   }

  ngOnInit(): void {
  }

  addAccommodationAppear(): void{
    const dialogRef = this.addDialog.open(DialogAddAccommodation, {
      width: '300px',
      data: {accommodation: {}}
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.addAccommodation(result.accommodation)
    });
  }

  addAccommodation(accommodation:Accommodation): void{
    this.accommodations = this.accommodationService.addAccommodation(accommodation)
  }
  
  modifyAccommodation(accommodation:Accommodation): void{
    this.accommodations = this.accommodationService.modifyAccommodation(accommodation)
  }

  deleteAccommodation(Id:number): void{
    this.accommodations = this.accommodationService.deleteAccommodation(Id)
  }

}

export interface DialogAccommodationData{
  accommodation: Accommodation
}

@Component({
  selector: 'add-accommodation',
  templateUrl: 'add-accommodation.html'
})
export class DialogAddAccommodation {

  spots:TouristSpot[] = []
  selectedSpot: number
  enableButton: boolean
  selectedAv: string = ""
  actualImageLink: string = ""
  actualStars: Number
  actualFee: Number
  availabilityControl = new FormControl('', Validators.required);
  spotControl = new FormControl('', Validators.required);
  userControl = new FormControl('', Validators.required);
  adressControl = new FormControl('', Validators.required);
  imageUploaded: boolean

  constructor(
    public dialogRef: MatDialogRef<DialogAddAccommodation>,
    @Inject(MAT_DIALOG_DATA) public data: DialogAccommodationData,
    spotService: TouristSpotService) {
      this.spots = spotService.getAllSpots()
      this.data.accommodation = {Name:'', Address:'', Id:0, Images:[]
      ,Stars:1,Description:'', Fee:100, Total:0, SpotId:0, State:false,selectedImage:0}
      this.actualStars = 1
      this.actualFee = 100
      this.imageUploaded = false
    }

  onNoClick(): void {
    this.dialogRef.close();
  }

  notEmptyName = new FormControl('', [Validators.required]);
  notEmptyPassword = new FormControl('', [Validators.required]);

  getErrorMessageName() {
    if (this.notEmptyName.hasError('required')) {
      return 'You must enter a value';
    }
  }

  getErrorMessagePass() {
    if (this.notEmptyPassword.hasError('required')) {
      return 'You must enter a value';
    }
  }

  changeSpot() {
    if (this.selectedSpot == undefined) {
      this.enableButton = false;
    }else{
      this.data.accommodation.SpotId = this.selectedSpot
    }
  }

  changeAv() {
    if (this.selectedAv == undefined) {
      this.enableButton = false;
    }else{
      if (this.selectedAv == 'Yes'){
        this.data.accommodation.State = true
      } else{
        this.data.accommodation.State = false
      }
      
    }
  }

  addImage(){
    if(this.actualImageLink.trim().length == 0){
      alert('The image was empty, please, try again')
    }else{
      this.data.accommodation.Images.push(this.actualImageLink)
      this.imageUploaded = true
      this.actualImageLink = ''
      alert('The image was added, you can continue adding images')
    }  
  }

  changeStars(){
    this.data.accommodation.Stars = this.actualStars
  }

  changeFee(){
    this.data.accommodation.Fee = this.actualFee
  }
  
  buttonEnabled(){
    return this.buttonEnabled && this.availabilityControl.valid 
    && this.imageUploaded && this.spotControl.valid &&
    this.userControl.valid && this.adressControl.valid &&
    this.data.accommodation.Name.trim().length != 0 &&
    this.data.accommodation.Address.trim().length == 0
  }

}
