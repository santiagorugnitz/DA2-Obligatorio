import { Component, OnInit } from '@angular/core';
import { Region } from 'src/models/region';
import { RegionService } from 'src/services/region.service';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import { FormControl, FormGroup } from '@angular/forms';
import { Category } from 'src/models/category';
import { CategoryService } from 'src/services/category.service';
import { TouristSpot} from 'src/models/tourist-spot';
import { TouristSpotService } from 'src/services/tourist-spot.service';
import { Administrator } from 'src/models/administrator';
import { AdministratorsService } from 'src/services/administrators.service';

@Component({
  selector: 'app-spot-search',
  templateUrl: './spot-search.component.html',
  styleUrls: ['./spot-search.component.css']
})
export class SpotSearchComponent implements OnInit {

  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches),
      shareReplay()
    );

  regions: Region[];
  categories: Category[];
  spots: TouristSpot[];

  selectedCategories: Number[] = []
  selectedRegion: Number


  constructor(private breakpointObserver: BreakpointObserver, private administratorService: AdministratorsService,
    private regionService: RegionService, private categoryService: CategoryService, private spotService: TouristSpotService) {
    this.getSpots()
  }

  ngOnInit(): void {
    this.regionService.getRegions().subscribe(
      res => {
        this.regions = res;
      },
      err => {
        alert('There was an unexpected error, please, try again');
        console.log(err);
      }
    );

    this.categoryService.getCategories().subscribe(
      res => {
        this.categories = res;
      },
      err => {
        alert('There was an unexpected error, please, try again');
        console.log(err);
      }
    );
  }

  userLoggued():boolean{
    const token = localStorage.token;
    return (token != null && token !== undefined && token !== '');
  }

  onCategoryClick(checked: Boolean, id: Number) {
    if (checked) {
      this.selectedCategories.push(id)
    }
    else {
      for (var i = 0; i < this.selectedCategories.length; i++) {
        if (this.selectedCategories[i] === id) {
          this.selectedCategories.splice(i);
        }
      }
    }
    this.getSpots()
  }

  getSpots() {
    if (this.selectedRegion == undefined) return
    this.spotService.getSpots(this.selectedRegion, this.selectedCategories).subscribe(
      res => {
        this.spots= res;
      },
      err => {
        alert('There was an unexpected error, please, try again');
        console.log(err);
      }
    );
  
  }

}
