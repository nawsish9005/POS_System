import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from '../auth/auth.service';

@Injectable({
  providedIn: 'root'
})
export class PosService {
  private baseUrl = 'https://localhost:7293/api';
  constructor(private http: HttpClient, private authService: AuthService) { }

  public customerUrl = "/customer";

  public GetAllCustomer(){
    return this.http.get(this.baseUrl + this.customerUrl);
   }

   public GetCustomerById(id: number){
    return this.http.get(this.baseUrl + this.customerUrl + "/" + id);
  }

  public CreateCustomer(data: any){
    return this.http.post(this.baseUrl + this.customerUrl, data);
  }
  
  public UpdateCustomer(id:number, data: any){
    return this.http.put(`${this.baseUrl + this.customerUrl}/${id}`, data)
  }
  
  public DeleteCustomer(id: number){
    return this.http.delete(this.baseUrl + this.customerUrl + "/" + id);
  }


  public categoryUrl = "/Category";

  public GetAllCategory(){
    return this.http.get(this.baseUrl + this.categoryUrl);
   }

   public GetCategoryById(id: number){
    return this.http.get(this.baseUrl + this.categoryUrl + "/" + id);
  }

  public CreateCategory(data: any){
    return this.http.post(this.baseUrl + this.categoryUrl, data);
  }
  
  public UpdateCategory(id:number, data: any){
    return this.http.put(`${this.baseUrl + this.categoryUrl}/${id}`, data)
  }
  
  public DeleteCategory(id: number){
    return this.http.delete(this.baseUrl + this.categoryUrl + "/" + id);
  }


  public branchesUrl = "/Branches";

  public GetAllBranches(){
    return this.http.get(this.baseUrl + this.branchesUrl);
   }

   public GetBranchesById(id: number){
    return this.http.get(this.baseUrl + this.branchesUrl + "/" + id);
  }

  public CreateBranches(data: any){
    return this.http.post(this.baseUrl + this.branchesUrl, data);
  }
  
  public UpdateBranches(id:number, data: any){
    return this.http.put(`${this.baseUrl + this.branchesUrl}/${id}`, data)
  }
  
  public DeleteBranches(id: number){
    return this.http.delete(this.baseUrl + this.branchesUrl + "/" + id);
  }

  public productUrl = "/Product";

  GetAllProduct(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl + this.productUrl);
  }

   public GetProductById(id: number){
    return this.http.get(this.baseUrl + this.productUrl + "/" + id);
  }

  public CreateProduct(data: any){
    return this.http.post(this.baseUrl + this.productUrl, data);
  }
  
  public UpdateProduct(id:number, data: any){
    return this.http.put(`${this.baseUrl + this.productUrl}/${id}`, data)
  }
  
  public DeleteProduct(id: number){
    return this.http.delete(this.baseUrl + this.productUrl + "/" + id);
  }


  public supplierUrl = "/Supplier";

  public GetAllSupplier(){
    return this.http.get(this.baseUrl + this.supplierUrl);
   }

   public GetSupplierById(id: number){
    return this.http.get(this.baseUrl + this.supplierUrl + "/" + id);
  }

  public CreateSupplier(data: any){
    return this.http.post(this.baseUrl + this.supplierUrl, data);
  }
  
  public UpdateSupplier(id:number, data: any){
    return this.http.put(`${this.baseUrl + this.supplierUrl}/${id}`, data)
  }
  
  public DeleteSupplier(id: number){
    return this.http.delete(this.baseUrl + this.supplierUrl + "/" + id);
  }


  public discountUrl = "/Discount";

  public GetAllDiscount(){
    return this.http.get(this.baseUrl + this.discountUrl);
   }

   public GetDiscountById(id: number){
    return this.http.get(this.baseUrl + this.discountUrl + "/" + id);
  }

  public CreateDiscount(data: any){
    return this.http.post(this.baseUrl + this.discountUrl, data);
  }
  
  public UpdateDiscount(id:number, data: any){
    return this.http.put(`${this.baseUrl + this.discountUrl}/${id}`, data)
  }
  
  public DeleteDiscount(id: number){
    return this.http.delete(this.baseUrl + this.discountUrl + "/" + id);
  }


  public taxUrl = "/Tax";

  public GetTaxes(){
    return this.http.get(this.baseUrl + this.taxUrl);
   }

   public GetTaxById(id: number){
    return this.http.get(this.baseUrl + this.taxUrl + "/" + id);
  }

  public CreateTax(data: any){
    return this.http.post(this.baseUrl + this.taxUrl, data);
  }
  
  public UpdateTax(id:number, data: any){
    return this.http.put(`${this.baseUrl + this.taxUrl}/${id}`, data)
  }
  
  public DeleteTax(id: number){
    return this.http.delete(this.baseUrl + this.taxUrl + "/" + id);
  }

  public stockUrl = "/Purchase";

  public GetStocks(){
    return this.http.get(this.baseUrl + this.stockUrl);
   }

   public GetStockById(id: number){
    return this.http.get(this.baseUrl + this.stockUrl + "/" + id);
  }

  public CreateStock(data: any){
    return this.http.post(this.baseUrl + this.stockUrl, data);
  }
  
  public UpdateStock(id:number, data: any){
    return this.http.put(`${this.baseUrl + this.stockUrl}/${id}`, data)
  }
  
  public DeleteStock(id: number){
    return this.http.delete(this.baseUrl + this.stockUrl + "/" + id);
  }


  public purchaseItemUrl = "/PurchaseItem";

  public GetPurchaseItems(){
    return this.http.get(this.baseUrl + this.purchaseItemUrl);
   }

   public GetPurchaseItemById(id: number){
    return this.http.get(this.baseUrl + this.purchaseItemUrl + "/" + id);
  }

  public CreatePurchaseItem(data: any){
    return this.http.post(this.baseUrl + this.purchaseItemUrl, data);
  }
  
  public UpdatePurchaseItem(id:number, data: any){
    return this.http.put(`${this.baseUrl + this.purchaseItemUrl}/${id}`, data)
  }
  
  public DeletePurchaseItem(id: number){
    return this.http.delete(this.baseUrl + this.purchaseItemUrl + "/" + id);
  }

