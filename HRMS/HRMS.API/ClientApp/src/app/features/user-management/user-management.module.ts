import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserListComponent } from './components/user-list/user-list.component';
import { UserRolesComponent } from './components/user-roles/user-roles.component';
import { RoleEditorComponent } from './components/role-editor/role-editor.component';



@NgModule({
  declarations: [
    UserListComponent,
    UserRolesComponent,
    RoleEditorComponent
  ],
  imports: [
    CommonModule
  ]
})
export class UserManagementModule { }
