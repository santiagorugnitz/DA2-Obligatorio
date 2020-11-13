import { Injectable } from '@angular/core';
import { TouristSpot } from '../models/tourist-spot';


@Injectable({
  providedIn: 'root'
})
export class TouristSpotService {
  

  constructor() { }

  getAllSpots(): TouristSpot[] {
    const spots : TouristSpot[] = [];
    spots.push({Id:1,Name:"Montevideo",Description:"Capital de Uruguay",Image:"https://montevideo.gub.uy/sites/default/files/styles/noticias_twitter/public/biblioteca/dsc0263_4.jpg?itok=am2Xii7V"})
    spots.push({Id:2,Name:"Region",Description:"Largo cat:",Image:"https://montevideo.gub.uy/sites/default/files/styles/noticias_twitter/public/biblioteca/dsc0263_4.jpg?itok=am2Xii7V"})
      
    return spots
  }

  getSpots(regionId:Number,categories:Number[]): TouristSpot[]{
    const spots : TouristSpot[] = [];
    spots.push({Id:1,Name:"Montevideo",Description:"Capital de Uruguay",Image:"https://montevideo.gub.uy/sites/default/files/styles/noticias_twitter/public/biblioteca/dsc0263_4.jpg?itok=am2Xii7V"})
    spots.push({Id:2,Name:"Region"+regionId,Description:"Largo cat:"+categories.length,Image:"https://montevideo.gub.uy/sites/default/files/styles/noticias_twitter/public/biblioteca/dsc0263_4.jpg?itok=am2Xii7V"})
      
    return spots
  }
}
