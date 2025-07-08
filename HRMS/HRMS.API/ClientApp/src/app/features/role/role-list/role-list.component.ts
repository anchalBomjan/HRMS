import { Component } from '@angular/core';
import { MessageService } from 'primeng/api';
import { RoleDTO } from 'src/app/core/models/role';
import { CreateRole } from 'src/app/core/models/role-create';
import { RoleApiService } from 'src/app/core/services/role-api.service';

@Component({
  selector: 'app-role-list',
  templateUrl: './role-list.component.html',
  styleUrls: ['./role-list.component.scss']
})
export class RoleListComponent {

  roles: RoleDTO[] = [];
  displayDialog: boolean = false;
  isEditMode: boolean = false;
  selectedRoleId: string = '';
  roleForm: CreateRole = { roleName: '' };

  constructor(private roleApi: RoleApiService, private messageService: MessageService) {
    this.loadRoles();
  }

  loadRoles() {
    this.roleApi.getAllRoles().subscribe((data) => {
      this.roles = data;
      console.log('loaded Role...',data);
    });
  }

  openCreateDialog() {
    this.roleForm = { roleName: '' };
    this.isEditMode = false;
    this.displayDialog = true;
  }

  openEditDialog(role: RoleDTO) {
    this.roleForm = { roleName: role.roleName };
    this.selectedRoleId = role.id.toString(); 
    this.isEditMode = true;
    this.displayDialog = true;
  }

  saveRole() {
    if (this.isEditMode) {
      const role: RoleDTO = {
        id: this.selectedRoleId,
        roleName: this.roleForm.roleName
      };
      this.roleApi.editRole(this.selectedRoleId, role).subscribe(() => {
        this.messageService.add({ severity: 'success', summary: 'Updated', detail: 'Role updated successfully' });
        this.displayDialog = false;
        this.loadRoles();
      });
    } else {
      this.roleApi.createRole(this.roleForm).subscribe(() => {
        this.messageService.add({ severity: 'success', summary: 'Created', detail: 'Role created successfully' });
        this.displayDialog = false;
        this.loadRoles();
      });
    }
  }

  confirmDelete(role: RoleDTO) {
    if (confirm(`Are you sure to delete role "${role.roleName}"?`)) {
      this.roleApi.deleteRole(role.id.toString()).subscribe(() => {
        this.messageService.add({ severity: 'warn', summary: 'Deleted', detail: 'Role deleted' });
        this.loadRoles();
      });
    }
  }

}
