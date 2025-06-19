import { NgModule, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { AuthService } from './services/auth.service';
import { LoggerService } from './services/logger.service';
import { authGuard } from './guards/auth.guard';
import { roleGuard } from './guards/role.guard';
import { AuthInterceptor } from './interceptors/auth.interceptor';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,HttpClientModule
  ],

  providers:[
    AuthService,
    LoggerService,
    {provide:'authGuard',useValue:authGuard},
    {provide:'roleGauard',useValue:roleGuard},
    {provide:'HTTP_INTERCEPTORS', useClass:AuthInterceptor,multi:true}

    
  
  ]
})
export class CoreModule { 
  constructor(@Optional() @SkipSelf()parentModule:CoreModule) {
    if(parentModule){
      throw new Error(
 
        'CoreModule is already loaded. Impoort it in the AppModule only'
      );

    }
   
    
  }
}
