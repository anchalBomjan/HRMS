import { Component } from '@angular/core';
import { MessageService } from 'primeng/api';
import { AssignUserRole } from 'src/app/core/models/assign-user-role';
import { RoleDTO } from 'src/app/core/models/role';
import { IUserDTO } from 'src/app/core/models/user-response-Dto';
import { IUserEditDTO } from 'src/app/core/models/usereditDto';
import { RoleApiService } from 'src/app/core/services/role-api.service';
import { UserApiService } from 'src/app/core/services/user-api.service';
@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent {
 
  users: IUserEditDTO[] = [];  // includes roles as full objects
  selectedUser: IUserDTO | null = null;

  availableRoles: RoleDTO[] = [];  // roles from backend with id + roleName
  selectedRoles: string[] = [];    // role names only

  displayAssignDialog: boolean = false;
  displayEditDialog: boolean = false;

  // Used for editing user - roles are represented as string[] here
  editUserData: IUserEditDTO | null = null;



  /// for editUserRole
 


  constructor(
    private userApi: UserApiService,
    private roleApi: RoleApiService,
    private messageService: MessageService
  ) {
    this.loadUsers();
    this.loadRoles();
  }

  loadUsers(): void {
    this.userApi.getAllUserDetails().subscribe({
      next: (data) => {
        this.users = data;
        console.log('Users loaded:', this.users);
      },
      error: (err) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to load users' });
        console.error('Load users error:', err);
      }
    });
  }

  loadRoles(): void {
    this.roleApi.getAllRoles().subscribe({
      next: (data) => {
        this.availableRoles = data;
        console.log('Roles loaded:', this.availableRoles);
      },
      error: (err) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to load roles' });
        console.error('Load roles error:', err);
      }
    });
  }

  openAssignDialog(): void {
    this.selectedUser = null;
    this.selectedRoles = [];
    this.displayAssignDialog = true;
  }

  onAssignRoles(): void {
    if (!this.selectedUser || this.selectedRoles.length === 0) {
      this.messageService.add({ severity: 'warn', summary: 'Validation', detail: 'Select user and at least one role.' });
      return;
    }

    const data: AssignUserRole = {
      userName: this.selectedUser.userName,
      roles: this.selectedRoles  // string[] of role names
  

    };

    console.log('AssignUserRole payload:', data);

    this.userApi.assignRoles(data).subscribe({
      next: (resp) => {
        this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Roles assigned successfully.' });
        this.displayAssignDialog = false;
        this.loadUsers();
      },
      error: (err) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to assign roles.' });
        console.error('Assign roles error:', err);
      }
    });
  }

  onDeleteUser(id: string): void {
    if (!confirm('Are you sure you want to delete this user?')) return;

    this.userApi.deleteUser(id).subscribe({
      next: () => {
        this.messageService.add({ severity: 'success', summary: 'Deleted', detail: 'User deleted successfully.' });
        this.loadUsers();
      },
      error: (err) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to delete user.' });
        console.error('Delete user error:', err);
      }
    });
  }

  openEditUser(user: IUserEditDTO): void {
    this.editUserData = { ...user };

  

    this.displayEditDialog = true;
  }

  saveUserEdit(): void {
    if (!this.editUserData) return;

    // Attach selectedRoles as roleName string array before sending to backend
    const updatedUser = {
      ...this.editUserData,
      roles: this.selectedRoles  // string[]
    };

    console.log('Edit User payload:', updatedUser);

    this.userApi.editUserProfile(this.editUserData.id, updatedUser).subscribe({
      next: () => {
        this.messageService.add({ severity: 'success', summary: 'Success', detail: 'User updated successfully.' });
        this.displayEditDialog = false;
        this.editUserData = null;
        this.selectedRoles = [];
        this.loadUsers();
      },
      error: (err) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to update user.' });
        console.error('Edit user error:', err);
      }
    });
  }

  cancelEdit(): void {
    this.displayEditDialog = false;
    this.editUserData = null;
    this.selectedRoles = [];
  }




}


