import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService); // inject AuthService
  const router = inject(Router);           // inject Router
  const expectedRole = route.data['role']; // get expected role from route data
  const userRole = authService.getUserRole(); // get user role from AuthService

  if (authService.isLoggedIn() && userRole === expectedRole) {
    return true;  // allow access if logged in and role matches
  }

  router.navigate(['login']); // otherwise redirect to login
  return false;
};
