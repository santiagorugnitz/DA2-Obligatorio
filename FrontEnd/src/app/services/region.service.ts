import { Injectable } from '@angular/core';
import { Region } from "../models/region";

@Injectable({
  providedIn: 'root'
})
export class RegionService {

  constructor() { }

  getRegions(): Region[]{
    const regions : Region[] = [];
    regions.push({Id:1,Name:"Metropolitana"})      
    regions.push({Id:2,Name:"Pajaros Pintados"})      
    regions.push({Id:3,Name:"Este"})      

    return regions
  }

}
