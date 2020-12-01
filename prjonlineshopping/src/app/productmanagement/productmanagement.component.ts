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
  // imageUrl: string = '/assets/images/download.png';
  // fileToUpload: File = null;
  // mdlSampleIsOpen: boolean = false;
  // productImages: Image[] = new Array<Image>();

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

  // handleFileInput(file: FileList) {
  //   debugger;
  //   this.fileToUpload = file.item(0);
  //   var image: Image = {
  //     ImageID: 0,
  //     IsDefault: false,
  //     ProductID: this.prod.ProductID,
  //     ProductImage: this.fileToUpload.name
  //   }
  //   this.productImages.push(image);
  //   var reader = new FileReader();
  //   reader.onload = (event: any) => {
  //     this.imageUrl = event.target.result;
  //   };
  //   reader.readAsDataURL(this.fileToUpload);
  // }

  // SaveImages() {
  //   debugger;
  //   this.prodservice.insertProductImage(this.productImages, this.prod.ProductID, true).subscribe((response: any) => {
  //     if (response == 'Success') {
  //       alert('ProductImage Saved Succesfully');
  //       var reader = new FileReader();
  //       reader.onload = (event: any) => {
  //         this.imageUrl = event.target.result;
  //       };
  //       reader.readAsDataURL(this.fileToUpload);
  //     }
  //     else {
  //       this.error = response;
  //     }
  //   });
  // }

  open(content) {
    this.modalService.open(content);
  }

  openDeletePopup(contentdelete, id) {
    this.deleteProductId = id;
    this.modalService.open(contentdelete);
  }

}
