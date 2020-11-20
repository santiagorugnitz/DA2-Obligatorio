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

  getSpotById(id:Number): TouristSpot{
    if(id > 3){
      return null
    }
    return null
  }

  getAllSpots(): Observable<TouristSpot[]> {
    return this.http.get<TouristSpot[]>(this.uri)
  }

  getSpots(regionId:Number,categories:Number[]): Observable<TouristSpot[]> {
    //TODO: fix this
    let params = new HttpParams().set("region",regionId.toString());
    categories.forEach(element => {
      params = params.append("cat",element.toString())
    });
    return this.http.get<TouristSpot[]>(this.uri,{params:params})
  }

  AddSpot(spot: TouristSpotDTO) {
    const spots : TouristSpotDTO[] = [];
    spots.push({Id:1,Name:"Montevideo",Description:"Capital de Uruguay",Image:"https://montevideo.gub.uy/sites/default/files/styles/noticias_twitter/public/biblioteca/dsc0263_4.jpg?itok=am2Xii7V",
    Categories:[], Region:0})
    spots.push({Id:2,Name:"Region",Description:"Largo cat:",Image:"https://montevideo.gub.uy/sites/default/files/styles/noticias_twitter/public/biblioteca/dsc0263_4.jpg?itok=am2Xii7V",
    Categories:[], Region:0})
    spots.push(spot)

    return spots
  }
}
