import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { jwtDecode } from 'jwt-decode';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private baseUrl = 'https://localhost:7293/api/account/';

  constructor(private http: HttpClient, private router: Router) {}

  // Register user
  register(model: any): Observable<any> {
    return this.http.post(`${this.baseUrl}register`, model);
  }

  // Login user and save the JWT token in localStorage
  login(model: any): Observable<any> {
    return this.http.post(`${this.baseUrl}login`, model);
  }

  // Logout by removing token from localStorage
  logout(): void {
    // Remove the token from localStorage or sessionStorage
    localStorage.removeItem('token');
    console.log(localStorage.getItem('token')); // Should log 'null'


    // Optionally, navigate to the login page
    this.router.navigate(['/login']);
  }

  // Check if the user is authenticated
  isAuthenticated(): boolean {
    const token = localStorage.getItem('token');
    return !!token && this.isTokenValid(token); // Added token validation check
  }

  // Get the JWT token from localStorage
  getToken(): string | null {
    return localStorage.getItem('token');
  }

  // Save JWT token to localStorage
  saveToken(token: string): void {
    localStorage.setItem('token', token);
  }

  // Get user roles from the token
  getUserRoles(): string[] {
    const token = this.getToken();
    if (!token) return [];
    const decoded: any = jwtDecode(token);
    const roles = decoded['role'];
    return Array.isArray(roles) ? roles : roles ? [roles] : [];
  }

  // Check if a token is valid (basic validation)
  private isTokenValid(token: string): boolean {
    try {
      const decoded: any = jwtDecode(token);
      return decoded && decoded.exp * 1000 > Date.now(); // Check expiration date
    } catch (e) {
      return false;
    }
  }

  // Add Authorization header with Bearer token
  getAuthHeaders(): HttpHeaders {
    const token = this.getToken();
    return token
      ? new HttpHeaders().set('Authorization', `Bearer ${token}`)
      : new HttpHeaders();
  }
}
