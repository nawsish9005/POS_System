import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoryComponent } from './category/category.component';
import { CustomerComponent } from './customer/customer.component';
import { BranchesComponent } from './branches/branches.component';
import { ProductComponent } from './product/product.component';

const routes: Routes = [
  {path: 'category',title:'Category' ,component: CategoryComponent},
  {path: 'customer',title:'Customer' ,component: CustomerComponent},
  {path: 'branches',title:'Branches' ,component: BranchesComponent},
  {path: 'product',title:'Product' ,component: ProductComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
