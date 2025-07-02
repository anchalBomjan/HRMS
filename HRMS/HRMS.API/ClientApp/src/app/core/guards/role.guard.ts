import { jwtDecode } from "jwt-decode";
import { TokenStorageService } from "../services/token-storage.service";
import { ActivatedRouteSnapshot, CanActivateFn, Router } from "@angular/router";
import { inject } from "@angular/core";

export const roleGuard: CanActivateFn = (route: ActivatedRouteSnapshot) => {
  const token = inject(TokenStorageService).getToken();
  const router = inject(Router);
  const expectedRole = route.data['expectedRole'];

  if (token) {
    const decoded: any = jwtDecode(token);
    if (decoded.role === expectedRole) return true;
  }

  router.navigate(['/access-denied']);
  return false;
};
//  dont use authrole guard