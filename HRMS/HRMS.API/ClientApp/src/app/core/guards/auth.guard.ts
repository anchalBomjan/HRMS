import { inject } from "@angular/core";
import { TokenStorageService } from "../services/token-storage.service";
import { CanActivateFn, Router } from "@angular/router";
import { jwtDecode } from "jwt-decode";

export const authGuard: CanActivateFn = () => {
  const token = inject(TokenStorageService).getToken();
  const router = inject(Router);

  if (token) {
    try {
      const decoded: any = jwtDecode(token);
      if (decoded.exp * 1000 > Date.now()) return true;
    } catch {}
  }

  router.navigate(['/auth/login']);
  return false;
};
