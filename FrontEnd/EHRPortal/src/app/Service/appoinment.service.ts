import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AppoinmentService {

  patientappoinmenturl ='https://localhost:7161/api/Appoinment/PatientAppoinment'

  getallspecialisationurl ='https://localhost:7161/api/Specialisation/GetAllSpecialisation'

  getproviderbyidurl = 'https://localhost:7161/api/Appoinment/GetAllProviderBySpecialisation'

  getallproviderurl ='https://localhost:7161/api/Appoinment/GetAllProvider'
  http = inject(HttpClient)
  constructor() { }




  patientappoinment(data :any){
    return this.http.post(this.patientappoinmenturl,data)
  }

  getallspecialisation(){
    return this.http.get(this.getallspecialisationurl)
  }

  getproviderbyid(id :number){
    return this.http.get(this.getproviderbyidurl+'/'+id)
  }

  getallProvider()
  {
    return this.http.get(this.getallproviderurl)
  }

}
