import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { TouristSpotService } from 'src/services/tourist-spot.service';

@Injectable({
  providedIn: 'root'
})
export class SpotNotExistsGuard implements CanActivate {
  constructor(private router: Router, private spotService: TouristSpotService) {}
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      let id = +route.url[1].path;
      if (isNaN(id) || id < 1 || this.spotService.getSpotById(id)==null) {
        alert('This tourist spot does not exists');
        this.router.navigate(['/spot-search']);
        return false;
      }
      return true;
  }
  
}
