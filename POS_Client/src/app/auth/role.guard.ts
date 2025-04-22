import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot): boolean {
    const expectedRoles = route.data['roles'] as Array<string>;
    const userRoles = this.authService.getUserRoles();

    if (!this.authService.isAuthenticated() || !expectedRoles.some(role => userRoles.includes(role))) {
      this.router.navigate(['/login']);
      return false;
    }

    return true;
  }
}
