
import { Component, inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { LoginService } from '../../Service/login.service';
import { Router, RouterLink } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-forgotpassword',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatButtonModule,
    MatInputModule,
    MatFormFieldModule,
    MatIconModule,
    RouterLink,
    FormsModule



  ],
  templateUrl: './forgotpassword.component.html',
  styleUrl: './forgotpassword.component.css'
})
export class ForgotpasswordComponent {

  service = inject(LoginService)

  toastr = inject(ToastrService)
  router = inject(Router)
  


  forgotPasswordForm={
    email:''
  }


  forgotpassword()
  {
    console.log(this.forgotPasswordForm);
    debugger
    this.service.forgotpassword(this.forgotPasswordForm).subscribe(
      {
        
        next: (res) => {
          console.log(res);
          this.toastr.success('Password Forgot successfully! Check your email' , 'Success', {
            timeOut: 3000,
            progressBar: true,
            progressAnimation: 'increasing',
            positionClass: 'toast-top-right'
          });
          this.router.navigateByUrl('/login');
        }
        , error: (err) => {
          console.log(err);
          this.toastr.error('Failed to register user. Please check the data and try again.', 'Error', {
            timeOut: 3000,
            progressBar: true,
            progressAnimation: 'increasing',
            positionClass: 'toast-top-right'
          });
        }

      })
  }




}
