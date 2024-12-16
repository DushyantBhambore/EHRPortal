import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginService } from '../../Service/login.service';
import { ToastrService } from 'ngx-toastr';
import { CommonModule } from '@angular/common';
import {MatButtonModule} from '@angular/material/button';
import { MatDialog } from '@angular/material/dialog';

import { PatientDialogComponent } from '../patient-dialog/patient-dialog.component';

@Component({
  selector: 'app-patient-profile',
  standalone: true,
  imports: [ReactiveFormsModule,
    CommonModule,
    MatButtonModule

  ],
  templateUrl: './patient-profile.component.html',
  styleUrl: './patient-profile.component.css'
})
export class PatientProfileComponent {

  patientform : FormGroup = new FormGroup({});
    imageFile: File | null = null;
    countries: any;
    states: any;
    roles: any;
    userObj: any;
    private router = inject(Router);
    private service = inject(LoginService);
    private toastr = inject(ToastrService);
    patientdata = JSON.parse(sessionStorage.getItem('logindata') || '{}');
    mode!: string;
    userId!: number;
    todayDate=new Date().toISOString().split('T')[0];
  
    selectedDate = new Date();
  
    constructor(private fb: FormBuilder, private dialog: MatDialog)  
    {

    }
  
    

    setform()
    {
      this.patientform = this.fb.group({
        firstName: ['', [Validators.minLength(2), 
          Validators.maxLength(20),Validators.pattern(/^[A-Za-z]+(?: [A-Za-z]+)*\s*$/)]],
        lastName: ['', []],
        email: ['', [ Validators.email]],
        mobile: ['', [ Validators.pattern('^[0-9]{10}$'),]],
        dob: ['', []],
        bloogGroup: ['', []],
        gender : ['', []],
        address: ['', []],
        stateId: ['', []],
        countryId: ['', []],
        city : ['', []],
        pinCode: ['', [Validators.pattern('^[0-9]{6}$')]],
        imageFile: [null, [  ]]
      });
    }
  
  
    // date picker future date 
    onSelectedChange(event: any) {
      this.selectedDate = event;
    }
  
  
    // number validation
    numberOnly(event: { which: any; keyCode: any; }): boolean {
      const charCode = (event.which) ? event.which : event.keyCode;
      if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
      }
      return true;
    }
  
    onKeyPress(event: KeyboardEvent) {
      const charCode = event.which ? event.which : event.keyCode;
      // Allow only numeric characters (keycodes for 0-9)
      if (charCode < 48 || charCode > 57) {
        event.preventDefault(); // Block non-numeric characters
      }
    }
  
    ngOnInit(): void {
      this.loadUserData()
      this.setform();
      this.loadCountries();
      this.LoadUserType();
      this.loadStates();
      this.sanitizeField('firstName');
      this.sanitizeField('lastName');
      this.sanitizeField('gender');
      // this.sanitizeField('bloogGroup');
      this.sanitizeField('address');
      this.sanitizeField('city');
     
    }

    openDialog() {
      const dialogRef = this.dialog.open(PatientDialogComponent, {
        width: '600px',
        data: this.userObj
      });
  
      dialogRef.afterClosed().subscribe(result => {
        if (result) {
          this.onSubmit();
        }
      });
    }
    
  
    // patient form 
    loadUserData() {

      var userId = this.patientdata.userId;
      this.service.getbyid(userId).subscribe({
        next: (res :any) => {
          this.userObj = res;
          console.log(this.userObj);
          console.log(res)
        },
        error: (err :any) => {
          console.log(err);
        }
      })
    }
  
    onEdit(data:any)
    {

      


    }

  
  
    onSubmit() : void{
      if(this.patientform.invalid)
      {
        this.patientform.markAllAsTouched();
          
        this.toastr.error('Please enter valid data', 'error',
          {
            timeOut: 3000,
            progressBar: true,
            progressAnimation: 'increasing',
            positionClass: 'toast-top-right'
  
          }
        );
        return;
      }
      const formData = new FormData();
      const formValues = this.patientform.getRawValue();
  
      if (formValues.dob) {
        const dob = new Date(formValues.dob);
        formValues.dob = dob.toISOString(); // Convert to ISO string which includes time
      }
      // Append all fields to FormData except imageFile if it's not selected
      Object.keys(formValues).forEach(key => {
        const value = formValues[key];
        
        if (key === 'imageFile' && this.imageFile) {
          formData.append(key, this.imageFile);  // Append image only if selected
        } else if (value !== null) {
          formData.append(key, value);
        }
      });
  
      this.service.updatepatient(formData).subscribe({
  
        next: (res :any) => {
          console.log(res);
          if(res.statusCode === 200)
          {
            this.toastr.success(res.message, 'success',
            {
              timeOut: 3000,
              progressBar: true,
              progressAnimation: 'increasing',
              positionClass: 'toast-top-right'
  
            }
          );
          this.loadUserData();
  
          }
          else{
            this.toastr.error(res.message, 'error',
            {
              timeOut: 3000,
              progressBar: true,
              progressAnimation: 'increasing',
              positionClass: 'toast-top-right'
          }
        );
        } 
      },
        error: (err :any) => {
          console.log(err);
        }
    })
   
    }



  
  
  
    onFileChange(event: any): void {
      if (event.target.files.length > 0) {
        this.imageFile = event.target.files[0];
        this.patientform  .patchValue({
          imageFile: this.imageFile
        });
      }
    }
  
    sanitizeField(fieldName: string): void {
      this.patientform  .get(fieldName)?.valueChanges.subscribe((value) => {
        if (value) {
          // Allow trailing spaces, but clean invalid characters and reduce multiple spaces
          const sanitizedValue = value
            .replace(/[^A-Za-z\s]/g, '') // Remove non-letters and non-spaces
            .replace(/\s{2,}/g, ' '); 
            
            // Replace multiple spaces with a single space (excluding trailing)
          if (value !== sanitizedValue) {
            this.patientform  .get(fieldName)?.setValue(sanitizedValue, {
              emitEvent: false, // Avoid recursive calls
            });
          }
        }
      });
    }
  
  
    private loadCountries(): void {
      this.service.getallcountry().subscribe({
        next: (res) => {
          console.log('Countries loaded:', res);
          this.countries = res;
        },
        error: (error) => {
          console.error('Error loading countries', error);
        }
      });
    }
  
    private LoadUserType(): void {
      this.service.getallusertype().subscribe({
        next: (res) => {
          console.log('Roles loaded:', res);
          this.roles = res;
        },
        error: (error) => {
          console.error('Error loading roles', error);
        }
      });
    }
  
    private loadStates(): void {
      this.service.getallstate().subscribe({
        next: (res) => {
          console.log('States loaded:', res);
          this.states = res;
        },
        error: (error) => {
          console.error('Error loading states', error);
        }
      });
    }
  


}
