import { Component, OnInit, Input } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import { RegionService } from '../services/region.service';
import { Region } from '../models/region';
import { MenuType } from '../models/menu-type.enum';
import { FormControl, FormGroup } from '@angular/forms';
import { AdministratorsService } from '../services/administrators.service';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent {

  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches),
      shareReplay()
    );

    menuType: MenuType;
    regions: Region[];
    isLoggued:boolean;
    username: string;
    Username = new FormControl('')
    Password = new FormControl('')
    
  constructor(private breakpointObserver: BreakpointObserver, private regionService: RegionService, private administratorService: AdministratorsService) {
    this.regions = regionService.getRegions()
    this.menuType = MenuType.SearchingMenu
    this.isLoggued = false
    this.username = ''
  }

  updateMenu(menu: MenuType) : void{
    this.menuType = menu;
  }

  login(): void {
    if  (this.administratorService.login(this.Username.value, this.Password.value))
    {
      this.isLoggued = true;
    }
  }

  logout(): void {
    this.administratorService.logout(this.Username.value)
    this.Username.setValue('')
    this.Password.setValue('')
    this.isLoggued = false
    this.menuType = MenuType.SearchingMenu
  }
}
