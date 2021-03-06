import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatRadioModule } from '@angular/material/radio';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatNativeDateModule, MatRippleModule } from '@angular/material/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import {MatTableModule} from '@angular/material/table';

import { UsersManagementComponent } from './users-management.component';

describe('UsersManagementComponent', () => {
  let component: UsersManagementComponent;
  let fixture: ComponentFixture<UsersManagementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [UsersManagementComponent],
      imports: [
        NoopAnimationsModule,
        ReactiveFormsModule,
        FormsModule,
        MatButtonModule,
        MatCardModule,
        MatInputModule,
        MatRadioModule,
        MatSelectModule,
        MatDatepickerModule,
        MatNativeDateModule,
        MatRippleModule,
        MatSnackBar,
        MatDialog,
        MatTableModule
      ]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UsersManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should compile', () => {
    expect(component).toBeTruthy();
  });
});
