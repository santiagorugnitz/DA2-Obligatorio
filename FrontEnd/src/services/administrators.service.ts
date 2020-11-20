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

  addUser(name:string, email:string, password:string): Observable<void> {
    return this.http.post<void>(this.uri, {Name: name, Email: email, Password: password});
  }

  login(email: string, password: string): Observable<string> {
    return this.http.post<string>(`${this.uri}/${"login"}`, {Email: email, Password: password});
  }

  logout(token: string): Observable<void> {
    const myHeaders = new HttpHeaders();
    myHeaders.append('token', localStorage.token);
    return this.http.delete<void>(`${this.uri}/${"logout"}`, { headers: myHeaders });
  }

  getAdministrators(): Observable<Administrator[]> {
    return this.http.get<Administrator[]>(this.uri);
  }

  getAdministrator(id:number): Observable<Administrator[]> {
    return this.http.get<Administrator[]>(`${this.uri}/${id}`);
  }

  modifyUser(Id:number, name:string, email:string, password:string): Observable<void>{
    return this.http.put<void>(`${this.uri}/${Id}`, {Name:name, Email:email, Password:password});
  }

  deleteUser(Id:number): Observable<void>{
    return this.http.delete<void>(`${this.uri}/${Id}`);
  }
}


