import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSliderModule } from '@angular/material/slider';
import { LayoutModule } from '@angular/cdk/layout';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import {MatSelectModule} from '@angular/material/select';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { SpotSearchComponent } from '../components/spot-search/spot-search.component';
import { ToolBarComponent } from '../components/tool-bar/tool-bar.component';
import { AccommodationsSearchComponent} from '../components/accommodations-search/accommodations-search.component';
import {MatCheckboxModule} from '@angular/material/checkbox';
import {MatGridListModule} from '@angular/material/grid-list';
import {MatCardModule} from '@angular/material/card';
import {MatDatepickerModule} from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import {MatNativeDateModule} from '@angular/material/core';
import {platformBrowserDynamic} from '@angular/platform-browser-dynamic';
import {MAT_FORM_FIELD_DEFAULT_OPTIONS} from '@angular/material/form-field';
import {MatRippleModule} from '@angular/material/core';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import {MatDialogModule} from '@angular/material/dialog';
import {AccommodationCommentsComponent} from '../components/accommodation-comments/accommodation-comments.component';
import { DialogModifyUser, DialogAddUser,UsersManagementComponent } from '../components/users-management/users-management.component';
import { SpotReportComponent } from '../components/spot-report/spot-report.component'
import { AccommodationManagementComponent, DialogAddAccommodation } from 'src/components/accommodation-management/accommodation-management.component';
import { MatRadioButton } from '@angular/material/radio';

@NgModule({
  declarations: [
    AppComponent,
    SpotSearchComponent,
    ToolBarComponent,
    AccommodationsSearchComponent,
    AccommodationCommentsComponent,
    UsersManagementComponent,
    DialogAddUser,
    DialogModifyUser,
    SpotReportComponent,
    DialogAddAccommodation,
    AccommodationManagementComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MatSliderModule,
    MatCardModule,
    BrowserAnimationsModule,
    LayoutModule,
    MatToolbarModule,
    MatGridListModule,
    MatButtonModule,
    MatSelectModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    MatCheckboxModule,
    CommonModule,
    ReactiveFormsModule,
    MatDatepickerModule,
    MatInputModule,
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    MatNativeDateModule,
    ReactiveFormsModule,
    MatRippleModule,
    MatSnackBarModule,
    MatDialogModule,
  ],
  providers: [{ provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: { appearance: 'fill' } },],
  bootstrap: [AppComponent]
})
export class AppModule { }
