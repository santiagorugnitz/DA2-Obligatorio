import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatTable } from '@angular/material/table';
import { Accommodation, AccommodationDTO } from 'src/models/accommodation';
import { TouristSpot } from 'src/models/tourist-spot';
import { AccommodationService } from 'src/services/accommodation.service';
import { TouristSpotService } from 'src/services/tourist-spot.service';
import { SpotNotExistsGuard } from '../guards/spot-not-exists.guard';

@Component({
  selector: 'accommodation-management',
  templateUrl: './accommodation-management.component.html',
  styleUrls: ['./accommodation-management.component.css']
})
export class AccommodationManagementComponent implements OnInit {

  accommodations: Accommodation[]
  displayedColumns: string[] = ['spot', 'name', 'isAvailable', 'modify', 'delete'];
  @ViewChild('table') table: MatTable<Accommodation>;

  constructor(private accommodationService: AccommodationService, public addDialog: MatDialog) {
  }

  ngOnInit(): void {
    this.getAccommodations()
    this.update()
  }

  getAccommodations(){
    this.accommodationService.getAccommodations().subscribe(
      res => {
        this.accommodations = res
      },
      err => {
        alert(err.message);
        console.log(err);
      })
  }

  addAccommodationAppear(): void {
    const dialogRef = this.addDialog.open(DialogAddAccommodation, {
      width: '300px',
      data: { accommodation: {} }
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.addAccommodation(result.accommodation)
    });
  }

  addAccommodation(accommodation: AccommodationDTO): void {
    this.accommodationService.addAccommodation(accommodation).subscribe(
      res => {
        alert(res)
        this.getAccommodations()
        this.update()
      },
      err => {
        alert(err.message);
        console.log(err);
      })

  
  }

  changeAvailability(accommodation: Accommodation): void {
    this.accommodationService.changeAvailability(accommodation.id, !accommodation.available).subscribe(
    res => {
      alert(res)
      this.getAccommodations()
      this.update()
    },
    err => {
      alert(err.message);
      console.log(err);
    })

  }

  deleteAccommodation(Id: number): void {
    this.accommodationService.deleteAccommodation(Id).subscribe(
    res => {
      alert(res)
      this.getAccommodations()
    this.update()
    },
    err => {
      alert(err.message);
      console.log(err);
    })
    
    
  }

  update() {
    let data: Accommodation[] = [];
    if (this.table.dataSource) {
      data = (this.table.dataSource as Accommodation[]);
    }
    data = this.accommodations
    this.table.dataSource = data;
    this.table.renderRows()
  }

}

export interface DialogAccommodationData {
  accommodation: AccommodationDTO
}

@Component({
  selector: 'add-accommodation',
  templateUrl: 'add-accommodation.html'
})
export class DialogAddAccommodation {

  spots: TouristSpot[] = []
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
    spotService.getAllSpots().subscribe(
      res => {
        this.spots = res
      },
      err => {
        alert(err.message);
        console.log(err);
      }
    );
    this.data.accommodation = {
      name: '',
      address: '',
      imageNames: [],
      stars: 1,
      description: '',
      fee: 100,
      touristSpotId: 0,
      available: false,
      telephone: '',
      contactInformation: ''
    }
    this.actualStars = 1
    this.actualFee = 100
    this.imageUploaded = false
  }

  ngOnInit(): void {

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

  getSpotName(id: Number) {
    this.spots.forEach(element => {
      if (element.id == id) return element.name
    });
    return ""
  }

  getErrorMessagePass() {
    if (this.notEmptyPassword.hasError('required')) {
      return 'You must enter a value';
    }
  }

  changeSpot() {
    if (this.selectedSpot == undefined) {
      this.enableButton = false;
    } else {
      this.data.accommodation.touristSpotId = this.selectedSpot
    }
  }

  changeAv() {
    if (this.selectedAv == undefined) {
      this.enableButton = false;
    } else {
      if (this.selectedAv == 'Yes') {
        this.data.accommodation.available = true
      } else {
        this.data.accommodation.available = false
      }

    }
  }

  addImage() {
    if (this.actualImageLink.trim().length == 0) {
      alert('The image was empty, please, try again')
    } else {
      this.data.accommodation.imageNames.push(this.actualImageLink)
      this.imageUploaded = true
      this.actualImageLink = ''
      alert('The image was added, you can continue adding images')
    }
  }

  changeStars() {
    this.data.accommodation.stars = this.actualStars
  }

  changeFee() {
    this.data.accommodation.fee = this.actualFee
  }

  buttonEnabled() {

    var xd = true;
    xd = xd && this.availabilityControl.valid;
    xd = xd && this.imageUploaded
    xd = xd && this.spotControl.valid
    xd = xd && this.userControl.valid
    xd = xd &&  this.adressControl.valid
    xd = xd && this.data.accommodation.name.trim().length != 0
    xd = xd && this.data.accommodation.address.trim().length != 0

    return xd
      
  }

}
