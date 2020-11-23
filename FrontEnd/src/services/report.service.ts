import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Accommodation } from 'src/models/accommodation';
import { ReportItem } from 'src/models/report-item';

@Injectable({
  providedIn: 'root'
})
export class ReportService {

  uri = `${environment.baseUrl}/report`;

  constructor(private http: HttpClient) {}

  getAccommodationsForReport(spotId: number, startingDate: Date, finishingDate: Date): Observable<ReportItem[]> {
    let myHeaders = new HttpHeaders();
    myHeaders = myHeaders.set('token', localStorage.token);
    var uri = `${this.uri}/${spotId.toString()}, ${startingDate.toISOString()}, ${finishingDate.toISOString()}`
    return this.http.get<ReportItem[]>(uri,{headers:myHeaders});
  }
}
