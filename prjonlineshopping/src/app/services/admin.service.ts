import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RegistrationModel } from '../models/registration-model';


@Injectable({ providedIn: 'root' })
export class Adminservice {
    registrationModel: RegistrationModel[];
    constructor(private http: HttpClient) { }

    // created a service for Fetching every retailer information from Usertable
    GetRetailer() {
        return this.http.get('https://localhost:44324/api/Admin/');
    }
    GetRetailerById(id) {
        debugger;
        return this.http.get('https://localhost:44324/api/Admin/' + 'GetRetailerById?id=' + id);
    }
    // created a service for deleting a retailer by ID
    DeleteRetailer(id) {
        debugger;
        return this.http.delete('https://localhost:44324/api/Admin/' + id);
    }
}
