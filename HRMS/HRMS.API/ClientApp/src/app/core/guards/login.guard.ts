import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { TokenStorageService } from '../services/token-storage.service';

export const loginGuard: CanActivateFn = () => {
  const tokenService = inject(TokenStorageService);
  const router = inject(Router);

  const user = tokenService.getUser();

  if (user?.role === 'HR') {
    router.navigate(['/hr/app-dashboard']);
    return false;
  } else if (user?.role === 'User') {
    router.navigate(['/user/dashboard']);
    return false;
  }

  return true;
};
