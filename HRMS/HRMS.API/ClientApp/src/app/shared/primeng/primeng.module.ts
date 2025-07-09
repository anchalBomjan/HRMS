import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { TableModule } from 'primeng/table';
import { InputTextModule } from 'primeng/inputtext';
import { DropdownModule } from 'primeng/dropdown';
import { ToolbarModule } from 'primeng/toolbar'; 
import { DialogModule } from 'primeng/dialog';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { MultiSelectModule } from 'primeng/multiselect';
import { ChipModule } from 'primeng/chip';




@NgModule({
  declarations: [],
  imports: [
    FormsModule,
    ButtonModule,
    TableModule,
   
    MultiSelectModule,
    ChipModule,
    InputTextModule,
    DropdownModule,
    ToolbarModule,
    DialogModule,
    ToastModule
  ],
  exports:[ FormsModule,
 
    ButtonModule,
    TableModule,
    MultiSelectModule,
    InputTextModule,
    DropdownModule,
    ToolbarModule,
    DialogModule,
    ToastModule
  ],
  providers:[MessageService]

  
})
export class PrimengModule { }
