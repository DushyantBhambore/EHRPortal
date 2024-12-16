import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogActions, MatDialogClose, MatDialogContent } from '@angular/material/dialog';

@Component({
  selector: 'app-payment-dialog',
  standalone: true,
  imports: [MatDialogContent
    ,MatDialogActions,
    MatDialogClose,
    MatButtonModule
  
  ],
  templateUrl: './payment-dialog.component.html',
  styleUrl: './payment-dialog.component.css'
})
export class PaymentDialogComponent {
  

}
