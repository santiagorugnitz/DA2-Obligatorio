export class Administrator {
    id:number
    name:string
    email:string
    password:string

    constructor(name: string, pass: string) {
        this.name = name;
        this.password = pass;
      }
}
