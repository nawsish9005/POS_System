import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PosService } from '../services/pos.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {
  productForm!: FormGroup;
  products: any[] = [];
  branches: any[] = [];
  categories: any[] = [];
  suppliers: any[] = [];

  isEditMode = false;
  selectedProductId: number | null = null;
  selectedPhoto: File | null = null;

  constructor(private fb: FormBuilder, private posService: PosService) {}

  ngOnInit(): void {
    this.initForm();
    this.loadProducts();
    this.loadDropdowns();
  }

  initForm() {
    this.productForm = this.fb.group({
      name: ['', Validators.required],
      price: [0, Validators.required],
      stockQuantity: [0],
      expiryDate: ['', Validators.required],
      manufactureDate: ['', Validators.required],
      branchId: ['', Validators.required],
      categoryId: ['', Validators.required],
      supplierId: ['', Validators.required],
      photo: [null]
    });
  }

  loadProducts() {
    this.posService.GetAllProduct().subscribe(
      (data: any) => this.products = data,
      error => console.error('Error loading products', error)
    );
  }

  loadDropdowns() {
    this.posService.GetAllBranches().subscribe(
      (data: any) => {
        this.branches = data;
        console.log('Branches loaded:', this.branches);
      },
      (error) => {
        console.error('Error loading branches:', error);
      }
    );
  
    this.posService.GetAllCategory().subscribe(
      (data: any) => {
        this.categories = data;
        console.log('Categories loaded:', this.categories);
      },
      (error) => {
        console.error('Error loading categories:', error);
      }
    );
  
    this.posService.GetAllSupplier().subscribe(
      (data: any) => {
        this.suppliers = data;
        console.log('Suppliers loaded:', this.suppliers);
      },
      (error) => {
        console.error('Error loading suppliers:', error);
      }
    );
  }

  
  onSubmit() {
    if (this.productForm.invalid) return;

    const formValues = this.productForm.value;
    const formData = new FormData();
    formData.append('name', formValues.name);
    formData.append('price', formValues.price);
    formData.append('stockQuantity', formValues.stockQuantity);
    formData.append('expiryDate', formValues.expiryDate);
    formData.append('manufactureDate', formValues.manufactureDate);
    formData.append('branchId', formValues.branchId);
    formData.append('categoryId', formValues.categoryId);
    formData.append('supplierId', formValues.supplierId);
    if (this.selectedPhoto) {
      formData.append('photo', this.selectedPhoto);
    }

    if (this.isEditMode && this.selectedProductId !== null) {
      this.posService.UpdateProduct(this.selectedProductId, formData).subscribe(() => {
        this.loadProducts();
        this.resetForm();
      });
    } else {
      this.posService.CreateProduct(formData).subscribe(() => {
        this.loadProducts();
        this.resetForm();
      });
    }
  }

  getCategoryName(id: number): string {
    const found = this.categories.find(c => c.id === id);
    return found ? found.categoryName : '';
  }
  
  getSupplierName(id: number): string {
    const found = this.suppliers.find(s => s.id === id);
    return found ? found.companyName : '';
  }
  
  getBranchName(id: number): string {
    const found = this.branches.find(b => b.id === id);
    return found ? found.branchName : '';
  }

  editProduct(product: any) {
    this.productForm.patchValue(product);
    this.isEditMode = true;
    this.selectedProductId = product.id;
  }

  deleteProduct(id: number) {
    this.posService.DeleteProduct(id).subscribe(() => this.loadProducts());
  }

  onFileChange(event: any) {
    this.selectedPhoto = event.target.files[0];
  }

  resetForm() {
    this.productForm.reset();
    this.isEditMode = false;
    this.selectedProductId = null;
    this.selectedPhoto = null;
  }
}
