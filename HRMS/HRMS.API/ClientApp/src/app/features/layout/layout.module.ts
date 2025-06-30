import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LayoutRoutingModule } from './layout-routing.module';
import { PageFooterComponent } from './components/page-footer/page-footer.component';
import { PageSideNavComponent } from './components/page-side-nav/page-side-nav.component';


@NgModule({
  declarations: [
    PageFooterComponent,
    PageSideNavComponent
  ],
  imports: [
    CommonModule,
    LayoutRoutingModule
  ]
})
export class LayoutModule { }
