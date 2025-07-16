import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AssignstockRoutingModule } from './assignstock-routing.module';
import { AssignStockListComponent } from './components/assign-stock-list/assign-stock-list.component';
import { AssignStockEditComponent } from './components/assign-stock-edit/assign-stock-edit.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'src/app/shared/shared.module';
import { AssignStockCreateComponent } from './components/assign-stock-create/assign-stock-create.component';


@NgModule({
  declarations: [
    AssignStockCreateComponent,
    AssignStockListComponent,
    AssignStockEditComponent

  ],
  imports: [
    CommonModule,
    AssignstockRoutingModule,
    ReactiveFormsModule,
    SharedModule

  ]
})
export class AssignstockModule { }
