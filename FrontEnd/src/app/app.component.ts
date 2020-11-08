import { Component } from '@angular/core';
import { MenuType } from 'src/models/menu-type.enum';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'FrontEnd';

  menuType: MenuType;

  constructor() {
    this.menuType = 0
  }

  actualMenuType(recivedMenu: MenuType) {
    this.menuType = recivedMenu;
  }
}
