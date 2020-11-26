import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ImportersService {

  uri = `${environment.baseUrl}/importers`;

  constructor(private http: HttpClient) {}

  getImporters(): Observable<string[]>{
    let myHeaders = new HttpHeaders();
    myHeaders = myHeaders.set('token', localStorage.token)
    return this.http.get<string[]>(this.uri, {headers:myHeaders});
  }

  import(id: number, fileName: string): Observable<string> {
    let myHeaders = new HttpHeaders();
    myHeaders = myHeaders.set('token', localStorage.token)
    var body = [{Type:"File",Name:"File",Value:fileName}]
    return this.http.post<string>(`${this.uri}/${id}/upload`,body, {headers:myHeaders, responseType: 'text' as 'json' })

  }

}
