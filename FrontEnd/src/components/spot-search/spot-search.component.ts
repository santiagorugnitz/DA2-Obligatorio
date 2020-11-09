import { Component, EventEmitter, Output} from '@angular/core';
import { Region } from 'src/models/region';
import { RegionService } from 'src/services/region.service';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import { FormControl, FormGroup } from '@angular/forms';
import { Category } from 'src/models/category';
import { CategoryService } from 'src/services/category.service';
import { TouristSpot } from 'src/models/tourist-spot';

@Component({
  selector: 'app-spot-search',
  templateUrl: './spot-search.component.html',
  styleUrls: ['./spot-search.component.css']
})
export class SpotSearchComponent {

  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches),
      shareReplay()
    );
    
    
  regions: Region[];
  categories: Category[];
  @Output() sendSpot = new EventEmitter<TouristSpot>();
    spot : TouristSpot;
  
  constructor(private breakpointObserver: BreakpointObserver, private regionService: RegionService, private categoryService: CategoryService) { 
    this.regions = regionService.getRegions()
    this.categories = categoryService.getCategories()
    this.spot = {Id:1,Name:"Montevideo",Description:"Capital de Uruguay", Image:"Nueva"}
  }

  searchAccomodations(sendingSpot:TouristSpot, $event){
    //this.spot = sendingSpot
    this.sendSpot.emit(this.spot);
  }

}
