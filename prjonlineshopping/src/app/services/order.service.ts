import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MyOrderModel } from '../models/MyOrder.model';

@Injectable({ providedIn: 'root' })
export class Orderservice {

myordermodel: MyOrderModel[];
constructor(private http: HttpClient) { }

PlaceOrder(model) {
    return this.http.post('https://localhost:44324/api/MyOrders/PlaceOrder', model);
}


}
