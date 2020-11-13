import { Injectable } from '@angular/core';
import { Accommodation } from 'src/models/accommodation';

@Injectable({
  providedIn: 'root'
})
export class AccommodationService {
  
  accommodations:Accommodation[] = []
  images: string[] = []

  constructor() {
    
    this.images.push('https://imgcy.trivago.com/c_limit,d_dummy.jpeg,f_auto,h_1300,q_auto,w_2000/itemimages/20/31/2031907_v2.jpeg')
    this.images.push('https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcT4nY1kUBFbEje1IUEXPPLvyAEnO2CRBXXMqA&usqp=CAU') 
    this.accommodations.push({SpotId: 1, Name: 'Hotel 1', Id: 1, Images: this.images, Address: 'Hotel 1', Stars: 2, Description: '', Fee: 300, Total: 0, State: true })
    const stars = 2.5;
    this.accommodations.push({SpotId: 1, Name: 'Hotel 2', Id: 2, Images: this.images, Address: 'Hotel 2', Stars: stars, Description: '', Fee: 100, Total: 0 ,State: true})
    const stars2 = 3.5;
    this.accommodations.push({SpotId: 1, Name: 'Hotel 3', Id: 3, Images: this.images, Address: 'Hotel 3', Stars: stars2, Description: '', Fee: 200, Total: 0, State: true })
    
  }

  deleteAccommodation(Id: number): Accommodation[] {
    this.accommodations = this.accommodations.filter(obj => obj.Id !== Id)
    return this.accommodations
  }
  modifyAccommodation(accommodation: Accommodation): Accommodation[] {
    this.accommodations = this.accommodations.filter(obj => obj.Id !== accommodation.Id)
    accommodation.State = !accommodation.State
    this.accommodations.push(accommodation)
    return this.accommodations
  }
  addAccommodation(accommodation: Accommodation): Accommodation[] {
    this.accommodations.push(accommodation)
    return this.accommodations
  }
  getAccommodations(): Accommodation[] {
    return this.accommodations
  }

  getAccommodationByTouristSpot(spotId: Number): Accommodation[] {
    return this.accommodations;
  }

  getAccommodationComments(accommodationId: Number): string[] {

    var comments: string[] = []
    comments.push("Excelent sevice")
    comments.push("Has no room, but is excelent")
    comments.push("Excelent facilities")
    return comments;
  }

  calculateTotal(Id: number, startingDate: Date, finishingDate: Date, adultQuantity: number,
    retiredQuantity: number, childrenQuantity: number, babyQuantity: number): Number {
    if (Id == 1) {
      return 5000;
    } else if (Id == 2) {
      return 1000;
    } else if (Id == 3) {
      return 3000;
    }
  }
}
