import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PosService } from '../services/pos.service';

@Component({
  selector: 'app-tax',
  templateUrl: './tax.component.html',
  styleUrls: ['./tax.component.css']
})
export class TaxComponent implements OnInit{
  taxForm!: FormGroup;
  taxes: any[] = [];
  isEditMode = false;
  selectedTaxId: number | null = null;

  constructor(private fb: FormBuilder, private posService: PosService) {}

  ngOnInit(): void {
    this.initForm();
    this.loadTaxes();
  }

  initForm(): void {
    this.taxForm = this.fb.group({
      name: ['', Validators.required],
      rate: [0, [Validators.required, Validators.min(0), Validators.max(100)]]
    });
  }

  loadTaxes(): void {
    this.posService.GetTaxes().subscribe(
      (data: any) => this.taxes = data,
      error => console.error('Failed to load taxes', error)
    );
  }

  onSubmit(): void {
    if (this.taxForm.invalid) return;

    const taxDto = this.taxForm.value;

    if (this.isEditMode && this.selectedTaxId !== null) {
      this.posService.UpdateTax(this.selectedTaxId, taxDto).subscribe(() => {
        this.loadTaxes();
        this.resetForm();
      });
    } else {
      this.posService.CreateTax(taxDto).subscribe(() => {
        this.loadTaxes();
        this.resetForm();
      });
    }
  }

  editTax(tax: any): void {
    this.isEditMode = true;
    this.selectedTaxId = tax.id;

    this.taxForm.patchValue({
      name: tax.name,
      rate: tax.rate
    });
  }

  deleteTax(id: number): void {
    this.posService.DeleteTax(id).subscribe(() => this.loadTaxes());
  }

  resetForm(): void {
    this.taxForm.reset({ rate: 0 });
    this.isEditMode = false;
    this.selectedTaxId = null;
  }
}
