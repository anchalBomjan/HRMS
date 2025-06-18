import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HeaderComponent } from './components/layout/header/header.component';
import { SidebarComponent } from './components/layout/sidebar/sidebar.component';
import { FooterComponent } from './components/layout/footer/footer.component';

import { RouterModule } from '@angular/router';
import { PrimengModule } from '../primeng/primeng.module';

@NgModule({
  declarations: [
    HeaderComponent,
    SidebarComponent,
    FooterComponent
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
    PrimengModule
  ]
})
export class SharedModule { }
