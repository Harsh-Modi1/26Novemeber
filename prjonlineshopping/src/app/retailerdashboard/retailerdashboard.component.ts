import { Component, OnInit } from '@angular/core';
import { productservice } from '../services/productservice';
import { Products } from '../models/Products.model';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-retailerdashboard',
  templateUrl: './retailerdashboard.component.html',
  styleUrls: ['./retailerdashboard.component.css']
})
export class RetailerdashboardComponent implements OnInit {
  products: Products[];
  deleteProductId: number;
  imagePath: string = 'https://localhost:44324/';
  
  constructor(private prodservice: productservice, private modalService: NgbModal) { }

  ngOnInit(): void {
    this.getProductsByRetailerId(Number(sessionStorage.getItem('userId')));
  }

  getProductsByRetailerId(retailerId) {
    this.prodservice.getProductsByRetailerId(retailerId).subscribe((data: any) => {
      this.products = data;
    });
  }

  DeleteConfirmation(id) {
    this.deleteProductId = id;
  }

  DeleteProduct() {
    this.prodservice.deleteProduct(this.deleteProductId).subscribe((response: any) => {
      this.getProductsByRetailerId(Number(sessionStorage.getItem('userId')));
    });
  }

  openDeletePopup(contentdelete, id) {
    this.deleteProductId = id;
    this.modalService.open(contentdelete);
  }

}
