import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Component, Inject,EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import { FormControl, FormGroup, FormBuilder } from '@angular/forms';
import { AdministratorsService } from 'src/services/administrators.service';
import { User } from 'src/models/user';
import { MatDialog,MatDialogRef,MAT_DIALOG_DATA } from '@angular/material/dialog';


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

    constructor(private breakpointObserver: BreakpointObserver, private administratorService: AdministratorsService, public dialog:MatDialog) {
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

