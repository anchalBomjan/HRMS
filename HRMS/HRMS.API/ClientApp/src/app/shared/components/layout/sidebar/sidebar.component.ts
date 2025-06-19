import { Component } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent {
  visible = true;
  
  navItems: MenuItem[] = [
    // Add your navigation items here
    { label: 'Dashboard', icon: 'pi pi-home', routerLink: '/dashboard' },
    { 
      label: 'User Management', 
      icon: 'pi pi-users',
      items: [
        { label: 'User List', routerLink: '/users' },
        { label: 'Roles', routerLink: '/roles' }
      ]
    },
    // Add more menu items as needed
  ];

  constructor(private router: Router) {}

  // Add navigation methods as needed
}