import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AssignStockGetUseridRoutingModule } from './assign-stock-get-userid-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'src/app/shared/shared.module';
import { AssignstockgetbyuseridlistComponent } from './components/assignstockgetbyuseridlist/assignstockgetbyuseridlist.component';


@NgModule({
  declarations: [
    AssignstockgetbyuseridlistComponent
  ],
  imports: [
    CommonModule,
    AssignStockGetUseridRoutingModule,
    ReactiveFormsModule,
    SharedModule
  ]
})
export class AssignStockGetUseridModule { }
