import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PosService } from '../services/pos.service';

@Component({
  selector: 'app-stock',
  templateUrl: './stock.component.html',
  styleUrls: ['./stock.component.css']
})
export class StockComponent implements OnInit{
  stockForm!: FormGroup;
  stocks: any[] = [];
  suppliers: any[] = [];
  products: any[] = [];
  branches: any[] = [];

  isEditMode = false;
  selectedStockId: number | null = null;

  constructor(private fb: FormBuilder, private posService: PosService) {}

  ngOnInit(): void {
    this.initForm();
    this.loadStocks();
    this.loadSuppliers();
    this.loadProducts();
    this.loadBranches();
  }

  initForm() {
    this.stockForm = this.fb.group({
      supplierId: ['', Validators.required],
      productId: ['', Validators.required],
      quantity: [1, [Validators.required, Validators.min(1)]],
      branchesId: ['', Validators.required],
      purchaseDate: ['', Validators.required],
      totalAmount: [{ value: 0, disabled: true }]
    });
  }

  loadStocks() {
    this.posService.GetStocks().subscribe((data: any) => {
      this.stocks = data;
    });
  }

  loadSuppliers() {
    this.posService.GetAllSupplier().subscribe((data: any) => {
      this.suppliers = data;
    });
  }

  loadProducts() {
    this.posService.GetAllProduct().subscribe((data: any) => {
      this.products = data;
    });
  }

  loadBranches() {
    this.posService.GetAllBranches().subscribe((data: any) => {
      this.branches = data;
    });
  }

  calculateTotalAmount() {
    const quantity = this.stockForm.get('quantity')?.value || 0;
    const productId = this.stockForm.get('productId')?.value;
    const product = this.products.find(p => p.id === productId);
    const unitPrice = product?.price || 0;
    const total = quantity * unitPrice;
    this.stockForm.get('totalAmount')?.setValue(total);
  }

  onSubmit() {
    if (this.stockForm.invalid) return;

    const formData = {
      ...this.stockForm.getRawValue() // include disabled fields like totalAmount
    };

    if (this.isEditMode && this.selectedStockId !== null) {
      this.posService.UpdateStock(this.selectedStockId, formData).subscribe(() => {
        this.resetForm();
        this.loadStocks();
      });
    } else {
      this.posService.CreateStock(formData).subscribe(() => {
        this.resetForm();
        this.loadStocks();
      });
    }
  }

  editStock(stock: any) {
    this.isEditMode = true;
    this.selectedStockId = stock.id;
    this.stockForm.patchValue({
      supplierId: stock.supplierId,
      productId: stock.productId,
      quantity: stock.quantity,
      branchesId: stock.branchesId,
      purchaseDate: stock.purchaseDate.substring(0, 10),
      totalAmount: stock.totalAmount
    });
  }

  deleteStock(id: number) {
    this.posService.DeleteStock(id).subscribe(() => {
      this.loadStocks();
    });
  }

  resetForm() {
    this.stockForm.reset();
    this.isEditMode = false;
    this.selectedStockId = null;
  }

  getSupplierName(id: number) {
    return this.suppliers.find(s => s.id === id)?.companyName || 'N/A';
  }

  getProductName(id: number) {
    return this.products.find(p => p.id === id)?.name || 'N/A';
  }

  getBranchName(id: number) {
    return this.branches.find(b => b.id === id)?.name || 'N/A';
  }
}
