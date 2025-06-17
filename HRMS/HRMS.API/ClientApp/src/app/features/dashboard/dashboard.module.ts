import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DashboardRoutingModule } from './dashboard-routing.module';
import { DashboardHomeComponent } from './components/dashboard-home/dashboard-home.component';
import { StockOverviewComponent } from './components/stock-overview/stock-overview.component';
import { EmployeeSummaryComponent } from './components/employee-summary/employee-summary.component';
import { UnauthorizedComponent } from './components/unauthorized/unauthorized.component';


@NgModule({
  declarations: [
    DashboardHomeComponent,
    StockOverviewComponent,
    EmployeeSummaryComponent,
    UnauthorizedComponent
  ],
  imports: [
    CommonModule,
    DashboardRoutingModule
  ]
})
export class DashboardModule { }
