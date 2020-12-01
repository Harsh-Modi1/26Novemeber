import { Component, OnInit } from '@angular/core';
import { productservice } from '../services/productservice';
import { WishlistModel } from 'src/app/models/wishlist.model';
import { Products } from '../models/Products.model';
@Component({
  selector: 'app-wishlist',
  templateUrl: './wishlist.component.html',
  styleUrls: ['./wishlist.component.css']
})
export class WishlistComponent implements OnInit {
  deleteProductId: number;
  deletewishid: number;
  products: Products[];
  wishlistmodel: WishlistModel[] = new Array<WishlistModel>();
  imagePath: string = 'https://localhost:44324/';
  constructor(private prodservice: productservice) { }

  ngOnInit(): void {
    debugger;
    this.getwishproducts();

  }
  getwishproducts(){
    let userId = Number(sessionStorage.getItem('userId'));
    this.prodservice.getWishlistProduct(userId).subscribe((data: any) => {
      debugger;
      this.wishlistmodel = data;
    });

  }
  // AddToCart(productModel) {
  //   if (!sessionStorage.getItem('userId')) {
  //     this.router.navigate(['login']);
  //     alert('Login first to add to Cart.');
  //     return;
  //   }
  //   let model = {
  //     ProductID: productModel.ProductID,
  //     TotalPrice: productModel.ProductPrice,
  //     Quantity: this.cartQuantity,
  //     UserID: sessionStorage.getItem('userId')
  //   };
  //   this.prodservice.AddToCart(model).subscribe((response: any) => {
  //     if (response == 'Success') {
  //       alert('Product Successfully added to cart.');
  //     }
  //     else if (response == 'ProductID Already Exists in Cart.') {
  //       alert(response);
  //     }
  //     else {
  //       alert(response);
  //     }
  //   });
  // }

  

  handleRemoveFromWishlist(id){
   this.prodservice. RemoveFromWishList(this.deletewishid = id).subscribe((response: any) => {
     if (response == 'Success'){
       alert('Product successfully removed from Wishlist');
       this.getwishproducts();
     }
    });
   }
   
}
