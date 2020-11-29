import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Products } from '../models/Products.model';

@Injectable({ providedIn: 'root' })
export class userproductservice {
    product: Products[];
    constructor(private http: HttpClient) { }

    readonly uri = 'https://localhost:44324/api/UserProduct/';

    getUserProducts(model) {
        return this.http.post(this.uri + 'GetUserProducts', model);
    }
}
