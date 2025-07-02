import { inject } from '@angular/core';
import { CanActivateFn, Router, ActivatedRouteSnapshot } from '@angular/router';
import { TokenStorageService } from '../services/token-storage.service';

export const roleGuard: CanActivateFn = (route: ActivatedRouteSnapshot) => {
  const tokenService = inject(TokenStorageService);
  const router = inject(Router);

  const user = tokenService.getUser();
  const expectedRole = route.data['expectedRole'];

  if (user && user.role === expectedRole) {
    return true;
  }

  router.navigate(['/access-denied']);
  return false;
};
