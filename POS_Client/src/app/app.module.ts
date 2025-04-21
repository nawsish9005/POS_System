import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { NavbarComponent } from './navbar/navbar.component';
import { CategoryComponent } from './category/category.component';
import { BranchesComponent } from './branches/branches.component';
import { CustomerComponent } from './customer/customer.component';
import { ProductComponent } from './product/product.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './auth/login/login.component';
import { RegistrationComponent } from './auth/registration/registration.component';
import { JwtInterceptor } from './auth/jwt.interceptor';
import { DashboardComponent } from './dashboard/dashboard.component';

@NgModule({
  declarations: [
    AppComponent,
    SidebarComponent,
    NavbarComponent,
    CategoryComponent,
    BranchesComponent,
    CustomerComponent,
    ProductComponent,
    LoginComponent,
    RegistrationComponent,
    DashboardComponent,
  ],
  imports: [
    HttpClientModule,
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
