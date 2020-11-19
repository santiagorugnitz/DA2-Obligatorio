export class Administrator {
    Id:number
    Name:string
    Email:string
    Password:string

    constructor(name: string, pass: string) {
        this.Name = name;
        this.Password = pass;
      }
}
