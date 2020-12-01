import { Component, OnInit } from '@angular/core';
import { MyOrdersservice } from '../services/myorders.service';
import { MyOrderModel } from '../models/MyOrder.model';

@Component({
  selector: 'app-my-orders',
  templateUrl: './my-orders.component.html',
  styleUrls: ['./my-orders.component.css']
})
export class MyOrdersComponent implements OnInit {
  myOrderDetailList: Array<MyOrderModel> = new Array<MyOrderModel>();
  imagePath: string = 'https://localhost:44324/';

  constructor(private myOrdersservice: MyOrdersservice) { }

  ngOnInit(): void {
    this.getMyOrders(Number(sessionStorage.getItem('userId')));
  }

  getMyOrders(userId) {
    this.myOrdersservice.getMyOrders(userId).subscribe((response: any) => {
      this.myOrderDetailList = response;
    });
  }
}
