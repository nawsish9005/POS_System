import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PosService } from '../services/pos.service';

@Component({
  selector: 'app-assign-role',
  templateUrl: './assign-role.component.html',
  styleUrls: ['./assign-role.component.css']
})
export class AssignRoleComponent implements OnInit{
  assignRoleForm!: FormGroup;
  allRoles: { id: string, name: string }[] = [];
  allAssignedRoles: any[] = [];
  allUsers: string[] = [];
  message = '';
  error = '';

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
        // If roles are wrapped in 'id' and 'name', unwrap them:
        if (r.id && r.name && r.id.id && r.name.name) {
          return {
            id: r.id.id,
            name: r.name.name
          };
        }
        // Fallback if response is normal
        return {
          id: r.id,
          name: r.name
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
      this.allUsers = data.map(u => u.userName);
    });
  }

  assignRole() {
    if (this.assignRoleForm.invalid) return;

    this.posService.assignRole(this.assignRoleForm.value).subscribe({
      next: () => {
        this.message = 'Role assigned successfully';
        this.error = '';
        this.assignRoleForm.reset();
        this.getAssignedRoles();
      },
      error: err => {
        this.error = 'Failed to assign role';
        this.message = '';
      }
    });
  }

  deleteAssignedRole(userName: string, role: string) {
    this.posService.deleteAssignedRole({ userName, role }).subscribe(() => {
      this.getAssignedRoles();
    });
  }
}
