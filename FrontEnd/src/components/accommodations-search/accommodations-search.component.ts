import { Component, Inject, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute } from '@angular/router';
import { Accommodation } from 'src/models/accommodation';
import { PendingReservation } from 'src/models/pending-reservation';
import { Reservation } from 'src/models/reservation';
import { TouristSpot } from 'src/models/tourist-spot';
import { AccommodationService } from 'src/services/accommodation.service';
import { ReservationService } from 'src/services/reservation.service';
import { TouristSpotService } from 'src/services/tourist-spot.service';
import { AccommodationCommentsComponent } from '../accommodation-comments/accommodation-comments.component';
import { Comment } from 'src/models/comment';
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

  constructor(private fb: FormBuilder, private currentRoute: ActivatedRoute, private spotService: TouristSpotService, private reservationService: ReservationService,
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
    this.spotService.getSpotById(id).subscribe(
      res => {
        this.spot = res
      },
      err => {
        alert(`${err.status}: ${err.error}`);;
        console.log(err);
      }
    );
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
        this.accommodationService.getAccommodationByTouristSpot(this.spot.id).subscribe(
          res => {
            this.accommodations = res
            for (let accommodation of this.accommodations) {
              accommodation.selectedImage = 0
              this.calculateTotal(accommodation.id)
              this.calculateScore(accommodation)
            }
            this.hasSearched = !this.hasSearched

          },
          err => {
            alert(`${err.status}: ${err.error}`);;
            console.log(err);
          })


      } else {
        alert('The dates are incorrect');
      }
    } else {
      this.hasSearched = !this.hasSearched
      this.accommodations = []
    }
  }

  calculateScore(accommodation: Accommodation) {
    this.reservationService.getFromAccomodation(accommodation.id).subscribe(
      res => {
        accommodation.comments = res.filter(x => x.score > 0).map(function (x) {
          var comment: Comment
          comment = {
            name: x.name,
            surname: x.surname,
            score: x.score,
            text: x.comment,
          }
          return comment
        })

        accommodation.score = 0
        accommodation.comments.forEach(x => accommodation.score += x.score)
        accommodation.score = accommodation.score / accommodation.comments.length

      },
      err => {
        alert(`${err.status}: ${err.error}`);;
        console.log(err);
      })
  }

  calculateTotal(Id: number): void {
    this.accommodationService.calculateTotal(Id, this.startingDate, this.finishingDate,
      this.adultQuantity, this.retiredQuantity,
      this.childrenQuantity, this.babyQuantity).subscribe(
        res => {
          this.getAccommodationById(Id).total = res
        },
        err => {
          alert(`${err.status}: ${err.error}`);
        })
  }


  nextImage(accommodation: Accommodation) {
    accommodation.selectedImage = (accommodation.selectedImage + 1) % accommodation.images.length
  }

  getSelectedImage(accommodation: Accommodation) {
    return accommodation.selectedImage
  }

  getAccommodationById(id: number) {
    var accomodation: Accommodation
    for (var _i = 0; _i < this.accommodations.length; _i++) {
      if (this.accommodations[_i].id == id) {
        accomodation = this.accommodations[_i]
      }
    }
    return accomodation
  }

  getAccommodationComments(accommodation: Accommodation): void {
    const dialogRef = this.dialog.open(AccommodationCommentsComponent, {
      width: '300px',
      height: '500px',
      data: { comments: accommodation.comments, accommodationName: accommodation.name }
    });
  }

  openReservationDialog(accommodation: Accommodation): void {

    const dialogRef = this.dialog.open(MakeReservationDialog, {
      width: '250px',
      data: {
        reservation: {
          AccommodationId: accommodation.id,
          CheckIn: this.startingDate,
          CheckOut: this.finishingDate,
          BabyQuantity: this.babyQuantity,
          ChildrenQuantity: this.childrenQuantity,
          AdultQuantity: this.adultQuantity,
          RetiredQuantity: this.retiredQuantity,
          ReservationState: "Created",
          StateDescription: "",
        },
        telephone: accommodation.telephone,
        contactInfo: accommodation.contactInformation
      }
    });
  }
}

@Component({
  selector: 'make-reservation-dialog',
  templateUrl: 'make-reservation-dialog.html',
})
export class MakeReservationDialog {

  userControl = new FormControl('', Validators.required);
  surnameControl = new FormControl('', Validators.required);
  emailControl = new FormControl('', Validators.required);

  constructor(
    public dialogRef: MatDialogRef<MakeReservationDialog>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData, private reservationService: ReservationService, public dialog: MatDialog) { }

  onNoClick(): void {
    this.dialogRef.close();
  }

  onSubmit(reservation: PendingReservation) {
    this.dialogRef.close()
    var reservationNumber = -1

    this.reservationService.postReservation(this.data.reservation).subscribe(
      res => {
        reservationNumber = res
        this.openConfirmationDialog(reservationNumber, this.data.telephone, this.data.contactInfo)
      },
      err => {
        alert(`${err.status}: ${err.error}`);;
        console.log(err);
      }
    );
  }

  buttonEnabled() {
    return this.userControl.valid && this.data.reservation.Name.trim() != ''
      && this.surnameControl.valid && this.data.reservation.Surname.trim() != ''
      && this.emailControl.valid && this.data.reservation.Email.trim() != ''
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