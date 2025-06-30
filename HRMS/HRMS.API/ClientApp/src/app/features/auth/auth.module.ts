import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthRoutingModule } from './auth-routing.module';
import { LoginComponent } from './components/login/login.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { ResetComponent } from './components/reset/reset.component';
import { ForgetComponent } from './components/forget/forget.component';
import { ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [

    LoginComponent,
    RegistrationComponent,
    ResetComponent,
    ForgetComponent,
    
  ],
  imports: [
    CommonModule,
    AuthRoutingModule,
    ReactiveFormsModule
  ]
})
export class AuthModule { }
