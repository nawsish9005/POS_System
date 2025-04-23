import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PosService {
  private baseUrl = 'https://localhost:7293/api';
  constructor(private http: HttpClient) { }

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

}
