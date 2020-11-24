import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Component, EventEmitter, Inject, Input, OnInit, Output } from '@angular/core';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import { MenuType } from 'src/models/menu-type.enum';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AdministratorsService } from 'src/services/administrators.service';
import { User } from 'src/models/user';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { TouristSpotService } from 'src/services/tourist-spot.service';
import { TouristSpotDTO } from 'src/models/tourist-spot-dto';
import { Region } from 'src/models/region';
import { Category } from 'src/models/category';
import { RegionService } from 'src/services/region.service';
import { CategoryService } from 'src/services/category.service';
import { Router } from '@angular/router';
import { MatSliderModule } from '@angular/material/slider';
import { Importer } from 'src/models/importer';
import { Administrator } from 'src/models/administrator';
import { ReservationService } from 'src/services/reservation.service';
import { Reservation } from 'src/models/reservation';
import { ImportersService } from 'src/services/importers.service';


@Component({
  selector: 'app-tool-bar',
  templateUrl: './tool-bar.component.html',
  styleUrls: ['./tool-bar.component.css']
})
export class ToolBarComponent {

  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches),
      shareReplay()
    );

  userLoggued: Administrator;
  Email = new FormControl('')
  Password = new FormControl('')
  isLoggued = false;

  reservationNumber: number

  constructor(private breakpointObserver: BreakpointObserver, private administratorService: AdministratorsService,
    public addDialog: MatDialog, private reservationService: ReservationService, private spotService: TouristSpotService, public dialog: MatDialog, private router: Router) {
    this.loguedUser()
  }

  login($event): void {
    this.administratorService.login(this.Email.value, this.Password.value).subscribe(
      res => {
        localStorage.setItem('token', res);
        localStorage.setItem('email', this.Email.value);
        localStorage.setItem('password', this.Password.value);
        this.loguedUser()
      },
      err => {

        alert('Username or Password are incorrect, please, try again');
        console.log(err);
      }
    );
  }

  logout($event): void {
    this.administratorService.logout().subscribe(
      res => {
        //alert(res)
      },
      err => {
        alert('There was an unexpected error, please, try again');
        console.log(err);
      }
    );
    this.Email.setValue('')
    this.Password.setValue('')
    localStorage.clear();
    this.router.navigate(['/spot-search']);
    this.loguedUser()
    this.isLoggued = false
  }

  loguedUser(): void {
    this.userLoggued = {
      name: '', email: localStorage.getItem('email'),
      password: localStorage.getItem('password'), id: 0
    }
    this.isUserLoggued()
  }

  isUserLoggued() {
    const token = localStorage.token;
    this.isLoggued = (token != null && token !== undefined && token !== '');
  }

  addSpotAppear(): void {
    const dialogRef = this.addDialog.open(DialogAddSpot, {
      width: '250px',
      data: { spot: {} }
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.addSpot(result.spot)
    });
  }

  addSpot(spot: TouristSpotDTO): void {
    this.spotService.AddSpot(spot).subscribe(
      res => {
        //alert(res)
      },
      err => {
        alert('There was an unexpected error, please, try again');
        console.log(err);
      }
    )
  }

  openReservation() {
    var reservation: Reservation
    this.reservationService.getReservation(this.reservationNumber).subscribe(
      res => {
        reservation = res
        const dialogRef = this.dialog.open(ReservationDialog, {
          data: {
            id: reservation.id,
            state: reservation.reservationState,
            description: reservation.stateDescription,
            userComment: reservation.comment,
            score: reservation.score
          }
        });

        dialogRef.afterClosed().subscribe(result => {
          result;
        });
      },
      err => {
        alert(err.message);
        console.log(err);
      }
    );
  }
  
  getUserComment(reservation: Reservation, email: string): string {
    return reservation.comment
  }
;

  openReservationStateChange() {
    var reservation: Reservation
    this.reservationService.getReservation(this.reservationNumber).subscribe(
      res => {
        reservation = res
        const dialogRef = this.dialog.open(ReservationStateDialog, {
          data: {
            id: reservation.id,
            state: reservation.reservationState,
            description: reservation.stateDescription,
            userComment: '',
            score: reservation.score
          }
        });

        dialogRef.afterClosed().subscribe(result => {
          result;
        });
      },
      err => {
        alert(err.message);
        console.log(err);
      }
    );
  };

  showImporters() {
    const importerdialogRef = this.dialog.open(ImportersDialog, {
      data: {
        importers: [{ Name: "Xml", Id: 1 }, { Name: "Json", Id: 2 }]
      }
    });
  }
}

export interface DialogSpotData {
  spot: TouristSpotDTO
}

@Component({
  selector: 'add-spot',
  templateUrl: 'add-spot.html'
})
export class DialogAddSpot {

