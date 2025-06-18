import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

// PrimeNG Modules
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { TableModule } from 'primeng/table';
import { ToastModule } from 'primeng/toast';
import { ToolbarModule } from 'primeng/toolbar';
import { DialogModule } from 'primeng/dialog';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { DropdownModule } from 'primeng/dropdown';
import { MenubarModule } from 'primeng/menubar';
import { CardModule } from 'primeng/card';
import { SidebarModule } from 'primeng/sidebar';
import { PanelMenuModule } from 'primeng/panelmenu';
import { MessageService } from 'primeng/api';
import { ConfirmationService } from 'primeng/api';
import { CalendarModule } from 'primeng/calendar';
import { InputNumberModule } from 'primeng/inputnumber';
import { TagModule } from 'primeng/tag';
import { MultiSelectModule } from 'primeng/multiselect';

const PRIME_MODULES = [
  ButtonModule,
  InputTextModule,
  TableModule,
  ToastModule,
  ToolbarModule,
  DialogModule,
  ConfirmDialogModule,
  DropdownModule,
  MenubarModule,
  CardModule,
  SidebarModule,
  PanelMenuModule,
  CalendarModule,
  InputNumberModule,
  TagModule,
  MultiSelectModule,
  ToastModule
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    ...PRIME_MODULES
  ],
  exports: [
    ...PRIME_MODULES
  ],
  providers: [
    MessageService,
    ConfirmationService
  ]
})
export class PrimengModule { }