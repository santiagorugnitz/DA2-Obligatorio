import { Injectable } from '@angular/core';
import { RouterStateSnapshot } from '@angular/router';
import { PendingReservation } from 'src/models/pending-reservation';
import {Reservation} from "../models/reservation";
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class ReservationService {

  uri = `${environment.baseUrl}/reservations`;

  constructor(private http: HttpClient) {}

  getReservation(id:number): Observable<Reservation>{
    return this.http.get<Reservation>(`${this.uri}/${id}`)
  }

  review(id:number,comment:string,score:number){
    let body = {comment:comment,score:score}
    return this.http.put(`${this.uri}/${id}`, body);
  }
  
  changeState(id:number,state:number,description:string):Observable<string>{
    let myHeaders = new HttpHeaders();
    myHeaders = myHeaders.set('token', localStorage.token);
    let body = {state:state,description:description}
    return this.http.put<string>(`${this.uri}/${id}`, body, {headers:myHeaders, responseType: 'text' as 'json'});
  }

  postReservation(reservation:PendingReservation):Observable<number>{
    return this.http.post<number>(this.uri, reservation);
  }

  getFromAccomodation(id:number):Observable<Reservation[]>{
    let params = new HttpParams().set("accomodationId",id.toString());
    return this.http.get<Reservation[]>(this.uri,{params:params})

  }

}
