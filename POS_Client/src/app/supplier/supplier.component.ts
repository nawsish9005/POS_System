import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PosService } from '../services/pos.service';

@Component({
  selector: 'app-supplier',
  templateUrl: './supplier.component.html',
  styleUrls: ['./supplier.component.css']
})
export class SupplierComponent {
  supplierForm!: FormGroup;
  suppliers: any = [];
  isEditMode = false;
  selectedSupplierId: number | null = null;

  constructor(private fb: FormBuilder, private posService: PosService) {}

  ngOnInit(): void {
    this.initForm();
    this.getSuppliers();
  }

  initForm() {
    this.supplierForm = this.fb.group({
      companyName: ['', Validators.required],
      contactName: ['', Validators.required],
      phone: ['', [Validators.required, Validators.pattern('^[0-9]{11}$')]],
      email: ['', [Validators.required, Validators.email]],
      address: ['']
    });
  }

  getSuppliers() {
    this.posService.GetAllSupplier().subscribe(
      (data: any) => {
        this.suppliers = data;
      },
      (error) => {
        console.error('Error fetching suppliers:', error);
      }
    );
  }
  

  onSubmit() {
    if (this.supplierForm.invalid) return;

    const supplierData = this.supplierForm.value;

    if (this.isEditMode && this.selectedSupplierId !== null) {
      this.posService.UpdateSupplier(this.selectedSupplierId, {
        id: this.selectedSupplierId,
        ...supplierData
      }).subscribe(() => {
        this.getSuppliers();
        this.resetForm();
      });
    } else {
      this.posService.CreateSupplier(supplierData).subscribe(() => {
        this.getSuppliers();
        this.resetForm();
      });
    }
  }

  editSupplier(supplier: any) {
    this.supplierForm.patchValue(supplier);
    this.selectedSupplierId = supplier.id;
    this.isEditMode = true;
  }

  deleteSupplier(id: number) {
    this.posService.DeleteSupplier(id).subscribe(() => {
      this.getSuppliers();
    });
  }

  resetForm() {
    this.supplierForm.reset();
    this.selectedSupplierId = null;
    this.isEditMode = false;
  }
}
