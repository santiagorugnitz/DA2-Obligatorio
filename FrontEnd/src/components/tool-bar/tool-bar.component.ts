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


@Component({
  selector: 'app-tool-bar',
  templateUrl: './tool-bar.component.html',
  styleUrls: ['./tool-bar.component.css']
})
export class ToolBarComponent  {

  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches),
      shareReplay()
    );
    
    
    @Output() sentUser = new EventEmitter<User>()
    @Input() userLoggued: User;
    Username = new FormControl('')
    Password = new FormControl('')

    reservationNumber = ""

    constructor(private breakpointObserver: BreakpointObserver, private administratorService: AdministratorsService,
      public addDialog: MatDialog, private spotService: TouristSpotService,public dialog:MatDialog) {
    }
  
    login($event): void {
      if  (this.administratorService.login(this.Username.value, this.Password.value))
      {
        this.userLoggued.Name = this.Username.value;
        this.userLoggued.Password = this.Password.value;
        this.userLoggued.isLoggued = true;
        this.sentUser.emit(this.userLoggued);
      }
    }
  
    logout($event): void {
      this.administratorService.logout(this.Username.value)
      this.Username.setValue('')
      this.Password.setValue('')
      this.userLoggued.Name = this.Username.value;
        this.userLoggued.Password = this.Password.value;
        this.userLoggued.isLoggued = false;
      this.sentUser.emit(this.userLoggued);
    }

    addSpotAppear(): void{
      const dialogRef = this.addDialog.open(DialogAddSpot, {
        width: '250px',
        data: {spot: {}}
      });
  
      dialogRef.afterClosed().subscribe(result => {
        console.log('The dialog was closed');
        this.addSpot(result.spot)
      });
    }
  
    addSpot(spot:TouristSpotDTO): void{
      this.spotService.AddSpot(spot)
    }

    openReservation(){
      const dialogRef = this.dialog.open(ReservationDialog,{
        data:{
          state:"Procesada",
          description:"xd",
          comment:"comentario",
          noComment:false,
          score:1,
        }
      });

      dialogRef.afterClosed().subscribe(result => {
        result;
      });
    };
}

export interface DialogSpotData{
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
      data.spot = {Id:0,Name:"",Description:"",Image:"", Categories:[], Region:0}
      this.regions = regionService.getRegions()
      this.categories = categoryService.getCategories()
    }

    onCategoryClick(checked: Boolean, id: number) {
      if (checked) {
        this.data.spot.Categories.push(id)
        this.categoriesCount ++
      }
      else {
        for (var i = 0; i < this.data.spot.Categories.length; i++) {
          if (this.data.spot.Categories[i] == id) {
            this.data.spot.Categories.splice(i);
            this.categoriesCount --
          }
        }
      }
    }

  onNoClick(): void {
    this.dialogRef.close();
  }

  buttonEnabled(){
    return this.userControl.valid && this.imageControl.valid && this.descriptionControl.valid &&
    this.regionControl.valid && this.data.spot.Name.trim().length != 0 && 
    this.data.spot.Image.trim().length != 0 && this.data.spot.Description.trim().length != 0 && 
    this.categoriesCount > 0 
  }

}
export interface ReservationData {
  state: string;
  description: string;
  noComment:Boolean;
  comment:string;
  score:number;
}

@Component({
  selector: 'reservation-state-dialog',
  templateUrl: 'reservation-state-dialog.html',
})
export class ReservationDialog {
  constructor(
    public dialogRef: MatDialogRef<ReservationDialog>,
    @Inject(MAT_DIALOG_DATA) 
    public data: ReservationData) {}

  comment:string;
  onSubmit( data:ReservationData){
    data
      //servicio y pum

    this.dialogRef.close()

  }

  decreaseScore(){
    this.data.score--
  }
  
  increaseScore(){
    this.data.score++
  }
}

