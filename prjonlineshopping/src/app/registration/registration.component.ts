import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MustMatch } from '../mustmatch';
import { RegistrationModel } from '../models/registration-model';
import { RegistrationService } from '../services/registration.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html'
})
export class RegistrationComponent implements OnInit {
  registrationModel: RegistrationModel = new RegistrationModel();
  registerForm: FormGroup;
  submitted: boolean = false;
  constructor(private formBuilder: FormBuilder, private registrationService: RegistrationService) { }

  ngOnInit(): void {
    this.registerForm = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      gender: ['', Validators.required],
      mobileNo: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      role: ['', [Validators.required, Validators.required]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', Validators.required]
    },
      { validator: MustMatch('password', 'confirmPassword') }
    );
  }
  get f() { return this.registerForm.controls; }

  onSubmit(model) {
    this.submitted = true;
    if (this.registerForm.invalid) {
      return;
    }
    this.registrationService.Register(model).subscribe((response: any) => {
      this.submitted = false;
      if (response == "Success") {
        alert("User Registered Successfully.");
        this.registrationModel = new RegistrationModel();
        window.location.href = 'login';
      }
      else{
        alert(response);
      }
    });

  }
}
