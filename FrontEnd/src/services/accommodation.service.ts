import { Injectable } from '@angular/core';
import { AccommodationsSearchComponent } from 'src/components/accommodations-search/accommodations-search.component';
import { Accommodation } from 'src/models/accommodation';

@Injectable({
  providedIn: 'root'
})
export class AccommodationService {

  accommodations:Accommodation[] = []
  images: string[] = []

  constructor() { }

  getAccommodationByTouristSpot(spotId:Number): Accommodation[]{

    this.images.push('https://imgcy.trivago.com/c_limit,d_dummy.jpeg,f_auto,h_1300,q_auto,w_2000/itemimages/20/31/2031907_v2.jpeg')
    this.images.push('https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcT4nY1kUBFbEje1IUEXPPLvyAEnO2CRBXXMqA&usqp=CAU')
    
    this.accommodations.push({Name:'Hotel 1', Id:1, Images:this.images, Adress:'Hotel 1', Stars:2, Description:''})

    const stars = 2.5;
    this.accommodations.push({Name:'Hotel 2', Id:2, Images:this.images, Adress:'Hotel 2', Stars:stars, Description:''})
    return this.accommodations; 
  }
}
