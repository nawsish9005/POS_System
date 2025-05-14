import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PosService } from '../services/pos.service';
import { HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-assign-role',
  templateUrl: './assign-role.component.html',
  styleUrls: ['./assign-role.component.css']
})
export class AssignRoleComponent implements OnInit {
  assignRoleForm!: FormGroup;
  allRoles: { id: string, name: string }[] = [];
  allAssignedRoles: any[] = [];
  allUsers: string[] = [];
  message = '';
  error = '';
  isSubmitting = false;

  constructor(private fb: FormBuilder, private posService: PosService) {}

  ngOnInit(): void {
    this.assignRoleForm = this.fb.group({
      userName: ['', Validators.required],
      role: ['', Validators.required]
    });

    this.getRoles();
    this.getAssignedRoles();
    this.getUsers();
  }

  getRoles() {
    this.posService.getAllRoles().subscribe(response => {
      this.allRoles = response.map((r: any) => {
        // Normalize in case of nested structure
        return {
          id: r?.id?.id || r.id,
          name: r?.name?.name || r.name
        };
      });
    });
  }

  getAssignedRoles() {
    this.posService.getAllAssignedRoles().subscribe(data => {
      this.allAssignedRoles = data;
    });
  }

  getUsers() {
    this.posService.getAllUsers().subscribe(data => {
      this.allUsers = data.map((u: any) => u.userName);
    });
  }

  assignRole() {
    if (this.assignRoleForm.invalid) return;

    const payload = {
      userName: this.assignRoleForm.value.userName,
      role: this.assignRoleForm.value.role
    };

    console.log('Assign Role Payload:', payload);
    this.isSubmitting = true;

    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });

    this.posService.assignRole(payload).subscribe({
      next: () => {
        this.message = 'Role assigned successfully';
        this.error = '';
        this.assignRoleForm.reset();
        this.getAssignedRoles();
        this.isSubmitting = false;
      },
      error: (err) => {
        console.error('Assign Role Error:', err);
        this.error = 'Failed to assign role';
        this.message = '';
        this.isSubmitting = false;
      }
    });
  }

  updateAssignedRole(userName: string, roles: string[]) {
    this.posService.updateAssignedRole({ userName, roles }).subscribe(() => {
      this.getAssignedRoles();
    });
  }

  deleteAssignedRole(userName: string, role: string) {
    this.posService.deleteAssignedRole({ userName, role }).subscribe(() => {
      this.getAssignedRoles();
    });
  }
}
