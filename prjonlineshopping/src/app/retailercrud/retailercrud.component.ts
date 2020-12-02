import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MustMatch } from '../mustmatch';
import { RegistrationModel } from '../models/registration-model';
import { RegistrationService } from '../services/registration.service';
import { Adminservice } from '../services/admin.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-retailercrud',
  templateUrl: './retailercrud.component.html',
  styleUrls: ['./retailercrud.component.css']
})
export class RetailercrudComponent implements OnInit {
  registrationModel: RegistrationModel[];
  registerForm: FormGroup;
  registration: RegistrationModel = new RegistrationModel();
  deleteUserId: number;
  addUpdate: string = 'Add';
  submitted = false;
  constructor(private formBuilder: FormBuilder, private registrationService: RegistrationService,
              private adminservice: Adminservice, private router: Router) {
    this.registerForm = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      gender: ['', Validators.required],
      mobileNo: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', Validators.required]
    },
      { validator: MustMatch('password', 'confirmPassword') }
    );
  }

  ngOnInit(): void {
    let url = this.router.url;
    if (Number(url.split('/')[2]) != 0) {
      this.GetRetailerById(Number(url.split('/')[2]));
    }
  }
  get f() { return this.registerForm.controls; }

  onSubmit(model) {
    this.submitted = true;
    if (this.registerForm.invalid) {
      return;
    }
    model.Role = 'Retailer';
    this.registrationService.Register(model).subscribe((response: any) => {
      this.submitted = false;
      if (response == "Success") {
        alert('Retailer Registered Succesfully');
        this.registration = new RegistrationModel();
        window.location.href = 'admin';
      }
      else {
        alert(response);
      }
    });
  }

  GetRetailerById(id) {
    this.adminservice.GetRetailerById(id).subscribe((response: any) => {
      this.addUpdate = 'Update';
      this.registration = response;
      this.registration.ConfirmPassword = this.registration.Password;
    });
  }
}
