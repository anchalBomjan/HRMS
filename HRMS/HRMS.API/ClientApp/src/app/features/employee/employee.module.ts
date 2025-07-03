import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeeListComponent } from './components/employee-list/employee-list.component';
import { EmployeeCreateComponent } from './components/components/employee-create/employee-create.component';
import { EmployeeEditComponent } from './components/employee-edit/employee-edit.component';



@NgModule({
  declarations: [
    EmployeeListComponent,
    EmployeeCreateComponent,
    EmployeeEditComponent
  ],
  imports: [
    CommonModule
  ]
})
export class EmployeeModule { }
