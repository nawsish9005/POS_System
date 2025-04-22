import { Component } from '@angular/core';
import { AuthService } from '../auth/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {

  constructor(private authService: AuthService) { }

  // Add any methods or properties you need for the navbar here
  // For example, you might want to handle logout or toggle a sidebar
  logout() {
    this.authService.logout();
   
  }

}
