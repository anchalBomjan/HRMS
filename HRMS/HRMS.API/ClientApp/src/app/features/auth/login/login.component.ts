import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
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
    // Match form controls exactly with backend DTO casing
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

        console.log(response);
        this.tokenStorage.saveToken(response.token);
        const user = {
          userName: response.userName,
          role: response.role
        };
        this.tokenStorage.saveUser(user);

        this.messageService.add({
          severity: 'success',
          summary: 'Login Successful',
          detail: `Welcome ${response.userName}!`,
          life: 3000
        });

        if (response.role === 'Hr' || response.role === 'User') {
          this.router.navigate(['/hr/dashboard']);
        } else if (response.role === 'User') {
          this.router.navigate(['/user/dashboard']);
        } else {
          this.router.navigate(['/access-denied']);
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
