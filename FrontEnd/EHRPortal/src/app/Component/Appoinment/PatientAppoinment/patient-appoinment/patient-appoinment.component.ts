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
<<<<<<< HEAD
=======
import { PaymentDialogComponent } from '../payment-dialog/payment-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { RazorPayService } from '../../../../Service/razor-pay.service';
import Razorpay from 'razorpay';
>>>>>>> a552e86ed2b20a2976205b01f4fb775cbec60056


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
<<<<<<< HEAD
=======
   paymentService= inject(RazorPayService)
>>>>>>> a552e86ed2b20a2976205b01f4fb775cbec60056

   specialisationdata : any
   providerbyspecialisationdata : any ;
   patientdata : any = {}
   userdata = JSON.parse(sessionStorage.getItem('logindata') || '{}');
   minDate = new Date()
   minTime!:string 
<<<<<<< HEAD

=======
   appointmentTime: Date;
  Fees! : any
   readonly dialog = inject(MatDialog);
>>>>>>> a552e86ed2b20a2976205b01f4fb775cbec60056

  constructor(private fb : FormBuilder) 
  {

<<<<<<< HEAD
=======
    this.minTime = this.calculateMinTime();
>>>>>>> a552e86ed2b20a2976205b01f4fb775cbec60056

    this.patientappoinmentform = this.fb.group({
      appointmentDate: ['',[Validators.required]],
      providerId: ['',Validators.required],
      patientId: [this.userdata.userId,Validators.required],
      specialisationId: [0,[Validators.required]] ,
      appinementTime: ['',[Validators.required]] ,
      chiefcomplaint: ['',[Validators.required]],

    })

<<<<<<< HEAD
=======
    this.appointmentTime = new Date();
    this.appointmentTime.setHours(this.appointmentTime.getHours() + 1);
    this.appointmentTime.setMinutes(0);
    this.appointmentTime.setSeconds(0);


>>>>>>> a552e86ed2b20a2976205b01f4fb775cbec60056
   }


   ngOnInit(): void {
<<<<<<< HEAD
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



=======

    this.getallprovider();
    this.getallspecialisation();
    // this.getallprovider();
  
   

   }

   calculateMinTime(): string {
    const now = new Date();
    const hour = now.getHours() + 1;
    const minute = now.getMinutes();
    return `${hour.toString().padStart(2, '0')}:${minute.toString().padStart(2, '0')}`;
}



getallprovider()
{
  this.service.getallProvider().subscribe((res: any) => {
    this.providerbyspecialisationdata = res.data;
  });
}

>>>>>>> a552e86ed2b20a2976205b01f4fb775cbec60056
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
<<<<<<< HEAD
=======
        console.log(this.providerbyspecialisationdata);

>>>>>>> a552e86ed2b20a2976205b01f4fb775cbec60056
      });
    }
  }

<<<<<<< HEAD
=======
  getSelectedProviderVisitingCharge(): any {
   this.Fees= this.providerbyspecialisationdata.find((provider: any) => provider.userId === this.patientappoinmentform.get('providerId')?.value);
    return this.Fees ? this.Fees.visitingCharge : '';
  }

>>>>>>> a552e86ed2b20a2976205b01f4fb775cbec60056
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

<<<<<<< HEAD
=======
  //  openDialog() {
  //   const dialogRef = this.dialog.open(PaymentDialogComponent);

  //   dialogRef.afterClosed().subscribe(result => {
  //     console.log(`Dialog result: ${result}`);
  //   });
  // }


  openModal(){
    const modal = document.getElementById("myModal");
    if (modal) {
      modal.style.display = "block";
    }
  }

  CloseModal(){
    const modal = document.getElementById("myModal");
    if (modal) {
      modal.style.display = "none";
    }
  }


  Pay()
  {
    const amount: number = (this.Fees);
    this.onPayNow(Math.floor(amount));
  }

  onPayNow(amount: number) {
    debugger;
    
    this.paymentService.createOrder(amount).subscribe((order:any) => {
      console.log('API request sent' , amount);
      console.log(order)
      const options: any = {
        key:'rzp_test_KMY5pBLSVhJH3u', // Replace with your Razorpay Key ID
        amount: amount * 100, // Amount in paise
        currency: 'INR',
        name: 'SDN Company',
        description: 'Payment for Order',
        order_id: order.orderId,
        handler: (response: any) => {
          // this.verifyPayment(response);
        },
        prefill: {
          name: 'Customer Name',
          email: 'customer@example.com',
        },
        theme: {
          color: '#F37254'
        }
      };
      
      const rzp1 :any = new Razorpay(options);
      rzp1.open();
    });
    this.onSubmit();
  }

  

>>>>>>> a552e86ed2b20a2976205b01f4fb775cbec60056
   onSubmit()
   {
     if(this.patientappoinmentform.invalid)
     {
      return;
     }

<<<<<<< HEAD
     this.service.patientappoinment(this.patientappoinmentform.value).subscribe({
=======
     const formValues = this.patientappoinmentform.value;
     formValues.appinementTime = this.appointmentTime.toISOString();
     this.service.patientappoinment(formValues).subscribe({
>>>>>>> a552e86ed2b20a2976205b01f4fb775cbec60056

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