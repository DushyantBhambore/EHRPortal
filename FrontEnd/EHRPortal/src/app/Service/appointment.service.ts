import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AppointmentService {


  patientappoinmenturl ='https://localhost:7161/api/Appoinment/PatientAppoinment'


  http = inject(HttpClient);
  constructor() { }

}
