import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class UploadImageService {

  constructor(private http: HttpClient) { }

  postFile(ProductCode: string, ProductName: string, CategoryID: string, ProductDescription: string, ProductPrice: string,
           // tslint:disable-next-line: max-line-length
           Brand: string, Quantity: number, Instock: string, Outstock: string, fileToUpload: File, fileToUpload1: File, fileToUpload2: File) {
    const endpoint = 'https://localhost:44324/api/Image/PostUploadImage';
    const formData: FormData = new FormData();
    formData.append('Image', fileToUpload, fileToUpload.name);
    formData.append('Image1', fileToUpload1, fileToUpload1.name);
    formData.append('Image2', fileToUpload2, fileToUpload2.name);
    formData.append('ProductCode', ProductCode);
    formData.append('ProductName', ProductName);
    formData.append('CategoryID', CategoryID);
    formData.append('ProductDescription', ProductDescription);
    formData.append('ProductPrice', ProductPrice);
    formData.append('Brand', Brand);
    formData.append('Instock', Instock);
    formData.append('Outstock', Outstock);
    formData.append('Quantity', String(Quantity));

    return this.http.post(endpoint, formData);
  }

}
