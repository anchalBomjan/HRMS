import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StockListComponent } from './components/stock-list/stock-list.component';
import { StockAssignmentsComponent } from './components/stock-assignments/stock-assignments.component';
import { StockEditorComponent } from './components/stock-editor/stock-editor.component';



@NgModule({
  declarations: [
    StockListComponent,
    StockAssignmentsComponent,
    StockEditorComponent
  ],
  imports: [
    CommonModule
  ]
})
export class StockManagementModule { }
