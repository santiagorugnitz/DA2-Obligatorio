import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AdministratorsService {

  constructor() { }

  login(username: string, password: string): boolean {
    return (username == 'admin' && password == 'admin') 
  }

  logout(username: string): void {
     
  }
}
