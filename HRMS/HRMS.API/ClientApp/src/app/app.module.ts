import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { SharedModule } from './shared/shared.module';
import { AccessDeniedComponent } from './shared/components/access-denied/access-denied.component';
import { CoreModule } from './core/core.module';

@NgModule({
  declarations: [
  AppComponent,                       //To  route this  access-denied  components form  app-routing module(root) we have to  register its components 
  AccessDeniedComponent              //// beacuse this  component is directly call in in app-routing app so we declare in app.module
  ],                                // we can do it by declaration in its own  sharemodule and export it and call it , means import in  app module by calling shared module 
                                    // thne there beacome so many dependency .. so we directly declare here to used direct call in app-routing
  imports: [
    BrowserModule,
    AppRoutingModule,
    SharedModule,
    CoreModule
    
  ],

  
  bootstrap: [AppComponent]
})
export class AppModule { }
