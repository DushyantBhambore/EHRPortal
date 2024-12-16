import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, inject, OnInit, signal } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { LoginService } from '../../Service/login.service';
import { Router, RouterLink } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatButtonModule,
    MatInputModule,
    MatFormFieldModule,
    MatIconModule,
    RouterLink


  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  showLoginForm = false;
  showPatientLoginForm = false;
  showProviderLoginForm = false;
  isLoginSuccessful = false;
  loginform: FormGroup = new FormGroup({});
verifytform: FormGroup = new FormGroup({});
username: string = '';
service = inject(LoginService);
router = inject(Router);
toastr = inject(ToastrService);
isLoading = signal(false); // To show a loading state

constructor(private fb: FormBuilder) {}

ngOnInit(): void {
 this.setOtpForm();
 this.setForm();
//   this.sanitizeField("username")
//   this.sanitizeField("password")
// }
}
setForm() {
 this.loginform = this.fb.group({
   username: [''],
   password: [''],
 });
}

setOtpForm() {
 const username = this.loginform.get('username')?.value;
 this.username = username; // Save the username for OTP verification
 this.verifytform = this.fb.group({
   username: [username],
   otp: ['']
 });
}



// onLogin() {
//   this.service.onLogin(this.loginform.value).subscribe({
//     next: (res) => {
//       console.log(res);
//       this.setOtpForm();
//       this.isLoginSuccessful = true; 
//       this.toastr.success('OTP sent successfully', 'Success', {
//         timeOut: 3000,
//         progressBar: true,
//         progressAnimation: 'increasing',
//         positionClass: 'toast-top-right',
//       });
//     },
//     error: (err) => {
//       console.log(err);
//       this.toastr.error('Login failed', 'Error', {
//         timeOut: 3000,
//         progressBar: true,
//         progressAnimation: 'increasing',
//         positionClass: 'toast-top-right'
//       });
//     }
//   });
// }

// onOTP() {
//   this.service.onVerifyOtp(this.verifytform.value).subscribe({
//     next: (res:any) => {
//       console.log(res);
//       localStorage.setItem('token', res.token);
//       localStorage.setItem('profileimage ',res.data.imageFile);
//       this.router.navigateByUrl('/dashboard');
//       this.toastr.success('Login successful', 'Success', {
//         timeOut: 3000,
//         progressBar: true,
//         progressAnimation: 'increasing',
//         positionClass: 'toast-top-right'
//       });
//     },
//     error: (err) => {
//       console.log(err);
//       this.toastr.error('OTP verification failed', 'Error', {
//         timeOut: 3000,
//         progressBar: true,
//         progressAnimation: 'increasing',
//         positionClass: 'toast-top-right'
//       });
//     }
//   });
// }

showLogin(type: string) {
  this.showLoginForm = true;
  if (type === 'patient') {
    this.showPatientLoginForm = true;
    this.showProviderLoginForm = false;
  } else if (type === 'provider') {
    this.showProviderLoginForm = true;
    this.showPatientLoginForm = false;
  }
}


onLogin() {

 debugger
 if(this.loginform.invalid){
   this.loginform.markAllAsTouched();
   this.toastr.error('Please enter valid data', 'error')
   return;
 }


 this.service.login(this.loginform.value).subscribe({
   next: (res :any) => {
debugger
     if(res.statusCode == 200)
     { this.isLoginSuccessful=true
       debugger
       this.toastr.success('OTP sent successfully', 'Success', {
         timeOut: 3000,
         progressBar: true,
         progressAnimation: 'increasing',
         positionClass: 'toast-top-right',
       });
       sessionStorage.setItem('loginuser', JSON.stringify(res.data));
       this.isLoginSuccessful = true;
       this.setOtpForm()

     }
     else{
       this.toastr.error('Wrong Creadentials Enter Coreect Credential', 'Error', {
         timeOut: 3000,
         progressBar: true,
         progressAnimation: 'increasing',
         positionClass: 'toast-top-right'
       });
     }
   },
   error: (err) => {
     this.toastr.error('Login failed', 'Error', {
       timeOut: 3000,
       progressBar: true,
       progressAnimation: 'increasing',
       positionClass: 'toast-top-right'
     });
   }
 });
}

onOTP() {
 
 this.service.verifyotp(this.verifytform.value).subscribe({
   next: (res: any) => {
     localStorage.setItem('token', res.token);
     localStorage.setItem('profileimage', res.data.imageFile);
     sessionStorage.setItem('logindata', JSON.stringify(res.data));
     sessionStorage.setItem('role',JSON.stringify(res.data.roleId))
     this.toastr.success('Login successful', 'Success', {
       timeOut: 3000,
       progressBar: true,
       progressAnimation: 'increasing',
       positionClass: 'toast-top-right'
     });

      if(res.data.userTypeId == 1)
      {
     this.router.navigateByUrl('/provider-dashboard');

      }
      else{
     this.router.navigateByUrl('/patient-dashboard');
      }
   },
   error: (err) => {
     this.toastr.error('OTP verification failed', 'Error', {
       timeOut: 3000,
       progressBar: true,
       progressAnimation: 'increasing',
       positionClass: 'toast-top-right'
     });
   }
 });
}
sanitizeField(fieldName: string): void {
 this.loginform.get(fieldName)?.valueChanges.subscribe((value) => {
   if (value) {
     // Allow trailing spaces, but clean invalid characters and reduce multiple spaces
     const sanitizedValue = value
       .replace(/[^A-Za-z\s]/g, '') // Remove non-letters and non-spaces
       .replace(/\s{2,}/g, ' ').trimO()
       
       // Replace multiple spaces with a single space (excluding trailing)
     if (value !== sanitizedValue) {
       this.loginform.get(fieldName)?.setValue(sanitizedValue, {
         emitEvent: false, // Avoid recursive calls
       });
     }
   }
 });
}


hide = signal(true);
clickEvent(event: MouseEvent) {
 this.hide.set(!this.hide());
 event.stopPropagation();
}

}
