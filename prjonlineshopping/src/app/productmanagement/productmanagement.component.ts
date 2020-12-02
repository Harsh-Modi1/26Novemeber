import { Component, OnInit } from '@angular/core';
import { productservice } from '../services/productservice';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Products } from '../models/Products.model';
import { Image } from '../models/image.model';

@Component({
  selector: 'app-productmanagement',
  templateUrl: './productmanagement.component.html',
  styleUrls: ['./productmanagement.component.css']
})
export class ProductmanagementComponent implements OnInit {
  products: Products[];
  prod: Products = new Products();
  error: string;
  deleteProductId: number;
  selectedFile = null;
  imagePath: string = 'https://localhost:44324/';

  constructor(private prodservice: productservice, private modalService: NgbModal) { }

  ngOnInit(): void {
    this.GetProducts();
  }
  GetProducts() {
    this.prodservice.getProduct().subscribe((data: any) => {
      this.products = data;
    });
  }

















  
  DeleteConfirmation(id) {
    this.deleteProductId = id;
  }

  DeleteProduct() {
    this.prodservice.deleteProduct(this.deleteProductId).subscribe((response: any) => {
      this.GetProducts();
    });
  }

  GetProductById(id) {
    this.prodservice.getProductbyid(id).subscribe((response: any) => {
      // this.addUpdate = 'Update';
      this.prod = response;
    });
  }



  open(content) {
    this.modalService.open(content);
  }

  openDeletePopup(contentdelete, id) {
    this.deleteProductId = id;
    this.modalService.open(contentdelete);
  }

}
