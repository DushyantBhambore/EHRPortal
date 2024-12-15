import { Routes } from '@angular/router';
import { LoginComponent } from './Component/login/login.component';
import { ForgotpasswordComponent } from './Component/forgotpassword/forgotpassword.component';
import { HomeComponent } from './Component/home/home.component';
import { PatientComponent } from './Component/patient/patient.component';
import { ProviderComponent } from './Component/provider/provider.component';
import { PatientDashboardComponent } from './Component/patient-dashboard/patient-dashboard.component';
import { ProviderDashboardComponent } from './Component/provider-dashboard/provider-dashboard.component';
import { ChangepasswordComponent } from './Component/changepassword/changepassword.component';
import { PatientProfileComponent } from './Component/patient-profile/patient-profile.component';
import { ProviderProfileComponent } from './Component/provider-profile/provider-profile.component';
import { PatientAppoinmentComponent } from './Component/Appoinment/PatientAppoinment/patient-appoinment/patient-appoinment.component';

export const routes: Routes = [
    {
        path:'',
        redirectTo: 'login',
        pathMatch: 'full'
    },
    {
        path: 'login',
        component:LoginComponent
    },
    {
        path:'forgot-password',
        component:ForgotpasswordComponent
    },
    {
        path:'',
        component:HomeComponent,
        children:[
            {
                path:'patient-dashboard',
                component:PatientDashboardComponent
                ,children:[
                    {
                        path:'profile',
                        component:PatientProfileComponent
                    },
                    {
                       path:'change-password',
                          component:ChangepasswordComponent 
                    },
                    {
                        path:'patient-appoinment',
                        component:PatientAppoinmentComponent
                    }
                ]
            },
            {
                path:'provider-dashboard',
                component:ProviderDashboardComponent,
                children:[
                    {
                        path:'profile',
                        component:ProviderProfileComponent
                    },
                    {
                        path:'change-password',
                        component:ChangepasswordComponent
                    }
                ]
            }
            
        ]
    },
    {
        path:'patient',
        component:PatientComponent
    },
    {
        path:'provider',
        component:ProviderComponent
    }
];
