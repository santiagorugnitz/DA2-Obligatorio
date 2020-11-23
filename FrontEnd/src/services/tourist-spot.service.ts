import { Injectable } from '@angular/core';
import { TouristSpot } from '../models/tourist-spot';
import { TouristSpotDTO } from '../models/tourist-spot-dto';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';



@Injectable({
  providedIn: 'root'
})
export class TouristSpotService {

  uri = `${environment.baseUrl}/spots`;

  constructor(private http: HttpClient) {}

  getSpotById(id:Number): Observable<TouristSpot>{
    return this.http.get<TouristSpot>(`${this.uri}/${id}`)
  }

  getAllSpots(): Observable<TouristSpot[]> {
    return this.http.get<TouristSpot[]>(this.uri)
  }

  getSpots(regionId:Number,categories:Number[]): Observable<TouristSpot[]> {
    let params = new HttpParams().set("region",regionId.toString());
    categories.forEach(element => {
      params = params.append("cat",element.toString())
    });
    return this.http.get<TouristSpot[]>(this.uri,{params:params})
  }

  AddSpot(spot: TouristSpotDTO) {
    return this.http.post<TouristSpotDTO>(this.uri, spot);
  }
}
