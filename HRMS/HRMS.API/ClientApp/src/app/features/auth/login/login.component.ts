import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';
import { MessageService } from 'primeng/api';
import { AuthService } from 'src/app/core/services/auth.service';
import { TokenStorageService } from 'src/app/core/services/token-storage.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  loginForm: FormGroup;
  isSubmitted = false;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private authService: AuthService,
    private tokenStorage: TokenStorageService,
    private messageService: MessageService
  ) {
    this.loginForm = this.fb.group({
      UserName: ['', Validators.required],
      Password: ['', Validators.required]
    });
  }

  onSubmit() {
    this.isSubmitted = true;
    if (this.loginForm.invalid) return;

    const credentials = this.loginForm.value;

    this.authService.login(credentials).subscribe({
      next: (response) => {
        const token = response.token;

        try {
          const decoded: any = jwtDecode(token);
          const role = decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
          const userName = decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];

          console.log('role and userName',role,userName);
          // Save token and user
          this.tokenStorage.saveToken(token);
          this.tokenStorage.saveUser({ userName, role });

          this.messageService.add({
            severity: 'success',
            summary: 'Login Successful',
            detail: `Welcome ${userName}`,
            life: 3000
          });

          // Navigate by role
          if (role === 'HR' || role === 'User') {
            this.router.navigate(['/hr/app-dashboard']);
          } else if (role === 'User') {
            this.router.navigate(['/user/dashboard']);
          } else {
            this.router.navigate(['/access-denied']);
          }
        } catch (err) {
          console.error('Token decode error:', err);
          this.messageService.add({
            severity: 'error',
            summary: 'Login Failed',
            detail: 'Invalid token format',
            life: 3000
          });
        }
      },
      error: () => {
        this.messageService.add({
          severity: 'error',
          summary: 'Login Failed',
          detail: 'Invalid username or password',
          life: 3000
        });
      }
    });
  }

  goToRegister() {
    this.router.navigate(['/auth/register']);
  }

  goToForgotPassword() {
    this.router.navigate(['/auth/forgot-password']);
  }
}
