import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LoginService } from '../../Service/login.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-provider-profile',
  standalone: true,
  imports: [],
  templateUrl: './provider-profile.component.html',
  styleUrl: './provider-profile.component.css'
})
export class ProviderProfileComponent {

   providerform : FormGroup = new FormGroup({});
    imageFile: File | null = null;
    countries: any;
    states: any;
    roles: any;
    specialisationlist : any
    userObj: any;
    private router = inject(Router);
    private service = inject(LoginService);
    private toastr = inject(ToastrService);
    mode!: string;
    providerdata = JSON.parse(sessionStorage.getItem('logindata') || '{}');
    userId!: number;
    todayDate=new Date().toISOString().split('T')[0];
    selectedDate = new Date();
  
  
    constructor(private fb: FormBuilder) {
    }

    setform(){
      this.providerform = this.fb.group({
        firstName: ['', [Validators.minLength(2), 
          Validators.maxLength(20),Validators.pattern(/^[A-Za-z]+(?: [A-Za-z]+)*\s*$/)]],
        lastName: ['', []],
        email: ['', [, Validators.email]],
        mobile: ['', [, Validators.pattern('^[0-9]{10}$'),]],
        dob: [Date, ],
        bloogGroup: ['', []],
        gender : ['', []],
        address: ['', []],
        stateId: ['', []],
        countryId: ['', []],
        city : ['', []],
        pinCode: ['', [,Validators.pattern('^[0-9]{6}$')]],
        imageFile: [null, []],
        qualification: ['', []],
        specialisation : ['', []],
        registrationNumber :['', []],
        visitingCharge : ['', []]
      });
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
      this.loadUserData();
      this.setform();
      this.getallSpecoalisation();
      this.loadCountries();
      this.LoadUserType();
      this.loadStates();
      this.sanitizeField('firstName');
      this.sanitizeField('lastName');
      this.sanitizeField('gender');
      this.sanitizeField('address');
      this.sanitizeField('city');
    }
    
    loadUserData() {
      var userId = this.providerdata.userId;
      this.service.getbyid(userId).subscribe({
        next: (res :any) => {
          this.userObj = res.data;
          console.log(this.userObj);
          console.log(res)
        },
        error: (err :any) => {
          console.log(err);
        }
      })
    }
  

  
  
    onSubmit() : void{
      if(this.providerform.invalid)
      {
        this.providerform.markAllAsTouched();
          
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
      const formValues = this.providerform.getRawValue();
      if (formValues.dob) {
        const dob = new Date(formValues.dob);
        formValues.dob = dob.toISOString(); // Convert to ISO string which includes time
      }
      Object.keys(formValues).forEach(key => {
        const value = formValues[key];;
        if (key === 'imageFile' && this.imageFile) {
          formData.append(key, this.imageFile);
        } else if (value) {
          formData.append(key, value);
        }
      });
  
      this.service.registerprovider(formData).subscribe({
  
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
        this.providerform  .patchValue({
          imageFile: this.imageFile
        });
      }
    }
  
    sanitizeField(fieldName: string): void {
      this.providerform  .get(fieldName)?.valueChanges.subscribe((value) => {
        if (value) {
          // Allow trailing spaces, but clean invalid characters and reduce multiple spaces
          const sanitizedValue = value
            .replace(/[^A-Za-z\s]/g, '') // Remove non-letters and non-spaces
            .replace(/\s{2,}/g, ' '); 
            
            // Replace multiple spaces with a single space (excluding trailing)
          if (value !== sanitizedValue) {
            this.providerform  .get(fieldName)?.setValue(sanitizedValue, {
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
  
  
    getallSpecoalisation()
    {
      this.service.getallspecialisation().subscribe({
  
        next: (res :any)=>{
          console.log(res);
          this.specialisationlist = res;
        },
        error:(err)=>{
          console.log(err);
          
        }
        
      })
    }
  
  

}
