import { Routes } from '@angular/router';
import { LoginComponent } from './Component/login/login.component';
import { ForgotpasswordComponent } from './Component/forgotpassword/forgotpassword.component';
import { HomeComponent } from './Component/home/home.component';
import { ProfileComponent } from './Component/profile/profile.component';
import { PatientComponent } from './Component/patient/patient.component';
import { ProviderComponent } from './Component/provider/provider.component';

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
                path:'profile',
                component:ProfileComponent
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
