
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ForbiddenComponent } from './shared/components/forbidden/forbidden.component';
import { Routes } from '@angular/router';

const routes:Routes=[
  {path: 'forbidden',component:ForbiddenComponent}
]


@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ]
})
export class AppRoutingModule { }
