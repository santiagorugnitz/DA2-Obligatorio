import { Injectable } from '@angular/core';
import { RouterStateSnapshot } from '@angular/router';
import {Reservation} from "../models/reservation";


@Injectable({
  providedIn: 'root'
})
export class ReservationService {

  constructor() { }

  getReservation(id:number): Reservation{
    return null
  }

  review(id:number,comment:string,score:number){
  }
  
  changeState(id:number,state:string,description:string){
  }

  postReservation(Reservation){
    
  }

  getFromAccomodation(id:number):Reservation[]{
    return null
  }

}
