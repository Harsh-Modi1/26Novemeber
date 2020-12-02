import { Component, OnInit } from '@angular/core';
import { RegistrationModel } from '../models/registration-model';
import { Adminservice } from '../services/admin.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';


@Component({
  selector: 'app-retailerview',
  templateUrl: './retailerview.component.html',
  styleUrls: ['./retailerview.component.css']
})
export class RetailerviewComponent implements OnInit {
  registrationModel: RegistrationModel[];
  registration: RegistrationModel = new RegistrationModel();
  deleteUserId: number;
  addUpdate: string = 'Add';
  constructor(private adminservice: Adminservice, private modalService: NgbModal) { }

  ngOnInit(): void {
    this.GetRetailer();
  }
  GetRetailer() {
    this.adminservice.GetRetailer().subscribe((data: any) => {
      this.registrationModel = data;
    });
  }
  GetRetailerById(id) {
    debugger;
    this.adminservice.GetRetailerById(id).subscribe((response: any) => {
      this.addUpdate = 'Update';
      this.registration = response;
    });
  }
  DeleteConfirmation(id) {
    this.deleteUserId = id;
  }

  DeleteProduct() {
    debugger;
    this.adminservice.DeleteRetailer(this.deleteUserId).subscribe((response: any) => {
      this.GetRetailer();
      alert("Retailer Removed Successfully")
    });
  }
  openDeletePopup(contentdelete, id) {
    this.deleteUserId = id;
    this.modalService.open(contentdelete);
  }
}
