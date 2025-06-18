import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

// PrimeNG UI modules
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
import { CalendarModule } from 'primeng/calendar';
import { InputNumberModule } from 'primeng/inputnumber';
import { TagModule } from 'primeng/tag';
import { MultiSelectModule } from 'primeng/multiselect';

import { MessageService, ConfirmationService } from 'primeng/api';

@NgModule({
  imports: [
    CommonModule,
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
    MultiSelectModule
  ],
  exports: [
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
    MultiSelectModule
  ],
  providers: [
    MessageService,
    ConfirmationService
  ]
})
export class PrimengModule { }
