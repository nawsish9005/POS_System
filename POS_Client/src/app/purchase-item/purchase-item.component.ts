import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PosService } from '../services/pos.service';

@Component({
  selector: 'app-purchase-item',
  templateUrl: './purchase-item.component.html',
  styleUrls: ['./purchase-item.component.css']
})
export class PurchaseItemComponent implements OnInit{
  purchaseItemForm!: FormGroup;
  purchaseItems: any[] = [];
  purchases: any[] = [];
  products: any[] = [];
  isEditMode = false;
  selectedId: number | null = null;

  constructor(private fb: FormBuilder, private posService: PosService) {}

  ngOnInit(): void {
    this.initForm();
    this.loadPurchaseItems();
    this.loadPurchases();
    this.loadProducts();
  }

  initForm() {
    this.purchaseItemForm = this.fb.group({
      purchaseId: ['', Validators.required],
      productId: ['', Validators.required],
      quantity: [1, Validators.required],
      unitCost: [0, Validators.required],
      subtotal: [{ value: 0, disabled: true }]
    });

    this.purchaseItemForm.valueChanges.subscribe(() => this.calculateSubtotal());
  }

  calculateSubtotal() {
    const quantity = this.purchaseItemForm.get('quantity')?.value || 0;
    const unitCost = this.purchaseItemForm.get('unitCost')?.value || 0;
    const subtotal = quantity * unitCost;
    this.purchaseItemForm.get('subtotal')?.setValue(subtotal, { emitEvent: false });
  }

  loadPurchaseItems() {
    this.posService.GetPurchaseItems().subscribe((data: any) => {
      this.purchaseItems = data;
    });
  }

  loadPurchases() {
    this.posService.GetStocks().subscribe((data: any) => {
      this.purchases = data;
    });
  }

  getProductName(productId: number): string {
    if (!this.products || this.products.length === 0) return '';
    const product = this.products.find((p: any) => p.id === productId);
    return product ? product.name : '';
  }  

  loadProducts() {
    this.posService.GetAllProduct().subscribe((data: any) => {
      this.products = data;
    });
  }

  onSubmit() {
    if (this.purchaseItemForm.invalid) return;

    const data = { ...this.purchaseItemForm.getRawValue() };

    if (this.isEditMode && this.selectedId !== null) {
      this.posService.UpdatePurchaseItem(this.selectedId, data).subscribe(() => {
        this.resetForm();
        this.loadPurchaseItems();
      });
    } else {
      this.posService.CreatePurchaseItem(data).subscribe(() => {
        this.resetForm();
        this.loadPurchaseItems();
      });
    }
  }

  edit(item: any) {
    this.isEditMode = true;
    this.selectedId = item.id;
    this.purchaseItemForm.patchValue(item);
    this.calculateSubtotal();
  }

  delete(id: number) {
    this.posService.DeletePurchaseItem(id).subscribe(() => {
      this.loadPurchaseItems();
    });
  }

  resetForm() {
    this.purchaseItemForm.reset({ quantity: 1, unitCost: 0, subtotal: 0 });
    this.isEditMode = false;
    this.selectedId = null;
  }
}
