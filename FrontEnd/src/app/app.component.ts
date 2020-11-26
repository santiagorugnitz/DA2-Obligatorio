import { Component } from '@angular/core';
import { NumberValueAccessor } from '@angular/forms';
import { MenuType } from 'src/models/menu-type.enum';
import { TouristSpot } from 'src/models/tourist-spot';
import { User } from 'src/models/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'FrontEnd';

  constructor() {

  }

}
