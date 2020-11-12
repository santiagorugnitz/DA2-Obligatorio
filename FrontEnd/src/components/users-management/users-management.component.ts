import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Administrator } from 'src/models/administrator';
import { AdministratorsService } from 'src/services/administrators.service';

@Component({
  selector: 'app-users-management',
  templateUrl: './users-management.component.html',
  styleUrls: ['./users-management.component.css']
})
export class UsersManagementComponent implements OnInit {

  admins:Administrator[] = []
  
  constructor(private administratorsService: AdministratorsService, public addDialog: MatDialog, public modifyDialog: MatDialog) {
    this.admins = this.administratorsService.getAdministrators();
   }

  ngOnInit(): void {
  }

  addUserAppear(): void{
    const dialogRef = this.addDialog.open(DialogAddUser, {
      width: '250px',
      data: {Id: 0, Name:'', Password:''}
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.addUser(result.Name, result.Password)
    });
  }

  addUser(Name:string, Password:string): void{
    this.admins = this.administratorsService.addUser(Name, Password)
  }

  modifyUserAppear(Id: number, Name:string, Password:string){
    const dialogRef = this.addDialog.open(DialogModifyUser, {
      width: '250px',
      data: {Id: Id, Name:Name, Password:Password}
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.modifyUser(result.Id, result.Name, result.Password)
    });
  }
  
  modifyUser(Id:number, Name:string, Password:string): void{
    this.admins = this.administratorsService.modifyUser(Id, Name, Password)
  }

  deleteUser(Id:number): void{
    this.admins = this.administratorsService.deleteUser(Id)
  }

}

export interface DialogUserData{
  Id:number,
  Name:string,
  Password:string
}

@Component({
  selector: 'add-user',
  templateUrl: 'add-user.html',
})
export class DialogAddUser {

  constructor(
    public dialogRef: MatDialogRef<DialogAddUser>,
    @Inject(MAT_DIALOG_DATA) public data: DialogUserData) {}

  onNoClick(): void {
    this.dialogRef.close();
  }

  notEmptyUser = new FormControl('', [Validators.required]);
  notEmptyPassword = new FormControl('', [Validators.required]);

  getErrorMessageUser() {
    if (this.notEmptyUser.hasError('required')) {
      return 'You must enter a value';
    }
  }

  getErrorMessagePass() {
    if (this.notEmptyPassword.hasError('required')) {
      return 'You must enter a value';
    }
  }

}

@Component({
  selector: 'modify-user',
  templateUrl: 'modify-user.html',
})
export class DialogModifyUser {

  constructor(
    public dialogRef: MatDialogRef<DialogModifyUser>,
    @Inject(MAT_DIALOG_DATA) public data: DialogUserData) {}

  onNoClick(): void {
    this.dialogRef.close();
  }

  notEmptyUser = new FormControl('', [Validators.required]);
  notEmptyPassword = new FormControl('', [Validators.required]);

  getErrorMessageUser() {
    if (this.notEmptyUser.hasError('required')) {
      return 'You must enter a value';
    }
  }

  getErrorMessagePass() {
    if (this.notEmptyPassword.hasError('required')) {
      return 'You must enter a value';
    }
  }

}