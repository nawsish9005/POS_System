import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.css']
})
export class CustomerComponent implements OnInit{
  customers: any[] = [];
  customerForm!: FormGroup;
  isEditMode = false;
  selectedCustomerId: number | null = null;

  constructor(private fb: FormBuilder, private http: HttpClient) {}

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
  getCustomers() {
    this.http.get<any[]>('https://localhost:7293/api/Customer')
      .subscribe({
        next: data => this.customers = data,
        error: err => console.error('Error loading customers', err)
      });
  }

  onSubmit() {
    if (this.customerForm.invalid) return;

    const customerData = this.customerForm.value;

    if (this.isEditMode && this.selectedCustomerId !== null) {
      this.http.put(`https://localhost:7293/api/Customer/${this.selectedCustomerId}`, { id: this.selectedCustomerId, ...customerData })
        .subscribe(() => {
          this.getCustomers();
          this.resetForm();
        });
    } else {
      this.http.post('https://localhost:7293/api/Customer', customerData)
        .subscribe(() => {
          this.getCustomers();
          this.resetForm();
        });
    }
  }

  editCustomer(customer: any) {
    this.customerForm.patchValue(customer);
    this.isEditMode = true;
    this.selectedCustomerId = customer.id;
  }

  deleteCustomer(id: number) {
    this.http.delete(`https://localhost:7293/api/Customer/${id}`)
      .subscribe(() => this.getCustomers());
  }

  resetForm() {
    this.customerForm.reset();
    this.isEditMode = false;
    this.selectedCustomerId = null;
  }
}
