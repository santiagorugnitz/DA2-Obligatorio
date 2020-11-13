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
      width: '250px',
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
  actualImageLink: string

  constructor(
    public dialogRef: MatDialogRef<DialogAddAccommodation>,
    @Inject(MAT_DIALOG_DATA) public data: DialogAccommodationData,
    spotService: TouristSpotService) {
      this.spots = spotService.getAllSpots()
      this.data.accommodation = {Name:'', Adress:'', Id:0, Images:[]
      ,Stars:0,Description:'', Fee:0, Total:0, SpotId:0, State:false}
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
    this.data.accommodation.Images.push(this.actualImageLink)
  }


}
