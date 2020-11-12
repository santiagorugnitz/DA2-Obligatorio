import { Injectable } from '@angular/core';
import { Administrator } from 'src/models/administrator';

@Injectable({
  providedIn: 'root'
})
export class AdministratorsService {

  admins: Administrator[] = []
  constructor() {
    this.admins.push({Id:1, Name:'Martin', Password:'Martin'})
    this.admins.push({Id:2, Name:'Santiago', Password:'Santiago'})
   }

  login(username: string, password: string): boolean {
    return (username == 'admin' && password == 'admin') 
  }

  logout(username: string): void {
     
  }

  getAdministrators(): Administrator[] {
    return this.admins
  }

  addUser(Name:string, Password:string): Administrator[]{
    this.admins.push({Id:3, Name:Name, Password:Password})
    return this.admins
  }

  modifyUser(Id:number, Name:string, Password:string): Administrator[]{
    this.admins = this.admins.filter(obj => obj.Id !== Id)
    this.admins.push({Id:Id, Name:Name, Password:Password})
    return this.admins
  }

  deleteUser(Id:number): Administrator[]{
    this.admins.pop()
    return this.admins
  }
}


