import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TokenStorageService } from 'src/app/core/services/token-storage.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  isLoggedIn = false;
  userName: string = '';

  constructor(private tokenService: TokenStorageService, private router: Router) {}

  ngOnInit(): void {
    const user = this.tokenService.getUser();
    this.isLoggedIn = !!user;
    this.userName = user?.userName || '';
  }

  logout(): void {
    this.tokenService.signOut();
    this.router.navigate(['/auth/login']);
  }

  toggleMobileMenu() {
    const menu = document.querySelector('.offcanvas');
    menu?.classList.toggle('show');
  }
  
}
