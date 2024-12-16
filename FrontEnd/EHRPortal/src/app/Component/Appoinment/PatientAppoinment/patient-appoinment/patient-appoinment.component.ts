import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AppoinmentService } from '../../../../Service/appoinment.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatNativeDateModule } from '@angular/material/core';
import { NgxMaterialTimepickerModule } from 'ngx-material-timepicker';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-patient-appoinment',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatCardModule,
    MatDatepickerModule,
    MatNativeDateModule,
    NgxMaterialTimepickerModule,
    CommonModule
  ],
  templateUrl: './patient-appoinment.component.html',
  styleUrl: './patient-appoinment.component.css'
})
export class PatientAppoinmentComponent {


  patientappoinmentform : FormGroup = new FormGroup({});

   router = inject(Router);
   toastr = inject(ToastrService); 
   service = inject(AppoinmentService)

   specialisationdata : any
   providerbyspecialisationdata : any ;
   patientdata : any = {}
   userdata = JSON.parse(sessionStorage.getItem('logindata') || '{}');
   minDate = new Date()
   minTime!:string 


  constructor(private fb : FormBuilder) 
  {


    this.patientappoinmentform = this.fb.group({
      appointmentDate: ['',[Validators.required]],
      providerId: ['',Validators.required],
      patientId: [this.userdata.userId,Validators.required],
      specialisationId: [0,[Validators.required]] ,
      appinementTime: ['',[Validators.required]] ,
      chiefcomplaint: ['',[Validators.required]],

    })

   }


   ngOnInit(): void {
    this.getallspecialisation();
    // this.getallprovider();
    this.patientappoinmentform.get('specialisationId')?.valueChanges.subscribe(value => {
      this.getproviderbyspecialisation(value);
    });

    this.patientappoinmentform.get('appointmentDate')?.valueChanges.subscribe((value) => {
      this.minTime = this.calculateMinTime(value);
    })

   }

   calculateMinTime(date :Date): string {
    const now = date ? new Date(date) : new Date();
    now.setHours(now.getHours() + 1);
    // return now.toTimeString().split(' ')[0].substring(0, 5);
    return now.toISOString().split('T')[1].split(':').slice(0, 2).join(':');

  }



  // getallprovider()
  // {
  //   this.service.getallProvider().subscribe((res : any) => {
  //     console.log(res);
  //     if(res.statusCode === 200)
  //     {
  //       this.providerbyspecialisationdata = res.data;
  //     console.log(this.providerbyspecialisationdata);

  //     }
  //     else{
  //       console.log("I am getallprovider error");
        
  //     }
      
  //   })
  // }
  getproviderbyspecialisation(specialisationId: any): void {
    if (specialisationId === null || specialisationId === 0) {
      this.service.getallProvider().subscribe((res: any) => {
        this.providerbyspecialisationdata = res.data;
      });
    } else {
      this.service.getproviderbyid(specialisationId).subscribe((res: any) => {
        this.providerbyspecialisationdata = res.data;
      });
    }
  }

  //  getproviderbyspecialisation()
  //  {
  //   this.service.getproviderbyid(this.patientappoinmentform.value.specialisationId).subscribe((res : any) => {
  //     console.log(res)
  //     this.providerbyspecialisationdata = res;
  //     console.log(this.providerbyspecialisationdata);
  //   })
  //  }

   getallspecialisation()
   {
     this.service.getallspecialisation().subscribe((res : any) => {
      console.log(res);
      this.specialisationdata = res;
      console.log(this.specialisationdata);
     })
   }

   onSubmit()
   {
     if(this.patientappoinmentform.invalid)
     {
      return;
     }

     this.service.patientappoinment(this.patientappoinmentform.value).subscribe({

      next: (res : any) => {
        if(res.statusCode === 200)
          {
            console.log(res);
            this.patientdata = res.data;
            this.toastr.success('Appoinment Booked Successfully');
            // this.router.navigate(['/patient']);
          }
          else {
            this.toastr.error(res.message);
          } 
      },
      error: (error) => {
        console.error(error);
      }
       
    
     })

   }
   

}
