import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { RegistrationService } from '../services/registration.service';
import { login } from '../login/login.model';

@Component({
  selector: 'app-adminlogin',
  templateUrl: './adminlogin.component.html',
  styleUrls: ['./adminlogin.component.css']
})
export class AdminloginComponent implements OnInit {
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
    this.registrationService.AdminLogin(this.loginobject).subscribe((response: any) => {
      if (response.IsValidUser) {
        sessionStorage.setItem('email', this.loginobject.email);
        sessionStorage.setItem('userId', response.UserId);
        sessionStorage.setItem('userName', response.UserName);
        sessionStorage.setItem('role', response.Role);
        window.location.href = 'admin';
      }
      else {
        this.err = 'Invalid username or password!!';
      }
    });
  }

}
