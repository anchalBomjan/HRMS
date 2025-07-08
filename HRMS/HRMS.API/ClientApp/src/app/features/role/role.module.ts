import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RoleRoutingModule } from './role-routing.module';
import { AssignRoleCreateComponent } from './compoenents/assign-role-create/assign-role-create.component';
import { AssignRoleEditComponent } from './components/assign-role-edit/assign-role-edit.component';


@NgModule({
  declarations: [
    AssignRoleCreateComponent,
    AssignRoleEditComponent
  ],
  imports: [
    CommonModule,
    RoleRoutingModule
  ]
})
export class RoleModule { }
