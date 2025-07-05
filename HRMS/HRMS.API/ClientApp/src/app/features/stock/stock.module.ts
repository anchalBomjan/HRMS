import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { StockRoutingModule } from './stock-routing.module';
import { StockListComponent } from './components/stock-list/stock-list.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    StockListComponent
  ],
  imports: [
    CommonModule,
    StockRoutingModule,
    SharedModule,
    ReactiveFormsModule
  ]
})
export class StockModule { }
