import { Component } from '@angular/core';
import { NumberValueAccessor } from '@angular/forms';
import { MenuType } from 'src/models/menu-type.enum';
import { TouristSpot } from 'src/models/tourist-spot';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'FrontEnd';

  menuType: MenuType;
  spot: TouristSpot;

  constructor() {
    this.menuType = 0
    this.spot = new TouristSpot
  }

  actualMenuType(recivedMenu: MenuType) {
    this.menuType = recivedMenu;
  }

  getSpot(receivedSpot: TouristSpot) {
    this.spot = receivedSpot;
    this.menuType = MenuType.AccomodationSearch
  }
}
