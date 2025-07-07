import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { StockRoutingModule } from './stock-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { ReactiveFormsModule } from '@angular/forms';
import { StockCreateComponent } from './components/stock-create/stock-create.component';
import { StockListComponent } from './components/stock-list/stock-list.component';


@NgModule({
  declarations: [
  StockListComponent,
  StockCreateComponent
  ],
  imports: [
    CommonModule,
    StockRoutingModule,
    SharedModule,
    ReactiveFormsModule
  ]
})
export class StockModule { }
