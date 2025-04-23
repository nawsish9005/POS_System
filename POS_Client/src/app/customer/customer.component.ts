import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PosService } from '../services/pos.service'; // Adjust path if needed

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.css']
})
export class CustomerComponent implements OnInit {
  customers: any = [];
  customerForm!: FormGroup;
  isEditMode = false;
  selectedCustomerId: number | null = null;

  constructor(
    private fb: FormBuilder,
    private posService: PosService
  ) {}

  ngOnInit(): void {
    this.initForm();
    this.getCustomers();
  }

  initForm() {
    this.customerForm = this.fb.group({
      fullName: ['', Validators.required],
      address: ['', Validators.required],
      phone: ['', [Validators.required, Validators.pattern('^[0-9]{11}$')]],
      email: ['', [Validators.required, Validators.email]]
    });
  }

  getCustomers(): void {
    this.posService.GetAllCustomer().subscribe({
      next: (data) => {
        this.customers = data;
      },
      error: (err) => {
        console.error('Error loading customers', err);
      }
    });
  }

  onSubmit() {
    if (this.customerForm.invalid) return;

    const customerData = this.customerForm.value;

    if (this.isEditMode && this.selectedCustomerId !== null) {
      this.posService.UpdateCustomer(this.selectedCustomerId, { id: this.selectedCustomerId, ...customerData })
        .subscribe({
          next: () => {
            this.getCustomers();
            this.resetForm();
          },
          error: err => console.error('Error updating customer', err)
        });
    } else {
      this.posService.CreateCustomer(customerData)
        .subscribe({
          next: () => {
            this.getCustomers();
            this.resetForm();
          },
          error: err => console.error('Error creating customer', err)
        });
    }
  }

  editCustomer(customer: any) {
    this.customerForm.patchValue(customer);
    this.isEditMode = true;
    this.selectedCustomerId = customer.id;
  }

  deleteCustomer(id: number) {
    if (confirm('Are you sure you want to delete this customer?')) {
      this.posService.DeleteCustomer(id).subscribe({
        next: () => this.getCustomers(),
        error: err => console.error('Error deleting customer', err)
      });
    }
  }

  resetForm() {
    this.customerForm.reset();
    this.isEditMode = false;
    this.selectedCustomerId = null;
  }
}
