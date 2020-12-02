import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Categories } from '../models/category.model';
@Injectable({ providedIn: 'root' })

export class categoryservice {
    constructor(private http: HttpClient) { }

    GetCategoryList() {
        return this.http.get('https://localhost:44324/api/Categories');
    }



}
