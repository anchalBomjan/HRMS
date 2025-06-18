import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { LoginComponent } from './features/auth/login/login.component';
import { RegistrationComponent } from './features/auth/registration/registration.component';
import { PrimengModule } from './primeng/primeng.module';
import { MessageService, SharedModule } from 'primeng/api';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegistrationComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,SharedModule
  ],
  providers: [],
  bootstrap: [AppComponent,  MessageService]
})
export class AppModule { }
