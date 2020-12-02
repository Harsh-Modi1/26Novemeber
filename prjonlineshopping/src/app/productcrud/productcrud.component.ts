import { Component, OnInit } from '@angular/core';
import { categoryservice } from '../services/category.service';
import { productservice } from '../services/productservice';
import { Categories } from '../models/category.model';
import { Products } from '../models/Products.model';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

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
  productForm: FormGroup;
  submitted: boolean = false;
  productFiles: File[] = new Array<File>();

  constructor(private prodservice: productservice, private catservice: categoryservice,
              private modalService: NgbModal, private router: Router, private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    let url = this.router.url;
    if (Number(url.split('/')[2]) != 0) {
      this.GetProductById(Number(url.split('/')[2]));
    }
    this.catservice.GetCategoryList().subscribe((response: any) => { this.categorylist = response; });

    this.productForm = this.formBuilder.group({
      productCode: ['', Validators.required],
      productName: ['', Validators.required],
      productDescription: ['', Validators.required],
      brand: ['', Validators.required],
      quantity: ['', [Validators.required, Validators.required]],
      productPrice: ['', [Validators.required, Validators.required]],
      categoryID: ['', [Validators.required, Validators.required]]
    });

  }
  get f() { return this.productForm.controls; }

  setQuantity() {
    let quantityValue: any = this.prod.Quantity;
    if (quantityValue <= 0 || quantityValue == 'e') {
      alert("Please enter valid quantity.");
      this.prod.Quantity = 1;
    }
  }

  SaveProduct() {
    this.submitted = true;
    if (this.productForm.invalid) {
      return;
    }
    this.prod.RetailerID = Number(sessionStorage.getItem('userId'));
    this.prodservice.insertProduct(this.prod).subscribe((response: any) => {
      this.submitted = false;
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
        alert(response);
      }
      else {
        this.error = response;
      }
    }, (error) => { window.alert(error.error.Message); }
    );
  }

  GetProductById(id) {
    this.prodservice.getProductbyid(id).subscribe((response: any) => {
      this.addUpdate = 'Update';
      this.prod = response;
    }, (error) => { window.alert(error.error.Message); });
  }

  handleFileInput(file: FileList) {
    this.fileToUpload = file.item(0);
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



