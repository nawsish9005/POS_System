import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
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

  public purchaseUrl = "/Purchase";

  public GetPurchases(){
    return this.http.get(this.baseUrl + this.purchaseUrl);
   }

   public GetPurchaseById(id: number){
    return this.http.get(this.baseUrl + this.purchaseUrl + "/" + id);
  }

  public CreatePurchase(data: any){
    return this.http.post(this.baseUrl + this.purchaseUrl, data);
  }
  
  public UpdatePurchase(id:number, data: any){
    return this.http.put(`${this.baseUrl + this.purchaseUrl}/${id}`, data)
  }
  
  public DeletePurchase(id: number){
    return this.http.delete(this.baseUrl + this.purchaseUrl + "/" + id);
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
}
