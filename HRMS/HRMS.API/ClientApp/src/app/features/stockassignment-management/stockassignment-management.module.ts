import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { StockassignmentManagementRoutingModule } from './stockassignment-management-routing.module';
import { StockassignmentListComponent } from './pages/stockassignment-list/stockassignment-list.component';
import { StockassignmentCreateComponent } from './pages/stockassignment-create/stockassignment-create.component';
import { StockassignmentEditComponent } from './pages/stockassignment-edit/stockassignment-edit.component';
import { StockassignmentDetailsComponent } from './pages/stockassignment-details/stockassignment-details.component';


@NgModule({
  declarations: [
    StockassignmentListComponent,
    StockassignmentCreateComponent,
    StockassignmentEditComponent,
    StockassignmentDetailsComponent
  ],
  imports: [
    CommonModule,
    StockassignmentManagementRoutingModule
  ]
})
export class StockassignmentManagementModule { }
