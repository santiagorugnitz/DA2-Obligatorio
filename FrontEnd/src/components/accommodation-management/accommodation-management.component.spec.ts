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

import { AccommodationManagementComponent } from './accommodation-management.component';

describe('AccommodationManagementComponent', () => {
  let component: AccommodationManagementComponent;
  let fixture: ComponentFixture<AccommodationManagementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [AccommodationManagementComponent],
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
        MatDialog
      ]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AccommodationManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should compile', () => {
    expect(component).toBeTruthy();
  });
});