import { Component, OnInit } from '@angular/core';
import { Region } from 'src/app/models/region';
import { RegionService } from 'src/app/services/region.service';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import { FormControl, FormGroup } from '@angular/forms';
import { Category } from 'src/app/models/category';
import { CategoryService } from 'src/app/services/category.service';

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
  
  constructor(private breakpointObserver: BreakpointObserver, private regionService: RegionService, private categoryService: CategoryService) { 
    this.regions = regionService.getRegions()
    this.categories = categoryService.getCategories()
  }

  ngOnInit(): void {
  }

}
