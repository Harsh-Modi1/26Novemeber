import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { RegistrationService } from '../services/registration.service';
import { login } from '../login/login.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {
  loginobject: login = new login();
  err: string;
  loginForm: FormGroup;
  constructor(private registrationService: RegistrationService) {
    this.loginForm = new FormGroup({
      mailid: new FormControl(null, [Validators.required, Validators.email]),
      password: new FormControl(null, Validators.required)
    });

  }

  ngOnInit(): void {
  }
  
  doLogin() {
    this.registrationService.Login(this.loginobject).subscribe((response: any) => {
      if (response.IsValidUser) {
        sessionStorage.setItem('email', this.loginobject.email);
        sessionStorage.setItem('userId', response.UserId);
        sessionStorage.setItem('userName', response.UserName);
        sessionStorage.setItem('role', response.Role);
        if (response.Role == 'User') {
          window.location.href = 'home';
        }
        else if (response.Role == 'Retailer') {
          window.location.href = 'retailerdashboard';
        }
        else if (response.Role == 'Admin') {
          window.location.href = 'admin';
        }
      }
      else {
        this.err = 'Invalid username or password!!';
      }
    });
  }
}
