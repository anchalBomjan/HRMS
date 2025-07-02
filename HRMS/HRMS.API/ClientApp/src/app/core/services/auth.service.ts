import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, tap } from 'rxjs';
import { TokenStorageService } from './token-storage.service';
import { AuthRequest } from '../models/AuthRequest';
import { AuthResponse } from '../models/AuthResponse';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = 'https://localhost:44372/api/Auth/Login';
  private isLoggedInSubject = new BehaviorSubject<boolean>(false);
  isLoggedIn$ = this.isLoggedInSubject.asObservable();

  constructor(
    private http: HttpClient,
    private tokenService: TokenStorageService,
    private router: Router
  ) {}

  login(data: AuthRequest) {
    return this.http.post<AuthResponse>(this.apiUrl, data).pipe(
      tap((response) => {
        this.tokenService.saveToken(response.token);
        this.tokenService.saveUser(response);
        this.isLoggedInSubject.next(true);
        this.redirectToDashboard(response.role);
      })
    );
  }

  logout() {
    this.tokenService.signOut();
    this.isLoggedInSubject.next(false);
    this.router.navigate(['/auth/login']);
  }

  private redirectToDashboard(role: string) {
    if (role === 'Hr') {
      this.router.navigate(['/hr/dashboard']);
    } else if (role === 'User') {
      this.router.navigate(['/user/dashboard']);
    } else {
      this.router.navigate(['/access-denied']);
    }
  }
}
