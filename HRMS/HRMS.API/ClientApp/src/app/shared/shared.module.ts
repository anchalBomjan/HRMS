import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './components/layout/header/header.component';
import { SidebarComponent } from './components/layout/sidebar/sidebar.component';
import { FooterComponent } from './components/layout/footer/footer.component';
import { DataGridComponent } from './components/data-grid/data-grid.component';
import { PrimengModule } from '../primeng/primeng.module';



@NgModule({
  declarations: [
    HeaderComponent,
    SidebarComponent,
    FooterComponent,
    DataGridComponent
  ],
  imports: [
    CommonModule,
    PrimengModule
  ]
})
export class SharedModule { }