  regions: Region[];
  categories: Category[];
  spots: TouristSpotDTO[];
  userControl = new FormControl('', Validators.required);
  imageControl = new FormControl('', Validators.required);
  descriptionControl = new FormControl('', Validators.required);
  regionControl = new FormControl('', Validators.required);
  categoriesCount: number = 0

  constructor(
    public dialogRef: MatDialogRef<DialogAddSpot>,
    @Inject(MAT_DIALOG_DATA) public data: DialogSpotData,
    private breakpointObserver: BreakpointObserver, private regionService: RegionService, private categoryService: CategoryService, private spotService: TouristSpotService) {
    data.spot = { Id: 0, Name: "", Description: "", Image: "", Categories: [], Region: 0 }
    this.regionService.getRegions().subscribe(
      res => {
        this.regions = res;
      },
      err => {
        alert('There was an unexpected error, please, try again');
        console.log(err);
      }
    );
    this.categoryService.getCategories().subscribe(
      res => {
        this.categories = res;
      },
      err => {
        alert('There was an unexpected error, please, try again');
        console.log(err);
      }
    );
  }

  onCategoryClick(checked: Boolean, id: number) {
    if (checked) {
      this.data.spot.Categories.push(id)
      this.categoriesCount++
    }
    else {
      for (var i = 0; i < this.data.spot.Categories.length; i++) {
        if (this.data.spot.Categories[i] == id) {
          this.data.spot.Categories.splice(i);
          this.categoriesCount--
        }
      }
    }
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  buttonEnabled() {
    return this.userControl.valid && this.imageControl.valid && this.descriptionControl.valid &&
      this.regionControl.valid && this.data.spot.Name.trim().length != 0 &&
      this.data.spot.Image.trim().length != 0 && this.data.spot.Description.trim().length != 0 &&
      this.categoriesCount > 0
  }

}
export interface ReservationData {
  id: number
  state: string;
  description: string;
  userComment: string;
  score: number;
}

export interface ImportersData {
  importers: Importer[];
}

@Component({
  selector: 'reservation-state-dialog',
  templateUrl: 'reservation-state-dialog.html',
})
export class ReservationDialog {
  
  posibleStates: string[] = ['Creada', 'Pendiente_Pago', 'Aceptada','Rechazada','Expirada']
  comment: string;
  score: number = 5;
  state:string

  constructor(
    public dialogRef: MatDialogRef<ReservationDialog>,
    @Inject(MAT_DIALOG_DATA)
    public data: ReservationData,
    private reservationService: ReservationService,
  ) {
    this.state = this.posibleStates[this.data.state]
    this.score = this.data.score
   } 

  
  onSubmit(data: ReservationData) {

    this.reservationService.review(data.id, this.score, this.comment).subscribe(
      res => {
        alert(res)
      },
      err => {
        alert('There was an unexpected error, please, try again');
        console.log(err);
      }
    )
    this.dialogRef.close()

  }
}

@Component({
  selector: 'reservation-change-state-dialog',
  templateUrl: 'reservation-change-state-dialog.html',
})
export class ReservationStateDialog {
  
  posibleStates: string[] = ['Creada', 'Pendiente_Pago', 'Aceptada','Rechazada','Expirada']
  score: number = 5;
  descriptionControl = new FormControl('', Validators.required);
  stateControl = new FormControl('', Validators.required);
  
  constructor(
    public dialogRef: MatDialogRef<ReservationDialog>,
    @Inject(MAT_DIALOG_DATA)
    public data: ReservationData,
    private reservationService: ReservationService,
  ) { }

  onSubmit(data: ReservationData) {
    this.reservationService.changeState(data.id, this.posibleStates.indexOf(this.data.state), this.data.description).subscribe(
      res => {
        alert(res)
      },
      err => {
        alert('There was an unexpected error, please, try again');
        console.log(err);
      }
    )
    this.dialogRef.close()
  }
  
  onNoClick(): void {
    this.dialogRef.close();
  }

  buttonEnabled() {
    return this.descriptionControl.valid && this.data.description.trim().length != 0 &&
    this.stateControl.valid
  }
}


@Component({
  selector: 'importers-dialog',
  templateUrl: 'importers.html',
})
export class ImportersDialog {
  constructor(
    public dialogRef: MatDialogRef<ImportersDialog>,
    @Inject(MAT_DIALOG_DATA)
    public data: ImportersData,
    private importersService: ImportersService
  ) { }

  fileName: string


  onImport(id: number) {
    if (this.fileName != "") {
      this.importersService.import(id, this.fileName).subscribe(
        res => {
          alert(res)
        },
        err => {
          alert(err.message)
          console.log(err);
        }
      )
    }
    this.dialogRef.close();

  }

}



