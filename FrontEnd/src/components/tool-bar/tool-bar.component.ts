import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import { MenuType } from 'src/models/menu-type.enum';
import { FormControl, FormGroup } from '@angular/forms';
import { AdministratorsService } from 'src/services/administrators.service';

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

    @Output() sendMenuType = new EventEmitter<MenuType>();
    menuType : MenuType;
    
    isLoggued:boolean;
    Username = new FormControl('')
    Password = new FormControl('')

    constructor(private breakpointObserver: BreakpointObserver, private administratorService: AdministratorsService) {
      this.isLoggued = false
      this.menuType = 0
    }
  
    login(): void {
      if  (this.administratorService.login(this.Username.value, this.Password.value))
      {
        this.isLoggued = true;
      }
    }
  
    logout($event): void {
      this.administratorService.logout(this.Username.value)
      this.Username.setValue('')
      this.Password.setValue('')
      this.isLoggued = false
      this.menuType = MenuType.SearchingMenu
      this.sendMenuType.emit(this.menuType);
    }
    
    updateMenu(menu: MenuType, $event) : void{
      this.menuType = menu;
      this.sendMenuType.emit(this.menuType);
    }

}
