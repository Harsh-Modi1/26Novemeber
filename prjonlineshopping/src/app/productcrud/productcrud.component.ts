import { Component, OnInit } from '@angular/core';
import { categoryservice } from '../services/category.service';
import { productservice } from '../services/productservice';
import { Categories } from '../models/category.model';
import { Products } from '../models/Products.model';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Image } from '../models/image.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-productcrud',
  templateUrl: './productcrud.component.html',
  styleUrls: ['./productcrud.component.css']
})

export class ProductcrudComponent implements OnInit {
  categorylist: Categories[] = new Array<Categories>();
  products: Products[];
  prod: Products = new Products();
  error: string;
  deleteProductId: number;
  addUpdate: string = 'Add';
  selectedFile = null;
  imageUrl: string = '/assets/images/download.png';
  fileToUpload: File = null;
  productImages: Image[] = new Array<Image>();
  productFiles: File[] = new Array<File>();
  constructor(private prodservice: productservice, private catservice: categoryservice, private modalService: NgbModal, private router: Router) { }

  ngOnInit(): void {
    debugger;
    let url = this.router.url;
    if (Number(url.split('/')[2]) != 0) {
      this.GetProductById(Number(url.split('/')[2]));
    }
    this.catservice.GetCategoryList().subscribe((response: any) => { this.categorylist = response; });
  }

  SaveProduct() {
    this.prod.RetailerID = Number(sessionStorage.getItem('userId'));
    this.prodservice.insertProduct(this.prod).subscribe((response: any) => {
      if (response.Status == 'Success') {
        this.prod = new Products();
        if (this.productFiles.length > 0) {
          this.SaveImages(response.ProductId);
        }
        else {
          this.router.navigate(['/retailerdashboard']);
        }
      }
      else if (response = "Product Quantity should not be in Negative Number") {
        alert('Product Quantity should not be in Negative Number');
      }
      else {
        this.error = response;
      }
    });
  }

  GetProductById(id) {
    this.prodservice.getProductbyid(id).subscribe((response: any) => {
      this.addUpdate = 'Update';
      this.prod = response;
    });
  }

  handleFileInput(file: FileList) {
    debugger;
    this.fileToUpload = file.item(0);
    // var image: Image = {
    //   ImageID: 0,
    //   IsDefault: false,
    //   ProductID: this.prod.ProductID,
    //   ProductImage: this.fileToUpload.name
    // }
    // this.productImages.push(image);
    // var reader = new FileReader();
    // reader.onload = (event: any) => {
    //   this.imageUrl = event.target.result;
    // };
    // reader.readAsDataURL(this.fileToUpload);
    this.productFiles.push(this.fileToUpload);
  }

  SaveImages(productId) {
    this.prodservice.insertProductImage(this.productFiles, productId, true).subscribe((response: any) => {
      if (response == 'Success') {
        alert('Product Saved Succesfully');
        this.router.navigate(['/retailerdashboard']);
      }
      else {
        this.error = response;
      }
    });
  }

  open(content) {
    this.modalService.open(content);
  }

  Cancel() {
    this.router.navigate(['/retailerdashboard']);
  }
}



