import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PosService } from '../services/pos.service';

@Component({
  selector: 'app-purchase',
  templateUrl: './purchase.component.html',
  styleUrls: ['./purchase.component.css']
})
export class PurchaseComponent implements OnInit{
  purchaseForm!: FormGroup;
  purchases: any = [];
  suppliers: any = [];
  products: any = [];
  isEditMode = false;
  selectedPurchaseId: number | null = null;

  constructor(private fb: FormBuilder, private posService: PosService) {}

  ngOnInit(): void {
    this.initForm();
    this.loadPurchases();
    this.loadSuppliers();
    this.loadProducts();  // Load products when the component initializes
  }

  loadProducts() {
    this.posService.GetAllProduct().subscribe((data: any) => {
      this.products = data;
    });
  }

  initForm() {
    this.purchaseForm = this.fb.group({
      supplierId: ['', Validators.required],
      purchaseDate: ['', Validators.required],
      totalAmount: [0, Validators.required],
      purchaseItems: this.fb.array([])
    });
  }

  get purchaseItems(): FormArray {
    return this.purchaseForm.get('purchaseItems') as FormArray;
  }

  getSupplierName(supplierId: number): string {
    const supplier = this.suppliers.find((s: any) => s.id === supplierId);
    return supplier ? supplier.companyName : 'Unknown';
  }
  
  findSupplierNameById(supplierId: number): string | undefined {
    const supplier = this.suppliers.find((s:any) => s.id === supplierId);
    return supplier?.companyName;
  }  
  
  addItem() {
    this.purchaseItems.push(
      this.fb.group({
        productId: ['', Validators.required],
        quantity: [1, Validators.required],
        unitPrice: [0, Validators.required]
      })
    );
  }

  removeItem(index: number) {
    this.purchaseItems.removeAt(index);
    this.calculateTotalAmount();
  }

  calculateTotalAmount() {
    const total = this.purchaseItems.controls.reduce((sum, item) => {
      const quantity = item.get('quantity')?.value || 0;
      const price = item.get('unitPrice')?.value || 0;
      return sum + (quantity * price);
    }, 0);
    this.purchaseForm.get('totalAmount')?.setValue(total);
  }

  loadPurchases() {
    this.posService.GetPurchases().subscribe((data: any) => {
      this.purchases = data;
    });
  }

  loadSuppliers() {
    this.posService.GetAllSupplier().subscribe((data: any) => {
      this.suppliers = data;
    });
  }

  onSubmit() {
    if (this.purchaseForm.invalid) return;

    const purchaseData = this.purchaseForm.value;

    if (this.isEditMode && this.selectedPurchaseId !== null) {
      this.posService.UpdatePurchase(this.selectedPurchaseId, purchaseData).subscribe(() => {
        this.resetForm();
        this.loadPurchases();
      });
    } else {
      this.posService.CreatePurchase(purchaseData).subscribe(() => {
        this.resetForm();
        this.loadPurchases();
      });
    }
  }

  editPurchase(purchase: any) {
    this.isEditMode = true;
    this.selectedPurchaseId = purchase.id;
    this.purchaseForm.patchValue({
      supplierId: purchase.supplierId,
      purchaseDate: purchase.purchaseDate.substring(0, 10),
      totalAmount: purchase.totalAmount
    });

    this.purchaseItems.clear();
    for (let item of purchase.purchaseItems) {
      this.purchaseItems.push(
        this.fb.group({
          productId: item.productId,
          quantity: item.quantity,
          unitPrice: item.unitPrice
        })
      );
    }
  }

  deletePurchase(id: number) {
    this.posService.DeletePurchase(id).subscribe(() => {
      this.loadPurchases();
    });
  }

  resetForm() {
    this.purchaseForm.reset();
    this.purchaseItems.clear();
    this.isEditMode = false;
    this.selectedPurchaseId = null;
  }
}
