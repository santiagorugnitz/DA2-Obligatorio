import { Component, Inject, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
import { dateInputsHaveChanged } from '@angular/material/datepicker/datepicker-input-base';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute } from '@angular/router';
import { Accommodation } from 'src/models/accommodation';
import { PendingReservation } from 'src/models/pending-reservation';
import { SelectedImage } from 'src/models/selected-image';
import { TouristSpot } from 'src/models/tourist-spot';
import { AccommodationService } from 'src/services/accommodation.service';
import { ReservationService } from 'src/services/reservation.service';
import { SpotService } from 'src/services/spot.service';
import { AccommodationCommentsComponent } from '../accommodation-comments/accommodation-comments.component';

export interface DialogData {
  reservation: PendingReservation;
  telephone: string
  contactInfo: string
}

export interface ConfirmationData {
  reservationNumber: number
  telephone: string
  contactInfo: string
}

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
    private accommodationService: AccommodationService, private _snackBar: MatSnackBar, public dialog: MatDialog) {
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

  changeStartingDate(event: MatDatepickerInputEvent<Date>) {
    this.startingDate = event.value
  }

  changeFinishingDate(event: MatDatepickerInputEvent<Date>) {
    this.finishingDate = event.value
  }

  addAdult() {
    this.adultQuantity++;
  }

  addRetired() {
    this.retiredQuantity++;
  }

  addChild() {
    this.childrenQuantity++;
  }

  addBaby() {
    this.babyQuantity++;
  }

  removeAdult() {
    if ((this.adultQuantity > 1 || this.retiredQuantity >= 1) && this.adultQuantity > 0) {
      this.adultQuantity--;
    }
  }

  removeRetired() {
    if ((this.adultQuantity >= 1 || this.retiredQuantity > 1) && this.retiredQuantity > 0) {
      this.retiredQuantity--;
    }
  }

  removeChild() {
    if (this.childrenQuantity > 0) {
      this.childrenQuantity--;
    }
  }

  removeBaby() {
    if (this.babyQuantity > 0) {
      this.babyQuantity--;
    }
  }

  search() {
    if (!this.hasSearched) {
      if (this.startingDate >= new Date() && this.startingDate < this.finishingDate) {
        this.accommodations = this.accommodationService.getAccommodationByTouristSpot(this.spot.Id)
        this.selectedImages = []
        for (var _i = 0; _i < this.accommodations.length; _i++) {
          this.selectedImages.push({ accommodationId: this.accommodations[_i].Id, imageNumber: 0 })
        }
        for (let accommodation of this.accommodations) {
          this.calculateTotal(accommodation.Id)
        }
        this.hasSearched = !this.hasSearched
      } else {
        alert('The dates are incorrect');
      }
    } else {
      this.hasSearched = !this.hasSearched
      this.accommodations = []
      this.selectedImages = []
    }
  }

  calculateTotal(Id: number): void {

    this.getAccommodationById(Id).Total = this.accommodationService.calculateTotal(
      Id, this.startingDate, this.finishingDate,
      this.adultQuantity, this.retiredQuantity,
      this.childrenQuantity, this.babyQuantity)
  }

  getSelectedImage(accomodationId: number) {
    var selectedImage: number
    for (let image of this.selectedImages) {
      if (image.accommodationId == accomodationId) {
        selectedImage = image.imageNumber
      }
    }
    return selectedImage
  }

  nextImage(accomodationId: number) {
    if (this.moreImagesLeft(accomodationId)) {
      for (let image of this.selectedImages) {
        if (image.accommodationId == accomodationId) {
          image.imageNumber++
        }
      }
    } else {
      alert('There is no more images')
    }
  }

  moreImagesLeft(accomodationId: number): boolean {
    var selectedImage: number
    for (let image of this.selectedImages) {
      if (image.accommodationId == accomodationId) {
        selectedImage = image.imageNumber
      }
    }
    return selectedImage < this.getAccommodationById(accomodationId).Images.length - 1
  }

  getAccommodationById(id: number) {
    var accomodation: Accommodation
    for (var _i = 0; _i < this.accommodations.length; _i++) {
      if (this.accommodations[_i].Id == id) {
        accomodation = this.accommodations[_i]
      }
    }
    return accomodation
  }

  getAccommodationComments(id: number, name: string): void {
    const commentList = this.accommodationService.getAccommodationComments(id)
    const dialogRef = this.dialog.open(AccommodationCommentsComponent, {
      width: '350px',
      data: { comments: commentList, accommodationName: name }
    });
  }

  openReservationDialog(id: number): void {

    const dialogRef = this.dialog.open(MakeReservationDialog, {
      width: '250px',
      data: {
        reservation: {
          AccommodationId: id,
          CheckIn: this.startingDate,
          CheckOut: this.finishingDate,
          BabyQty: this.babyQuantity,
          ChildrenQty: this.childrenQuantity,
          AdultQty: this.adultQuantity,
          RetiredQty: this.retiredQuantity,
        },
        telephone: "+59812343",
        contactInfo:"Thank you for your reservation, see you soon!"
      }
    });
  }
}

@Component({
  selector: 'make-reservation-dialog',
  templateUrl: 'make-reservation-dialog.html',
})
export class MakeReservationDialog {

  constructor(
    public dialogRef: MatDialogRef<MakeReservationDialog>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData, private reservationService: ReservationService, public dialog: MatDialog) { }

  onNoClick(): void {
    this.dialogRef.close();
  }

  onSubmit(reservation: PendingReservation) {
    //servicio mensaje y pum
    this.dialogRef.close()

    var reservationNumber = this.reservationService.postReservation(this.data.reservation)

    if (reservationNumber > 0) {
      this.openConfirmationDialog(reservationNumber, this.data.telephone, this.data.contactInfo)
    }
    else {
      //TODO: tirar un toast con un error o capaz que hay un errorhandler re copado y esto no se usa
    }

  }

  openConfirmationDialog(id: number, tel: string, info: string) {
    const dialogRef = this.dialog.open(ReservationConfirmationDialog, {
      width: '350px',
      data: {
        reservationNumber: id,
        telephone: tel,
        contactInfo: info
      }
    });
  }

};

@Component({
  selector: 'reservation-confirmation-dialog',
  templateUrl: 'reservation-confirmation-dialog.html',
})
export class ReservationConfirmationDialog {

  constructor(
    public dialogRef: MatDialogRef<ReservationConfirmationDialog>,
    @Inject(MAT_DIALOG_DATA) public data: ConfirmationData) { }

  onNoClick(): void {
    this.dialogRef.close();
  }


};