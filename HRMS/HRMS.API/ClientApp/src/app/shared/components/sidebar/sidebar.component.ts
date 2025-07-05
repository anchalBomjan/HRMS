import { Component } from '@angular/core';
import { TokenStorageService } from 'src/app/core/services/token-storage.service';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent {
  role: string | null = '';
  userName: string | null = '';
  panelName: string = '';
  navItems: { label: string, route: string }[] = [];

  constructor(private tokenService: TokenStorageService) {
    const user = this.tokenService.getUser(); // { userName, role }

    if (user && user.role) {
      this.role = user.role;
      this.userName = user.userName;
      this.panelName = `${this.role} Panel`;
      this.setNavItemsBasedOnRole(user.role);
    }
  }

  setNavItemsBasedOnRole(role: string) {
    switch (role) {
      case 'Admin':
        this.navItems = [
          { label: 'Dashboard', route: '/admin/dashboard' },
          { label: 'Manage Users', route: '/admin/users' },
          { label: 'Reports', route: '/admin/reports' }
        ];
        break;
      case 'HR':
        this.navItems = [
          { label: 'Dashboard', route: '/home/app-dashboard/' },
          { label: 'Employees', route: '/home/app-dashboard/employees' },

          {label:'Stock',route:'/home/app-dashboard/stocks'},
          {label:'AssignStock',route:'/home/app-dashboard/Assignstocks'},
          {label:'Manages User',route:'/home/app-dashboard/ManagesUser'},
          // {label:'Stock',route:'/home/app-dashboard/stocks'},


          { label: 'Leaves', route: '/hr/leaves' }
        ];
        break;
      case 'User':
        this.navItems = [
          { label: 'Dashboard', route: '/user/dashboard' },
          { label: 'My Profile', route: '/user/profile' },
          { label: 'My Requests', route: '/user/requests' }
        ];
        break;
      default:
        this.navItems = [{ label: 'Home', route: '/home' }];
    }
  }
}
