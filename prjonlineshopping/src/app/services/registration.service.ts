import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class RegistrationService {

  constructor(private http: HttpClient) { }
  // Calling WebApi For User Registration
  Register(model) {
    return this.http.post('https://localhost:44324/api/Users/RegisterUser', model);
  }
  
  // Calling WebApi For User Login
  Login(model) {
    return this.http.get('https://localhost:44324/api/Users/?email=' + model.email + '&password=' + model.password);
  }

  AdminLogin(model) {
    return this.http.get('https://localhost:44324/api/AdminLogin/AdminLogin/?email=' + model.email + '&password=' + model.password);
  }

  // Calling WebApi For Forgot Password
  ForgotPassword(email) {
    return this.http.get('https://localhost:44324/api/OTP/VerifyEmail?email=' + email);
  }
  
  // Calling WebApi For Change Password
  ChangePassword(model) {
    return this.http.post('https://localhost:44324/api/OTP/ChangePassword', model);
  }

  GetRetailer() {
    return this.http.get('https://localhost:44324/api/Users/');
  }
}
