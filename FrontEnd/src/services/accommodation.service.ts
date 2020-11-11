import { Injectable } from '@angular/core';
import { AccommodationsSearchComponent } from 'src/components/accommodations-search/accommodations-search.component';
import { Accommodation } from 'src/models/accommodation';

@Injectable({
  providedIn: 'root'
})
export class AccommodationService {


  constructor() { }

  getAccommodationByTouristSpot(spotId: Number): Accommodation[] {
    var accommodations: Accommodation[] = []
    var images: string[] = []
    images.push('https://imgcy.trivago.com/c_limit,d_dummy.jpeg,f_auto,h_1300,q_auto,w_2000/itemimages/20/31/2031907_v2.jpeg')
    images.push('https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcT4nY1kUBFbEje1IUEXPPLvyAEnO2CRBXXMqA&usqp=CAU')

    accommodations.push({ Name: 'Hotel 1', Id: 1, Images: images, Adress: 'Hotel 1', Stars: 2, Description: '', Fee: 300, Total: 0 })

    const stars = 2.5;
    accommodations.push({ Name: 'Hotel 2', Id: 2, Images: images, Adress: 'Hotel 2', Stars: stars, Description: '', Fee: 100, Total: 0 })

    const stars2 = 3.5;
    accommodations.push({ Name: 'Hotel 3', Id: 3, Images: images, Adress: 'Hotel 3', Stars: stars2, Description: '', Fee: 200, Total: 0 })
    return accommodations;
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
