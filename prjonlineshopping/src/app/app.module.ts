import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomepageComponent } from './homepage/homepage.component';
import { LoginComponent } from './login/login.component';
import { RegistrationComponent } from './registration/registration.component';
import { HttpClientModule } from '@angular/common/http';
import { ProductcrudComponent } from './productcrud/productcrud.component';
import { productservice } from './services/productservice';
import { categoryservice } from './services/category.service';
import { HeaderComponent } from './components/shared/header/header.component';
import { NavComponent } from './components/shared/nav/nav.component';
import { FooterComponent } from './components/shared/footer/footer.component';
import { ForgotpasswordComponent } from './forgotpassword/forgotpassword.component';
import { CompareproductsComponent } from './compareproducts/compareproducts.component';
import { ViewdetailComponent } from './viewdetail/viewdetail.component';
import { UserprofileComponent } from './userprofile/userprofile.component';
import { WishlistComponent } from './wishlist/wishlist.component';
import { RetailercrudComponent } from './retailercrud/retailercrud.component';
import { AdminregistrationComponent } from './adminregistration/adminregistration.component';
import { AdminloginComponent } from './adminlogin/adminlogin.component';
import { AdmindashboardComponent } from './admindashboard/admindashboard.component';
import { RetailerviewComponent } from './retailerview/retailerview.component';
import { RetailerdashboardComponent } from './retailerdashboard/retailerdashboard.component';
import { ProductfilterPipe } from 'src/app/filter/productfilter.pipe';
import { CartComponent } from './components/shared/cart/cart.component';
import { ProductListComponent } from './components/shared/product-list/product-list.component';
import { PagenotfoundComponent } from './pagenotfound/pagenotfound.component';
import { ProductmanagementComponent } from './productmanagement/productmanagement.component';
import { SortPipe } from './filter/sort.pipe';
import { UploadImageService } from './services/imageupload.service';


@NgModule({
  declarations: [
    AppComponent,
    HomepageComponent,
    LoginComponent,
    RegistrationComponent,
    ProductcrudComponent,
    HeaderComponent,
    NavComponent,
    FooterComponent,
    ProductListComponent,
    CartComponent,
    ForgotpasswordComponent,
    CompareproductsComponent,
    ViewdetailComponent,
    UserprofileComponent,
    WishlistComponent,
    RetailercrudComponent,
    AdminregistrationComponent,
    AdminloginComponent,
    AdmindashboardComponent,
    RetailerviewComponent,
    RetailerdashboardComponent,
    ProductfilterPipe,
    PagenotfoundComponent,
    ProductmanagementComponent,
    SortPipe
    ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [productservice, categoryservice, UploadImageService],
  bootstrap: [AppComponent]
})
export class AppModule { }
