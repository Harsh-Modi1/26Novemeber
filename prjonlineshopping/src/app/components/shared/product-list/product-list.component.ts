import { Component, OnInit } from '@angular/core';
import { Products } from '../../../models/Products.model';
import { productservice } from '../../../services/productservice';
import { userproductservice } from '../../../services/userproduct.service';
import { FilterView } from '../../../models/FilterView.model';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {
  compareBtn: string = 'Add to Compare';
  products: Products[] = new Array<Products>();
  filterView: FilterView = new FilterView();
  constructor(private prodservice: productservice, private userproductservice: userproductservice) { }

  ngOnInit(): void {
    this.GetUserProducts(this.filterView);
  }

  GetUserProducts(filterModel) {
    this.userproductservice.getUserProducts(filterModel).subscribe((data: any) => {
      this.products = data;
    });
  }

  ClearFilter() {
    this.filterView = new FilterView();
    this.GetUserProducts(this.filterView);
  }

  // calling the service from productservice.ts
  addtoCompare(productId) {
    this.prodservice.compareProduct(productId).subscribe((data: any) => {
      alert('Added to compared Product.');
    });
  }
}
