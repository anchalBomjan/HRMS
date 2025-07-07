import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AssignstockRoutingModule } from './assignstock-routing.module';
import { AssignStockEditComponent } from './components/assign-stock-edit/assign-stock-edit.component';
import { AssignStockCreateComponent } from './components/assign-stock-create/assign-stock-create.component';
import { AssignStockListComponent } from './components/assign-stock-list/assign-stock-list.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'src/app/shared/shared.module';


@NgModule({
  declarations: [
    AssignStockEditComponent,
    AssignStockCreateComponent,
    AssignStockListComponent
  ],
  imports: [
    CommonModule,
    AssignstockRoutingModule,
    ReactiveFormsModule,
    SharedModule
  ]
})
export class AssignstockModule { }
