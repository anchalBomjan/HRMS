import { inject } from '@angular/core';
import { CanActivateFn, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

export const roleGuard: CanActivateFn = (
  next: ActivatedRouteSnapshot,
  state: RouterStateSnapshot
) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  const requiredRoles = next.data['roles'] as Array<string>;
  const userRole = authService.getUserRole(); // Returns string | null

  if (userRole && requiredRoles?.includes(userRole)) {
    return true;
  }

  router.navigate(['/forbidden']);
  return false;
};
