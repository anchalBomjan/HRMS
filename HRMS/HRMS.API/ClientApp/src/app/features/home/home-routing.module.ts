import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { EmployeeCreateComponent } from '../employee/components/employee-create/employee-create.component';
import { EmployeeListComponent } from '../employee/components/employee-list/employee-list.component';
import { EmployeeEditComponent } from '../employee/components/employee-edit/employee-edit.component';
import { StockListComponent } from '../stock/components/stock-list/stock-list.component';
import { AssignStockListComponent } from '../assignstock/components/assign-stock-list/assign-stock-list.component';
import { AssignStockCreateComponent } from '../assignstock/components/assign-stock-create/assign-stock-create.component';
import { RoleListComponent } from '../role/role-list/role-list.component';
import { UserListComponent } from '../user/components/user-list/user-list.component';
import { AssignstockgetbyuseridlistComponent } from '../assign-stock-get-userid/components/assignstockgetbyuseridlist/assignstockgetbyuseridlist.component';
import { Dashboard2Component } from '../dashboard2/dashboard2.component';

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

           // Stock routes added here
      {
        path: 'stocks',
        component: StockListComponent
      },

      { path: 'assignments', component:AssignStockListComponent },
      {
        path:'assign-stock-create',component:AssignStockCreateComponent
      },

      {path:'app-role-list',component:RoleListComponent

      },
      {
        path:'app-user-list',component:UserListComponent
      },

      {
        path:'app-assignstockgetbyuseridlist',component:AssignstockgetbyuseridlistComponent
      },
      {
        path:'',component:Dashboard2Component
      }
    
      
    
    ]
  },

 
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeRoutingModule { }
