import { Injectable } from '@angular/core';
import { Accommodation, AccommodationDTO } from 'src/models/accommodation';
import { Comment } from 'src/models/comment';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccommodationService {


  uri = `${environment.baseUrl}/accomodations`;

  constructor(private http: HttpClient) { }


  deleteAccommodation(id: number): Observable<String> {
    return this.http.delete<String>(`${this.uri}/${id}`)
  }

  changeAvailability(Id: number, state: boolean): Observable<String> {
    return this.http.put<String>(`${this.uri}/${Id}`, state)
  }

  addAccommodation(accommodation: AccommodationDTO): Observable<string> {
    return this.http.post<string>(this.uri, accommodation);

  }
  getAccommodations(): Observable<Accommodation[]> {
    return this.http.get<Accommodation[]>(this.uri);
  }

  getAccommodationByTouristSpot(spotId: Number): Observable<Accommodation[]> {
    let params = new HttpParams().set("spotId", spotId.toString());
    return this.http.get<Accommodation[]>(this.uri, { params: params })
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
    //return this.http.get<number>(`${this.uri}/${id}/calculatetotal`)
    
  }
}
