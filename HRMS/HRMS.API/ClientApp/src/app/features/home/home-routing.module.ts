import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { EmployeeCreateComponent } from '../employee/components/employee-create/employee-create.component';
import { EmployeeListComponent } from '../employee/components/employee-list/employee-list.component';
import { EmployeeEditComponent } from '../employee/components/employee-edit/employee-edit.component';

const routes: Routes = [
  {
    path:'app-dashboard',component:DashboardComponent,
    children:[
    
        { 
        
         path:'employees',
          component: EmployeeListComponent
         },
         { path: 'employees/create', component: EmployeeCreateComponent },
         { path: 'employees/edit/:id', component: EmployeeEditComponent },

      
    
      
    
    ]
  },

 
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeRoutingModule { }
