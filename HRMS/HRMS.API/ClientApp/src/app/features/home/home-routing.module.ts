import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { EmployeeCreateComponent } from '../employee/components/employee-create/employee-create.component';

const routes: Routes = [
  {
    path:'app-dashboard',component:DashboardComponent
  },

  {

    path:'app-employee-create',component:EmployeeCreateComponent
  }
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeRoutingModule { }
