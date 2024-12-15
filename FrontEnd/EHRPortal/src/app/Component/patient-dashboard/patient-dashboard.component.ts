import { Component, inject, OnInit } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { MatIconTestingModule } from '@angular/material/icon/testing';
import { MatMenuModule } from '@angular/material/menu';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-patient-dashboard',
  standalone: true,
  imports: [
    RouterOutlet,RouterLink,CommonModule,MatIconTestingModule,MatIconModule,MatMenuModule
  ],
  templateUrl: './patient-dashboard.component.html',
  styleUrl: './patient-dashboard.component.css'
})
export class PatientDashboardComponent {

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
