import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LoginService {



  registerpatienturl ='https://localhost:7161/api/User/PatientRegister'
  registerproviderurl ='https://localhost:7161/api/User/PractitionerRegister'

  updatepatienturl ='https://localhost:7161/api/User/UpdatedPatient'

  updateproviderurl ='https://localhost:7161/api/User/UpdatedPractitioner'

  verifyotpurl ='https://localhost:7161/api/User/VerifyOTP'

  forgotpasswordurl ='https://localhost:7161/api/User/ForgotPassword'

  changepasswordurl ='https://localhost:7161/api/User/ChangePassword'

  loginurl = 'https://localhost:7161/api/User/LoginUser'

  getbyidurl ='https://localhost:7161/api/User/GetById'

  getallcountryurl ='https://localhost:7161/api/Country/GetAllCountry'

  getallstateurl ='https://localhost:7161/api/State/GetAllState'

  getstatebyidurl ='https://localhost:7161/api/State/GetStateById'

  getallspecialisationurl ='https://localhost:7161/api/Specialisation/GetAllSpecialisation'


  getallusertypeurl ='https://localhost:7161/api/UserType/GetAllUserType'
  


  http = inject(HttpClient);
  constructor() { }


  registerpatient(data :any){
    return this.http.post(this.registerpatienturl,data)
  }

  registerprovider(data :any){
    return this.http.post(this.registerproviderurl,data)
  }

  updatepatient(data :any){
    return this.http.post(this.updatepatienturl,data)
  }

  updateprovider(data :any){
    return this.http.post(this.updateproviderurl,data)
  }

  verifyotp(data :any){
    return this.http.post(this.verifyotpurl,data)
  }

  forgotpassword(data :any){
    return this.http.post(this.forgotpasswordurl,data)
  }

  changepassword(data :any){
    return this.http.post(this.changepasswordurl,data)
  }

  login(data :any){
    return this.http.post(this.loginurl,data)
  }

  getbyid(data :any){
    return this.http.post(this.getbyidurl,data)
  }

  getallcountry(){
    return this.http.get(this.getallcountryurl)
  }


  getallstate(){
    return this.http.get(this.getallstateurl)
  }

  getstatebyid(data :any){
    return this.http.post(this.getstatebyidurl,data)
  }

  getallspecialisation(){
    return this.http.get(this.getallspecialisationurl)
  }


  getallusertype(){
    return this.http.get(this.getallusertypeurl)
  }

}
