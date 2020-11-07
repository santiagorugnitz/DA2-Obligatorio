import { Injectable } from '@angular/core';
import { Region } from "../models/region";

@Injectable({
  providedIn: 'root'
})
export class RegionService {

  constructor() { }

  getRegions(): Region[]{
    const regions : Region[] = [];
    regions.push({Id:1,Name:"Ciudad"})      
    regions.push({Id:2,Name:"Campo"})      
    regions.push({Id:3,Name:"Playa"})      

    return regions
  }

}
