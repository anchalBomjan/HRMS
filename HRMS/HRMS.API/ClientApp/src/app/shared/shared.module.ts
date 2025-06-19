
import { CommonModule } from '@angular/common';

import { HeaderComponent } from './components/layout/header/header.component';
import { SidebarComponent } from './components/layout/sidebar/sidebar.component';
import { FooterComponent } from './components/layout/footer/footer.component';

import { PrimengModule } from '../primeng/primeng.module';
import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { ForbiddenComponent } from './components/forbidden/forbidden.component';

@NgModule({
  declarations: [
    HeaderComponent,
    SidebarComponent,
    FooterComponent,
    ForbiddenComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    PrimengModule
  ],
  exports: [
    HeaderComponent,
    SidebarComponent,
    FooterComponent,
    PrimengModule,RouterModule
  ]
})
export class SharedModule { }
