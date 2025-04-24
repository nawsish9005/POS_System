import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PosService } from '../services/pos.service';

@Component({
  selector: 'app-discount',
  templateUrl: './discount.component.html',
  styleUrls: ['./discount.component.css']
})
export class DiscountComponent implements OnInit{
  discountForm!: FormGroup;
  discounts: any[] = [];
  isEditMode = false;
  selectedDiscountId: number | null = null;

  constructor(private fb: FormBuilder, private posService: PosService) {}

  ngOnInit(): void {
    this.initForm();
    this.loadDiscounts();
  }

  initForm(): void {
    this.discountForm = this.fb.group({
      name: ['', Validators.required],
      percentage: [0, [Validators.required, Validators.min(0), Validators.max(100)]],
      isActive: [true]
    });
  }

  loadDiscounts(): void {
    this.posService.GetAllDiscount().subscribe(
      (data: any) => this.discounts = data,
      error => console.error('Failed to load discounts', error)
    );
  }

  onSubmit(): void {
    if (this.discountForm.invalid) return;

    const discountDto = this.discountForm.value;

    if (this.isEditMode && this.selectedDiscountId !== null) {
      this.posService.UpdateDiscount(this.selectedDiscountId, discountDto).subscribe(() => {
        this.loadDiscounts();
        this.resetForm();
      });
    } else {
      this.posService.CreateDiscount(discountDto).subscribe(() => {
        this.loadDiscounts();
        this.resetForm();
      });
    }
  }

  editDiscount(discount: any): void {
    this.isEditMode = true;
    this.selectedDiscountId = discount.id;

    this.discountForm.patchValue({
      name: discount.name,
      percentage: discount.percentage,
      isActive: discount.isActive
    });
  }

  deleteDiscount(id: number): void {
    this.posService.DeleteDiscount(id).subscribe(() => this.loadDiscounts());
  }

  resetForm(): void {
    this.discountForm.reset({ isActive: true });
    this.isEditMode = false;
    this.selectedDiscountId = null;
  }
}
