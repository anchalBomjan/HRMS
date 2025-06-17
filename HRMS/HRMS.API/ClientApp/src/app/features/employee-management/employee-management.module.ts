import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeeListComponent } from './components/employee-list/employee-list.component';
import { EmployeeDetailComponent } from './components/employee-detail/employee-detail.component';



@NgModule({
  declarations: [
    EmployeeListComponent,
    EmployeeDetailComponent
  ],
  imports: [
    CommonModule
  ]
})
export class EmployeeManagementModule { }
