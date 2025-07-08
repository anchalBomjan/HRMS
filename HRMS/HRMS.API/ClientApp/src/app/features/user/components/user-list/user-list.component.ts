import { Component } from '@angular/core';
import { MessageService } from 'primeng/api';
import { AssignUserRole } from 'src/app/core/models/assign-user-role';
import { RoleDTO } from 'src/app/core/models/role';
import { IUserDTO } from 'src/app/core/models/user-response-Dto';
import { RoleApiService } from 'src/app/core/services/role-api.service';
import { UserApiService } from 'src/app/core/services/user-api.service';


@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent {
  users: IUserDTO[] = [];
  selectedUser: IUserDTO | null = null;

  availableRoles: RoleDTO[] = [];
  selectedRoles: string[] = [];

  displayAssignDialog: boolean = false;

  constructor(
    private userApi: UserApiService,
    private roleApi: RoleApiService,
    private messageService: MessageService
  ) {
    this.loadUsers();
    this.loadRoles();
  }

  loadUsers(): void {
    this.userApi.getAllUsers().subscribe({
      next: (data) => {
        console.log('Fetched Users:', data);
        this.users = data;
      },
      error: (err) => console.error('Error loading users:', err)
    });
  }
  loadRoles(): void {
    this.roleApi.getAllRoles().subscribe({
      next: (data) => {
        console.log('Fetched Roles:', data); 
        this.availableRoles = data;
      },
      error: (err) => console.error('Error loading roles:', err)
    });
  }

  openAssignDialog(): void {
    this.selectedUser = null;
    this.selectedRoles = [];
    this.displayAssignDialog = true;
  }

  onAssignRoles(): void {
    if (!this.selectedUser || this.selectedRoles.length === 0) {
      this.messageService.add({
        severity: 'warn',
        summary: 'Validation',
        detail: 'Select user and at least one role.'
      });
      return;
    }

    const data: AssignUserRole = {
      userName: this.selectedUser.userName, // correct!
      roles: this.selectedRoles
    };

    this.userApi.assignRoles(data).subscribe({
      next: () => {
        this.messageService.add({
          severity: 'success',
          summary: 'Success',
          detail: 'Roles assigned successfully.'
        });
        this.displayAssignDialog = false;
        this.loadUsers();
      },
      error: (err) => {
        console.error(err);
        this.messageService.add({
          severity: 'error',
          summary: 'Error',
          detail: 'Failed to assign roles.'
        });
      }
    });
  }

  onDeleteUser(id: string): void {
    if (confirm("Are you sure you want to delete this user?")) {
      this.userApi.deleteUser(id).subscribe({
        next: () => {
          this.messageService.add({
            severity: 'success',
            summary: 'Deleted',
            detail: 'User deleted successfully.'
          });
          this.loadUsers();
        },
        error: (err) => {
          console.error(err);
          this.messageService.add({
            severity: 'error',
            summary: 'Error',
            detail: 'Failed to delete user.'
          });
        }
      });
    }
  }
  
  
}
