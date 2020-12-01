import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MustMatch } from '../mustmatch';
import { RegistrationModel } from '../models/registration-model';
import { RegistrationService } from '../services/registration.service';

@Component({
  selector: 'app-adminregistration',
  templateUrl: './adminregistration.component.html',
  styleUrls: ['./adminregistration.component.css']
})
export class AdminregistrationComponent implements OnInit {
  registrationModel: RegistrationModel = new RegistrationModel();
  registerForm: FormGroup;
  submitted = false;

  constructor(private formBuilder: FormBuilder, private registrationService: RegistrationService) { }

  ngOnInit(): void {
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
  get f() { return this.registerForm.controls; }

  onSubmit(model) {
    this.submitted = true;
    if (this.registerForm.invalid) {
      return;
    }
    model.Role = 'Admin';
    this.registrationService.Register(model).subscribe((response: any) => {
      this.submitted = false;
      alert("Admin Registered Successfully.");
      this.registrationModel = new RegistrationModel();
    });
  }
}
