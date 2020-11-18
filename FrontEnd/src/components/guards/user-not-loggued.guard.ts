import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AdministratorsService } from 'src/services/administrators.service';

@Injectable({
  providedIn: 'root'
})
export class UserNotLogguedGuard implements CanActivate {
  constructor(private router: Router, private administratorService: AdministratorsService) {}
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      if (!this.administratorService.isLogued().isLoggued) {
        alert('You have to be logged to enter that feature');
        this.router.navigate(['/spot-search']);
        return false;
      }
      return true;
  }
  
}
