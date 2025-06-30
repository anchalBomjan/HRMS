import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LayoutRoutingModule } from './layout-routing.module';
import { PageFooterComponent } from './components/page-footer/page-footer.component';
import { PageSideNavComponent } from './components/page-side-nav/page-side-nav.component';
import { PageHeaderComponent } from './components/page-header/page-header.component';
import { LayoutComponent } from './layout.component';



@NgModule({
  declarations: [
    PageFooterComponent,
    PageSideNavComponent,
    PageHeaderComponent,
    LayoutComponent
  ],
  imports: [
    CommonModule,
    LayoutRoutingModule
  ],

  // exports:[
  //   PageFooterComponent,
  //   PageSideNavComponent,
  //   PageHeaderComponent,
  //   LayoutComponent



  // ]
})
export class LayoutModule { }
