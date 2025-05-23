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
import { DashboardComponent } from './dashboard/dashboard.component';
import { AuthInterceptor } from './auth/auth.interceptor';
import { LogoutComponent } from './auth/logout/logout.component';
import { SupplierComponent } from './supplier/supplier.component';
import { DiscountComponent } from './discount/discount.component';
import { TaxComponent } from './tax/tax.component';
import { PurchaseItemComponent } from './purchase-item/purchase-item.component';
import { UpdateProfileComponent } from './update-profile/update-profile.component';
import { RoleComponent } from './role/role.component';
import { StockComponent } from './stock/stock.component';
import { AssignRoleComponent } from './assign-role/assign-role.component';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';

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
    LogoutComponent,
    SupplierComponent,
    DiscountComponent,
    TaxComponent,
    PurchaseItemComponent,
    UpdateProfileComponent,
    RoleComponent,
    StockComponent,
    AssignRoleComponent,
  ],
  imports: [
    HttpClientModule,
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    SweetAlert2Module.forRoot()
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
