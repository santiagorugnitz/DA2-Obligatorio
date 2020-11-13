import { Injectable } from '@angular/core';
import { TouristSpot } from 'src/models/tourist-spot';

@Injectable({
  providedIn: 'root'
})
export class SpotService {

  constructor() { }

  getSpotById(id:Number): TouristSpot{
    return {Name: 'Montevideo', Id: 1, Description: 'Description', Image: '', Categories:["asd","asd"]}
  }
}
