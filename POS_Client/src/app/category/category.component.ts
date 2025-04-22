import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PosService } from '../services/pos.service';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css'],
})
export class CategoryComponent implements OnInit {
  categoryForm: FormGroup;
  categories: any[] = [];
  isEditing = false;
  isEditMode = false;
  selectedCategoryId: number | null = null;

  constructor(private fb: FormBuilder, private posService: PosService) {
    this.categoryForm = this.fb.group({
      id: [0], // Add this
      categoryName: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.getCategories(); // Load categories on init
  }

  getCategories(): void {
    this.posService.GetAllCategory().subscribe({
      next: (res: any) => {
        this.categories = res; // Store the response data
      },
      error: (err) => {
        console.error('Error fetching categories', err);
      },
    });
  }

  onSubmit(): void {
    const category = this.categoryForm.value;

    if (this.isEditMode && this.selectedCategoryId) {
      this.posService.UpdateCategory(this.selectedCategoryId, category).subscribe({
        next: () => {
          this.resetForm();
          this.getCategories();
        },
        error: (err) => console.error('Error updating category', err),
      });
    } else {
      this.posService.CreateCategory(category).subscribe({
        next: () => {
          this.resetForm();
          this.getCategories();
        },
        error: (err) => console.error('Error creating category', err),
      });
    }
  }

  onEdit(category: any): void {
    this.categoryForm.patchValue({ categoryName: category.categoryName });
    this.selectedCategoryId = category.id;
    this.isEditing = true;
  }

  editCategory(category: any): void {
    this.isEditMode = true;
    this.categoryForm.patchValue({
      id: category.id,
      categoryName: category.categoryName
    });
    this.selectedCategoryId = category.id;
  }
  

  deleteCategory(id: number): void {
    if (confirm('Are you sure you want to delete this category?')) {
      this.posService.DeleteCategory(id).subscribe({
        next: () => this.getCategories(),
        error: (err) => console.error('Error deleting category', err),
      });
    }
  }

  resetForm(): void {
    this.categoryForm.reset();
    this.isEditing = false;
    this.selectedCategoryId = null;
  }
}
