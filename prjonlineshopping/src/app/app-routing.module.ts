import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegistrationComponent } from './registration/registration.component';
import { ProductcrudComponent } from './productcrud/productcrud.component';
import { ForgotpasswordComponent } from 'src/app/forgotpassword/forgotpassword.component';
import { ViewdetailComponent } from './viewdetail/viewdetail.component';
import { CompareproductsComponent } from './compareproducts/compareproducts.component';
import { WishlistComponent } from './wishlist/wishlist.component';
import { RetailercrudComponent } from './retailercrud/retailercrud.component';
import { RetailerviewComponent } from './retailerview/retailerview.component';
import { RetailerdashboardComponent } from './retailerdashboard/retailerdashboard.component';
import { CartComponent } from './components/shared/cart/cart.component';
import { ProductListComponent } from './components/shared/product-list/product-list.component';
// import { PagenotfoundComponent } from './pagenotfound/pagenotfound.component';
import { ProductmanagementComponent } from './productmanagement/productmanagement.component';
import { MyOrdersComponent } from './my-orders/my-orders.component';

import { AdminregistrationComponent } from './adminregistration/adminregistration.component';
import { AdmindashboardComponent } from './admindashboard/admindashboard.component';
import { AdminloginComponent } from './adminlogin/adminlogin.component';

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'home', component: ProductListComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegistrationComponent },
  { path: 'productcrud/:id', component: ProductcrudComponent },
  { path: 'forgotpassword', component: ForgotpasswordComponent },
  { path: 'cart', component: CartComponent },
  { path: 'viewdetail/:id', component: ViewdetailComponent },
  { path: 'compare', component: CompareproductsComponent },
  { path: 'wishlist', component: WishlistComponent },
  { path: 'retailercrud', component: RetailercrudComponent },
  { path: 'retailerview', component: RetailerviewComponent },
  { path: 'retailerdashboard', component: RetailerdashboardComponent },
  // {path: '**', component: PagenotfoundComponent},
  { path: 'productmanagement', component: ProductmanagementComponent },
  { path: 'myorders', component: MyOrdersComponent },
  { path: 'admin', component: AdmindashboardComponent },
  { path: 'admin/register', component: AdminregistrationComponent },
  { path: 'admin/login', component: AdminloginComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
