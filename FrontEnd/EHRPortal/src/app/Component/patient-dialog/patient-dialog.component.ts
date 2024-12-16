import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogActions, MatDialogContent, MatDialogRef } from '@angular/material/dialog';
import { LoginService } from '../../Service/login.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-patient-dialog',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatDialogContent,MatDialogActions
  ],
  templateUrl: './patient-dialog.component.html',
  styleUrl: './patient-dialog.component.css'
})
export class PatientDialogComponent {

  patientform: FormGroup;
  imageFile: File | null = null;
  countries: any;
  states: any;
  roles: any;
  todayDate = new Date().toISOString().split('T')[0];

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<PatientDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private service: LoginService,
    private toastr: ToastrService
  ) {
    this.patientform = this.fb.group({
      firstName: [data.firstName, [Validators.minLength(2), Validators.maxLength(20), Validators.pattern(/^[A-Za-z]+(?: [A-Za-z]+)*\s*$/)]],
      lastName: [data.lastName, []],
      email: [data.email, [Validators.email]],
      mobile: [data.mobile, [Validators.pattern('^[0-9]{10}$')]],
      dob: [data.dob, []],
      bloogGroup: [data.bloogGroup, []],
      gender: [data.gender, []],
      address: [data.address, []],
      stateId: [data.stateId, []],
      countryId: [data.countryId, []],
      city: [data.city, []],
      pinCode: [data.pinCode, [Validators.pattern('^[0-9]{6}$')]],
      imageFile: [null, []]
    });

    this.loadCountries();
    this.loadStates();
    this.loadRoles();
  }

  loadCountries(): void {
    this.service.getallcountry().subscribe({
      next: (res) => {
        this.countries = res;
      },
      error: (error) => {
        console.error('Error loading countries', error);
      }
    });
  }

  loadStates(): void {
    this.service.getallstate().subscribe({
      next: (res) => {
        this.states = res;
      },
      error: (error) => {
        console.error('Error loading states', error);
      }
    });
  }

  loadRoles(): void {
    this.service.getallusertype().subscribe({
      next: (res) => {
        this.roles = res;
      },
      error: (error) => {
        console.error('Error loading roles', error);
      }
    });
  }

  onFileChange(event: any): void {
    if (event.target.files.length > 0) {
      this.imageFile = event.target.files[0];
      this.patientform.patchValue({
        imageFile: this.imageFile
      });
    }
  }

  onSubmit(): void {
    if (this.patientform.invalid) {
      this.patientform.markAllAsTouched();
      this.toastr.error('Please enter valid data', 'error', {
        timeOut: 3000,
        progressBar: true,
        progressAnimation: 'increasing',
        positionClass: 'toast-top-right'
      });
      return;
    }

    const formData = new FormData();
    const formValues = this.patientform.getRawValue();

    if (formValues.dob) {
      const dob = new Date(formValues.dob);
      formValues.dob = dob.toISOString();
    }

    Object.keys(formValues).forEach(key => {
      const value = formValues[key];
      if (key === 'imageFile' && this.imageFile) {
        formData.append(key, this.imageFile);
      } else if (value !== null) {
        formData.append(key, value);
      }
    });

    this.service.updatepatient(formData).subscribe({
      next: (res: any) => {
        if (res.statusCode === 200) {
          this.toastr.success(res.message, 'success', {
            timeOut: 3000,
            progressBar: true,
            progressAnimation: 'increasing',
            positionClass: 'toast-top-right'
          });
          this.dialogRef.close(true);
        } else {
          this.toastr.error(res.message, 'error', {
            timeOut: 3000,
            progressBar: true,
            progressAnimation: 'increasing',
            positionClass: 'toast-top-right'
          });
        }
      },
      error: (err: any) => {
        console.error(err);
      }
    });
  }
}
