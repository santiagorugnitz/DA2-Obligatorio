import { Injectable } from '@angular/core';
import { Category } from '../models/category';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor() { }

  getCategories(): Category[]{
    const categories : Category[] = [];
    categories.push({Id:1,Name:"Ciudad"})      
    categories.push({Id:2,Name:"Campo"})      
    categories.push({Id:3,Name:"Playa"})      

    return categories
  }
}
