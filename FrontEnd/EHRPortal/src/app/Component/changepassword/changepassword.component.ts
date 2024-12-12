import { Component, inject, signal } from '@angular/core';
import { LoginService } from '../../Service/login.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-changepassword',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatButtonModule,
    MatInputModule,
    MatFormFieldModule,
    MatIconModule,
    FormsModule

  ],
  templateUrl: './changepassword.component.html',
  styleUrl: './changepassword.component.css'
})
export class ChangepasswordComponent {

  service = inject(LoginService)

   usernamedata =  JSON.parse(sessionStorage.getItem('logindata') || '{}')
  toastr = inject(ToastrService)
  router = inject(Router)
  
  changePaswordForm ={
    username :this.usernamedata.username,
    newPassword:  '',
    confirmPassword: ''
  }
  // changePaswordForm = new FormGroup({
  //   username : new FormControl('', [Validators.required,]),
  //   newPassword: new FormControl('', [Validators.required,]),
  // })

  changePassword() {
    debugger
    if (this.changePaswordForm.newPassword !== this.changePaswordForm.confirmPassword) {
      this.toastr.error("New password and confirm password do not match", "Error");
    }
    this.service.changepassword(this.changePaswordForm).subscribe(
      {
        next: (res : any) => {
          debugger
          if(res.statusCode === 400)
          {
            this.toastr.error("Passsword is not Matched  data", res.message);
          }
          else{
            debugger
          console.log(res);
          this.toastr.success('Password Changed Successfully', 'Success', {
            timeOut: 3000,
            progressBar: true,
            progressAnimation: 'increasing',
            positionClass: 'toast-top-right'
          });
        }
        },
        error: (err) => {
          console.log(err);
          this.toastr.error('Password Not Changed', 'Error', {
            timeOut: 3000,
            progressBar: true,
            progressAnimation: 'increasing',
            positionClass: 'toast-top-right'
          });
        }
      })
  }

  hide = signal(true);
  clickEvent(event: MouseEvent) {
    this.hide.set(!this.hide());
    event.stopPropagation();
  }
}
