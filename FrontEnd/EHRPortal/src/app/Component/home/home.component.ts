import { Component, inject } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { MatIconTestingModule } from '@angular/material/icon/testing';
import { MatMenuModule } from '@angular/material/menu';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    RouterOutlet,RouterLink,CommonModule,MatIconTestingModule,MatIconModule,MatMenuModule
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  dropdownOpen = false;


  router = inject(Router)
  onLogOut(){
    localStorage.removeItem('token');
    this.router.navigateByUrl('login');
  }

  toggleDropdown() { this.dropdownOpen = !this.dropdownOpen; }
}
