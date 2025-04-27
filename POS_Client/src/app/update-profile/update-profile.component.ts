import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PosService } from '../services/pos.service';

@Component({
  selector: 'app-update-profile',
  templateUrl: './update-profile.component.html',
  styleUrls: ['./update-profile.component.css']
})
export class UpdateProfileComponent implements OnInit{
  profileForm: FormGroup;
  successMessage: string = '';
  errorMessage: string = '';
  isLoading: boolean = false;

  constructor(private fb: FormBuilder, private posService: PosService) {
    this.profileForm = this.fb.group({
      userName: ['', [Validators.required, Validators.minLength(3)]],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', [Validators.required, Validators.pattern(/^(\+88|0088)?(01[3-9]\d{8})$/)]],
      fullName: ['', [Validators.required, Validators.minLength(3)]]
    });
  }

  ngOnInit(): void {
    this.loadProfileData();
  }

  // Load the current user profile data
  private loadProfileData(): void {
    this.isLoading = true;
    this.posService.GetProfile().subscribe({
      next: (data) => {
        this.profileForm.patchValue(data);
        this.isLoading = false;
      },
      error: (err) => {
        this.errorMessage = 'Error loading profile';
        this.isLoading = false;
        console.error('Error loading profile', err);
      }
    });
  }

  // Update the profile
  onSubmit(): void {
    if (this.profileForm.invalid) {
      return;
    }

    this.isLoading = true;
    const updatedProfile = this.profileForm.value;

    this.posService.updateProfile(updatedProfile).subscribe({
      next: (res) => {
        this.successMessage = 'Profile updated successfully!';
        this.errorMessage = ''; // Clear any previous error message
        this.isLoading = false;
      },
      error: (err) => {
        this.errorMessage = 'Error updating profile';
        this.successMessage = ''; // Clear any previous success message
        this.isLoading = false;
        console.error('Error updating profile', err);
      }
    });
  }

  // Getter for easy form control access
  get f() {
    return this.profileForm.controls;
  }
}
