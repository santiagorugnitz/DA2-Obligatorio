import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Comment } from 'src/models/comment' ;

export interface DialogData {
  comments: Comment[];
  accommodationName: string;
}

@Component({
  selector: 'app-accommodation-comments',
  templateUrl: './accommodation-comments.component.html',
  styleUrls: ['./accommodation-comments.component.css']
})
export class AccommodationCommentsComponent {

  comments:Comment[] = []
  
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