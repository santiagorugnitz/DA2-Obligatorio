import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ImportersService {

  uri = `${environment.baseUrl}/importers`;

  constructor(private http: HttpClient) {}

  import(id: number, fileName: string): Observable<string> {

    var body = [{Type:"File",Name:"File",Value:fileName}]
    return this.http.post<string>(`${this.uri}/${id}/upload`,body)

  }

}
