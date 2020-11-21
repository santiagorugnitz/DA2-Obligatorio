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

  reservationNumber :number

  constructor(private breakpointObserver: BreakpointObserver, private administratorService: AdministratorsService,
    public addDialog: MatDialog,private reservationService:ReservationService, private spotService: TouristSpotService, public dialog: MatDialog, private router: Router) {
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
    this.administratorService.logout(localStorage.getItem('email'))
    this.Email.setValue('')
    this.Password.setValue('')
    localStorage.clear();
    this.router.navigate(['/spot-search']);
    this.loguedUser()
    this.isLoggued = false
  }

  loguedUser(): void {
    this.userLoggued = {Name:'', Email:localStorage.getItem('email'), 
    Password:localStorage.getItem('password'), Id:0}
    this.isUserLoggued()
  }

  isUserLoggued(){
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
        alert("Yay")
      },
      err =>{
        alert('There was an unexpected error, please, try again');
        console.log(err);
      }
    )
  }

  openReservation() {

    var reservation: Reservation

    this.reservationService.getReservation(this.reservationNumber).subscribe(
      res => {
        reservation= res
      },
      err => {
        alert('There was an unexpected error, please, try again');
        console.log(err);
      }
    );


    const dialogRef = this.dialog.open(ReservationDialog, {
      data: {
        id:reservation.id,
        state: reservation.reservationState,
        description: reservation.stateDescription,
        noComment: reservation.score==0,
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      result;
    });
  };


  showImporters() {
    const importerdialogRef = this.dialog.open(ImportersDialog, {
      data: {
        importers: [{ Name: "Xml", Id: 1 }]
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
  id:number
  state: string;
  description: string;
  noComment: Boolean;
}

export interface ImportersData {
  importers: Importer[];
}

@Component({
  selector: 'reservation-state-dialog',
  templateUrl: 'reservation-state-dialog.html',
})
export class ReservationDialog {
  constructor(
    public dialogRef: MatDialogRef<ReservationDialog>,
    @Inject(MAT_DIALOG_DATA)
    public data: ReservationData,
    private reservationService: ReservationService
    ) { }

  comment: string;
  score: number = 5;

  onSubmit(data: ReservationData) {
    
    this.reservationService.review(data.id,this.comment,this.score)
    this.dialogRef.close()

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
    public data: ImportersData) { }

  fileName = ""

  onFileChange(evt): void {

    if (evt.target.files[0].name == undefined) {
      this.fileName = ""
    }
    else {
      this.fileName = evt.target.files[0].name;
    }
  }

  onImport(id: number) {
    if (this.fileName != "") {
      //servicio
    }
    this.dialogRef.close();

  }

}