// pos.service.ts
private updateProfileUrl = '/account/updateprofile';
private getProfileUrl = '/account/getprofile';

public updateProfile(data: any): Observable<any> {
  return this.http.put(`${this.baseUrl}${this.updateProfileUrl}`, data);
}

public GetProfile(): Observable<any> {
  return this.http.get(`${this.baseUrl}${this.getProfileUrl}`);
}

public getAllUsers(): Observable<any[]> {
  return this.http.get<any[]>(this.roleBaseUrl + '/get-users');
}



private roleBaseUrl = this.baseUrl + '/account';

// ✅ POST: Create a new role
createRole(roleData: any): Observable<any> {
  return this.http.post(`${this.roleBaseUrl}/add-role`, roleData);
}

updateRole(id: string, newRoleName: any): Observable<any> {
  return this.http.put(`${this.roleBaseUrl}/roleUpdate?id=${id}`, newRoleName);
}

public getAllRoles() {
  return this.http.get<string[]>(this.roleBaseUrl + '/get-roles');
}

// ✅ DELETE: Remove role
public deleteRole(id: string) {
  return this.http.delete(this.roleBaseUrl + `/roleDelete?id=${id}`);
}

// Assign role to user — unchanged
public assignRole(data: { userName: string; role: string }) {
  return this.http.post(this.roleBaseUrl + '/assign-role', data);
}

// ✅ GET: Get all assigned roles for all users
public getAllAssignedRoles(): Observable<any[]> {
  return this.http.get<any[]>(`${this.roleBaseUrl}/get-all-assign-role`);
}

// ✅ GET: Get assigned roles by username
public getAssignedRoleById(userName: string): Observable<any> {
  return this.http.get(`${this.roleBaseUrl}/get-assign-role-by-id?username=${userName}`);
}

// ✅ PUT: Update user's assigned roles
public updateAssignedRole(data: { userName: string; roles: string[] }): Observable<any> {
  return this.http.put(`${this.roleBaseUrl}/update-assign-role`, data);
}

// ✅ DELETE: Remove role from user
public deleteAssignedRole(data: { userName: string; role: string }): Observable<any> {
  return this.http.request('delete', `${this.roleBaseUrl}/delete-assign-role`, { body: data });
}

// Get role by ID — optional
public getRoleById(id: string) {
  return this.http.get(this.roleBaseUrl + `/getRoleById?id=${id}`);
}

}
