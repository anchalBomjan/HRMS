import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { BehaviorSubject, tap } from 'rxjs';
import { TokenStorageService } from './token-storage.service';
import { AuthRequest } from '../models/AuthRequest';
import { AuthResponse } from '../models/AuthResponse';
import { jwtDecode } from 'jwt-decode';
import { CreateUser } from '../models/create-user';
@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = 'https://localhost:44372/api/Auth';
  private isLoggedInSubject = new BehaviorSubject<boolean>(false);
  isLoggedIn$ = this.isLoggedInSubject.asObservable();

  constructor(
    private http: HttpClient,
    private tokenService: TokenStorageService,
    private router: Router
  ) {}

  login(data: AuthRequest) {
    return this.http.post<AuthResponse>(`${this.apiUrl}/Login`, data)
   .pipe(
      tap((response) => {
        const token = response.token;
        const decoded: any = jwtDecode(token);
        const role = decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
        const userName = decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];

        const userId = decoded['userId']; // 

        this.tokenService.saveToken(token);
        this.tokenService.saveUser({ userName, role ,userId});
        this.isLoggedInSubject.next(true);

        this.redirectToDashboard(role);
      })
    );
  }

  logout() {
    this.tokenService.signOut();
    this.isLoggedInSubject.next(false);
    this.router.navigate(['/auth/login']);
  }

  private redirectToDashboard(role: string) {
    if (role === 'HR' || role === 'HR' || role === 'User') {
      this.router.navigate(['/hr/app-dashboard']);
    } else if (role === 'User') {
      this.router.navigate(['/user/dashboard']);
    } else {
      this.router.navigate(['/access-denied']);
    }
  }


  register(user: CreateUser) {
    return this.http.post<number>(`${this.apiUrl}/Create`, user);
  }
}



