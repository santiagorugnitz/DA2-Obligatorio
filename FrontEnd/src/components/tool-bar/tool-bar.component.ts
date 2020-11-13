import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Component, EventEmitter, Inject, Input, OnInit, Output } from '@angular/core';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import { MenuType } from 'src/models/menu-type.enum';
import { FormControl, FormGroup } from '@angular/forms';
import { AdministratorsService } from 'src/services/administrators.service';
import { User } from 'src/models/user';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { TouristSpotService } from 'src/services/tourist-spot.service';
import { TouristSpot } from 'src/models/tourist-spot';
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

    constructor(private breakpointObserver: BreakpointObserver, private administratorService: AdministratorsService,
      public addDialog: MatDialog, private spotService: TouristSpotService) {
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
  
    addSpot(spot:TouristSpot): void{
      this.spotService.AddSpot(spot)
    }

}

export interface DialogSpotData{
  spot: TouristSpot
}

@Component({
  selector: 'add-spot',
  templateUrl: 'add-spot.html'
})
export class DialogAddSpot {

  regions: Region[];
  categories: Category[];
  spots: TouristSpot[];

  constructor(
    public dialogRef: MatDialogRef<DialogAddSpot>,
    @Inject(MAT_DIALOG_DATA) public data: DialogSpotData,
    private breakpointObserver: BreakpointObserver, private regionService: RegionService, private categoryService: CategoryService, private spotService: TouristSpotService) {
      data.spot = {Id:0,Name:"",Description:"",Image:"https://montevideo.gub.uy/sites/default/files/styles/noticias_twitter/public/biblioteca/dsc0263_4.jpg?itok=am2Xii7V", Categories:[], Region:0}
      this.regions = regionService.getRegions()
      this.categories = categoryService.getCategories()
    }

    onCategoryClick(checked: Boolean, id: number) {
      if (checked) {
        this.data.spot.Categories.push(id)
      }
      else {
        for (var i = 0; i < this.data.spot.Categories.length; i++) {
          if (this.data.spot.Categories[i] === id) {
            this.data.spot.Categories.splice(i);
          }
        }
      }
    }

  onNoClick(): void {
    this.dialogRef.close();
  }

}