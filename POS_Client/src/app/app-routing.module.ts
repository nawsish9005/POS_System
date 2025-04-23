import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoryComponent } from './category/category.component';
import { CustomerComponent } from './customer/customer.component';
import { BranchesComponent } from './branches/branches.component';
import { ProductComponent } from './product/product.component';
import { LoginComponent } from './auth/login/login.component';
import { RegistrationComponent } from './auth/registration/registration.component';
import { AuthGuard } from './auth/auth.guard';
import { DashboardComponent } from './dashboard/dashboard.component';
import { SupplierComponent } from './supplier/supplier.component';

const routes: Routes = [
  { path: 'login', title: 'Login', component: LoginComponent },
  { path: 'register', title: 'Register', component: RegistrationComponent },
  { path: 'dashboard', title: 'Dashboard', component: DashboardComponent },

  { path: 'category', title: 'Category', component: CategoryComponent, canActivate: [AuthGuard],
    data: { roles: ['Admin'] }},
  { path: 'customer', title: 'Customer', component: CustomerComponent, canActivate: [AuthGuard] },
  { path: 'branches', title: 'Branches', component: BranchesComponent, canActivate: [AuthGuard] },
  { path: 'product', title: 'Product', component: ProductComponent, canActivate: [AuthGuard] },
  { path: 'supplier', title: 'Supplier', component: SupplierComponent, canActivate: [AuthGuard] },

  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: '**', redirectTo: '/login' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
