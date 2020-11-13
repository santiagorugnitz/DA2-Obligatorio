import { Injectable } from '@angular/core';
import { Accommodation } from 'src/models/accommodation';
import { ReportItem } from 'src/models/report-item';

@Injectable({
  providedIn: 'root'
})
export class ReportService {

  constructor() { }

  getAccommodationsForReport(spotId: number, startingDate: Date, finishingDate: Date): ReportItem[] {
    var items: ReportItem[] = []
    var images: string[] = []
    images.push('https://imgcy.trivago.com/c_limit,d_dummy.jpeg,f_auto,h_1300,q_auto,w_2000/itemimages/20/31/2031907_v2.jpeg')
    images.push('https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcT4nY1kUBFbEje1IUEXPPLvyAEnO2CRBXXMqA&usqp=CAU')

    var accommodation1 = ({ Name: 'Hotel 1', Id: 1, Images: images, Address: 'Hotel 1', Stars: 2, Description: '', Fee: 300, Total: 0, SpotId:spotId,State:true })

    const stars = 2.5;
    var accommodation2 = ({ Name: 'Hotel 2', Id: 2, Images: images, Address: 'Hotel 2', Stars: stars, Description: '', Fee: 100, Total: 0, SpotId:spotId,State:true })

    const stars2 = 3.5;
    var accommodation3 = ({ Name: 'Hotel 3', Id: 3, Images: images, Address: 'Hotel 3', Stars: stars2, Description: '', Fee: 200, Total: 0, SpotId:spotId,State:true })
    items.push({Accommodation: accommodation1, ReservationQuantity:8})
    items.push({Accommodation: accommodation2, ReservationQuantity:4})
    items.push({Accommodation: accommodation3, ReservationQuantity:2})

    return items;
  }
}
