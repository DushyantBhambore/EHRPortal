import { Component, inject, OnInit } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { MatIconTestingModule } from '@angular/material/icon/testing';
import { MatMenuModule } from '@angular/material/menu';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-provider-dashboard',
  standalone: true,
  imports: [
    RouterOutlet,RouterLink,CommonModule,MatIconTestingModule,MatIconModule,MatMenuModule
  ],
  templateUrl: './provider-dashboard.component.html',
  styleUrl: './provider-dashboard.component.css'
})
export class ProviderDashboardComponent implements OnInit {
  dropdownOpen = false;

  name = JSON.parse(sessionStorage.getItem('logindata') || '{}');
  profile = this.name.imageFile;

  ngOnInit(){
   console.log(this.profile)
  }

 router = inject(Router)
 onLogOut(){
   localStorage.removeItem('token');
   this.router.navigateByUrl('login');
 }

 toggleDropdown() { this.dropdownOpen = !this.dropdownOpen; }

}
