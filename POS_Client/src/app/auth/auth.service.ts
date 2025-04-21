// src/app/auth/auth.service.ts
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl = 'https://localhost:7293/api/account/'; // Change if needed

  constructor(private http: HttpClient) {}
  public registrationUrl = "register";

  register(model: any): Observable<any> {
    return this.http.post(this.baseUrl + this.registrationUrl, model);
  }

  public loginUrl = "login";

  login(model: any): Observable<any> {
    return this.http.post(this.baseUrl + this.loginUrl, model);
  }

  logout(): void {
    localStorage.removeItem('token');
  }

  isAuthenticated(): boolean {
    return !!localStorage.getItem('token');
  }
    getToken(): string | null {
        return localStorage.getItem('token');
    }
}
