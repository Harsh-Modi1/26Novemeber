import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({ providedIn: 'root' })
export class MyOrdersservice {
    constructor(private http: HttpClient) { }
    
    getMyOrders(userId) {
        return this.http.get('https://localhost:44324/api/MyOrders/GetMyOrders?userId=' + userId);
    }
}
