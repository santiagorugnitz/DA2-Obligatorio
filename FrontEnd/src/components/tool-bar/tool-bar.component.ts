import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import { MenuType } from 'src/models/menu-type.enum';
import { FormControl, FormGroup } from '@angular/forms';
import { AdministratorsService } from 'src/services/administrators.service';
import { User } from 'src/models/user';

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
    
    @Output() sentUser = new EventEmitter<User>()
    @Input() userLoggued: User;
    Username = new FormControl('')
    Password = new FormControl('')

    constructor(private breakpointObserver: BreakpointObserver, private administratorService: AdministratorsService) {
    }
  
    login($event): void {
      if  (this.administratorService.login(this.Username.value, this.Password.value))
      {
        this.userLoggued.Name = this.Username.value;
        this.userLoggued.Password = this.Password.value;
        this.userLoggued.isLoggued = true;
        this.sentUser.emit(this.userLoggued);
      }
    }
  
    logout($event): void {
      this.administratorService.logout(this.Username.value)
      this.Username.setValue('')
      this.Password.setValue('')
      this.userLoggued.Name = this.Username.value;
        this.userLoggued.Password = this.Password.value;
        this.userLoggued.isLoggued = false;
      this.sentUser.emit(this.userLoggued);
    }

}
