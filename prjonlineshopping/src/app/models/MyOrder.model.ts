import { CartModel } from './Cart.model';

export class MyOrderModel {
    constructor() {
        this.OrderID = null;
        this.UserID = null;
        this.OrderTotal = 0;
        this.OrderDate = null;
        this.CartModel = new Array<CartModel>();
    }
    OrderID: number;
    UserID: number;
    OrderTotal: number;
    OrderDate: Date;
    CartModel: CartModel[];
}
