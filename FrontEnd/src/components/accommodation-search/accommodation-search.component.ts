import { Component, Input, OnInit } from '@angular/core';
import { TouristSpot } from 'src/models/tourist-spot';

@Component({
  selector: 'app-accommodation-search',
  templateUrl: './accommodation-search.component.html',
  styleUrls: ['./accommodation-search.component.css']
})
export class AccommodationSearchComponent implements OnInit {

  @Input() spot: TouristSpot;

  constructor() { }

  ngOnInit(): void {
  }

}
