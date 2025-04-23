import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PosService } from '../services/pos.service';

@Component({
  selector: 'app-branches',
  templateUrl: './branches.component.html',
  styleUrls: ['./branches.component.css']
})
export class BranchesComponent implements OnInit{
  branches: any = [];
  branchForm!: FormGroup;
  isEditMode = false;
  selectedBranchId: number | null = null;

  constructor(private fb: FormBuilder, private posService: PosService) {}

  ngOnInit(): void {
    this.initForm();
    this.getBranches();
  }

  initForm() {
    this.branchForm = this.fb.group({
      branchName: ['', Validators.required],
      location: ['', Validators.required]
    });
  }

  getBranches() {
    this.posService.GetAllBranches().subscribe({
      next: (data) => this.branches = data,
      error: (err) => console.error('Error loading branches', err)
    });
  }

  onSubmit() {
    if (this.branchForm.invalid) return;

    const branchData = this.branchForm.value;

    if (this.isEditMode && this.selectedBranchId !== null) {
      this.posService.UpdateBranches(this.selectedBranchId, { id: this.selectedBranchId, ...branchData })
        .subscribe(() => {
          this.getBranches();
          this.resetForm();
        });
    } else {
      this.posService.CreateBranches(branchData)
        .subscribe(() => {
          this.getBranches();
          this.resetForm();
        });
    }
  }

  editBranch(branch: any) {
    this.branchForm.patchValue(branch);
    this.isEditMode = true;
    this.selectedBranchId = branch.id;
  }

  deleteBranch(id: number) {
    this.posService.DeleteBranches(id).subscribe(() => this.getBranches());
  }

  resetForm() {
    this.branchForm.reset();
    this.isEditMode = false;
    this.selectedBranchId = null;
  }
}
