import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Products } from '../models/Products.model';

@Injectable({ providedIn: 'root' })
export class productservice {

    constructor(private http: HttpClient) { }

    readonly uri = 'https://localhost:44324/api/Products/';

    getProduct() {
        return this.http.get(this.uri);
    }

    getProductsByRetailerId(retailerId) {
        return this.http.get('https://localhost:44324/api/RetailersProduct/GetProductsByRetailerId?retailerId=' + retailerId);
    }

    Product(model) {
        return this.http.post('https://localhost:44324/api/Products/', model);
    }

    insertProduct(prod) {
        return this.http.post(this.uri, prod);
    }

    insertProductImage(fileToUploads, productId, isDefault) {
        // const formDataObj: FormData[] = new Array<FormData>();
        // fileToUploads.forEach(fileToUpload => {
        //     const formData: FormData = new FormData();
        //     formData.append('Name', fileToUpload.name);
        //     formData.append('Image', fileToUpload);
        //     formData.append('ProductId', productId);
        //     formData.append('IsDefault', isDefault);
        //     formDataObj.push(formData);
        // });
        const formData = new FormData();

        formData.append('ProductId', productId);
        formData.append('IsDefault', isDefault);

        const array = new Array<File>();

        fileToUploads.forEach(fileToUpload => {
            array.push(fileToUpload);
        });

        array.forEach(x => {
            formData.append('Image', x, x.name);
        });

        return this.http.post('https://localhost:44324/api/Image/UploadImage', formData);
    }

    deleteProduct(id) {
        return this.http.delete(this.uri + id);
    }

    putProduct(product: Products) {
        return this.http.put(this.uri + product.ProductID, product);
    }

    getProductbyid(id) {
        return this.http.get(this.uri + 'GetProductById?id=' + id);
    }

    updateProduct(product: Products) {
        return this.http.put(this.uri + product.ProductID, product);
    }
    // webapi called for adding the products to Cart
    AddToCart(model) {
        return this.http.post('https://localhost:44324/api/Carts/AddToCart', model);
    }
    // webapi called for adding the products to Wishlist
    AddToWishlist(model) {
        return this.http.post('https://localhost:44324/api/Wishlist/', model);
    }

    // webapi called for adding the products for Comparison
    compareProduct(id) {
        return this.http.get('https://localhost:44324/api/CompareProducts/AddProducts?id1=' + id);
    }
    // webapi called for comparing the products for Comparison for products
    getCompareProduct() {
        return this.http.get('https://localhost:44324/api/CompareProducts/GetProducts');
    }

    getCartProduct(userId) {
        return this.http.get('https://localhost:44324/api/Carts/GetCart?userId=' + userId);
    }

    getWishlistProduct(userId) {
        return this.http.get('https://localhost:44324/api/Wishlist/WishlistProduct?userId=' + userId);
    }

    RemoveFromWishList(id) {
        return this.http.delete("https://localhost:44324/api/Wishlist/" + id);
    }

    UpdateCart(model) {
        return this.http.put('https://localhost:44324/api/Carts/UpdateCart', model);
    }
    RemovefromCart(id) {
        return this.http.delete("https://localhost:44324/api/Carts/" + id);
    }
}
