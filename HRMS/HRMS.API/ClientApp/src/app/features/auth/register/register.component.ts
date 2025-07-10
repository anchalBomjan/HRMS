import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/core/services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  registerForm: FormGroup;

  showDialog: boolean = false;
  dialogMessage: string = '';
  dialogTitle: string = '';

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.registerForm = this.fb.group({
      fullName: ['', Validators.required],
      userName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      confirmationPassword: ['', Validators.required],
    });
  }

  onSubmit(): void {
    if (this.registerForm.invalid) {
      this.registerForm.markAllAsTouched();
      return;
    }

    const { password, confirmationPassword } = this.registerForm.value;

    if (password !== confirmationPassword) {
      this.openDialog('Error', 'Passwords do not match!');
      return;
    }

    this.authService.register(this.registerForm.value).subscribe({
      next: (res) => {
        if (res === 1) {
          this.openDialog('Success', 'Registration successful!');
          this.registerForm.reset();
          setTimeout(() => this.router.navigate(['/auth/login']), 2000);
        } else {
          this.openDialog('Failed', 'Registration failed!');
        }
      },
      error: (err) => {
        console.error(err);
        this.openDialog('Error', 'Something went wrong!');
      },
    });
  }

  openDialog(title: string, message: string): void {
    this.dialogTitle = title;
    this.dialogMessage = message;
    this.showDialog = true;
  }


  
}

