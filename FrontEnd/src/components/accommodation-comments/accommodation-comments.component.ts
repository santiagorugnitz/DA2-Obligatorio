import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

export interface DialogData {
  comments: string[];
  accommodationName: string;
}

@Component({
  selector: 'app-accommodation-comments',
  templateUrl: './accommodation-comments.component.html',
  styleUrls: ['./accommodation-comments.component.css']
})
export class AccommodationCommentsComponent {

  comments:string[] = []
  
  constructor(
    public dialogRef: MatDialogRef<AccommodationCommentsComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData) 
    {
      this.comments = data.comments
    }

  onCloseClick(): void {
    this.dialogRef.close();
  }

}