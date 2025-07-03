import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { LoadingSpinnerComponent } from './components/loading-spinner/loading-spinner.component';
import { ErrorDisplayComponent } from './components/error-display/error-display.component';
import { SafeHtmlPipe } from './pipes/safe-html.pipe';
import { RoleNamePipe } from './pipes/role-name.pipe';
import { DateFormatPipe } from './pipes/date-format.pipe';
import { HasRoleDirective } from './directives/has-role.directive';
import { InputMaskDirective } from './directives/input-mask.directive';
import { RouterModule } from '@angular/router';
import { PrimengModule } from './primeng/primeng.module';
import { EmployeeListComponent } from '../features/employee/components/employee-list/employee-list.component';
import { TableModule } from 'primeng/table';



@NgModule({
  declarations: [
    HeaderComponent,
    FooterComponent,
    SidebarComponent,
    LoadingSpinnerComponent,
    ErrorDisplayComponent,
    SafeHtmlPipe,
    RoleNamePipe,
    DateFormatPipe,
    HasRoleDirective,
    InputMaskDirective
  ],
  imports: [
    CommonModule,RouterModule,PrimengModule,
  
  ],

  exports:[    // Reusable Components
  HeaderComponent,
  FooterComponent,
  SidebarComponent,
  LoadingSpinnerComponent,
  ErrorDisplayComponent,

  // Directives
  HasRoleDirective,
  InputMaskDirective,

  // Pipes
  SafeHtmlPipe,
  RoleNamePipe,
  DateFormatPipe,

  // Modules
  CommonModule,
  RouterModule,
  PrimengModule]
})
export class SharedModule { }
