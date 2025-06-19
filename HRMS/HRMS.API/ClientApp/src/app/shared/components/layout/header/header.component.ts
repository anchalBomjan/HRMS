import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent {
  @Input() panelName: string = '';
  appTitle = 'Library Management System';
  
  loggedIn = true;
  name = 'Admin User'; // This should be 'name' not 'username'

  constructor(private router: Router) {}

  logout() {
    console.log('Logging out...');
    this.router.navigate(['/login']);
  }
}