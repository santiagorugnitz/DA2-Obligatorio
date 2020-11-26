import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Administrator } from 'src/models/administrator';
import { User } from 'src/models/user';

@Injectable({
  providedIn: 'root'
})
export class AdministratorsService {

  uri = `${environment.baseUrl}/administrators`;

  constructor(private http: HttpClient) {}

  addUser(name:string, email:string, password:string): Observable<any> {
    let myHeaders = new HttpHeaders();
    myHeaders = myHeaders.set('token', localStorage.token);
    const user:Administrator = {name: name, email: email, password: password, id:0}
    return this.http.post<string>(this.uri, user, { headers: myHeaders, responseType: 'text' as 'json' });
  }

  login(email: string, password: string): Observable<string> {
    let myHeaders = new HttpHeaders();
    myHeaders = myHeaders.set('Accept', 'application/text');
    return this.http.post<string>(`${this.uri}/${"login"}`, {Email: email, Password: password}, 
    { headers: myHeaders, responseType: 'text' as 'json' });
  }

  logout(): Observable<string> {
    let myHeaders = new HttpHeaders();
    myHeaders = myHeaders.set('token', localStorage.token);
    return this.http.delete<string>(`${this.uri}/${"logout"}`, { headers: myHeaders, responseType: 'text' as 'json' });
  }

  getAdministrators(): Observable<Administrator[]> {
    let myHeaders = new HttpHeaders();
    myHeaders = myHeaders.set('token', localStorage.token);
    return this.http.get<Administrator[]>(this.uri, { headers: myHeaders });
  }

  getAdministrator(id:number): Observable<Administrator> {
    let myHeaders = new HttpHeaders();
    myHeaders = myHeaders.set('token', localStorage.token);
    return this.http.get<Administrator>(`${this.uri}/${id}`, { headers: myHeaders });
  }

  modifyUser(Id:number, name:string, email:string, password:string): Observable<any>{
    let myHeaders = new HttpHeaders();
    myHeaders = myHeaders.set('token', localStorage.token).set("Content-Type","application/json");
    return this.http.put<void>(`${this.uri}/${Id}`, {Name:name, Email:email, Password:password}, 
    { headers: myHeaders, responseType: 'text' as 'json' });
  }

  deleteUser(Id:number): Observable<any>{
    let myHeaders = new HttpHeaders();
    myHeaders = myHeaders.set('token', localStorage.token);
    var uri = `${this.uri}/${Id}`
    return this.http.delete<void>(uri, { headers: myHeaders, responseType: 'text' as 'json' });
  }
}


