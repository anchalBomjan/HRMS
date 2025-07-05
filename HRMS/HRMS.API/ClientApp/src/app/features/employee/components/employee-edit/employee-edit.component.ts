import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { EmployeeApiServiceService } from 'src/app/core/services/employee-api-service.service';

@Component({
  selector: 'app-employee-edit',
  templateUrl: './employee-edit.component.html',
  providers: [MessageService]
})
export class EmployeeEditComponent {
  employeeForm: FormGroup;
  employeeId: number;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private employeeService: EmployeeApiServiceService,
    private messageService: MessageService,
    private router: Router
  ) {
    // Get ID from route param immediately
    this.employeeId = Number(this.route.snapshot.paramMap.get('id'));

    // Initialize form
    this.employeeForm = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', Validators.required]
    });

    // Load employee data
    this.employeeService.getEmployeeById(this.employeeId).subscribe({
      next: (emp) => {
        this.employeeForm.patchValue(emp);
      },
      error: () => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Employee not found' });
        this.router.navigate(['/home/app-dashboard/employees']);
      }
    });
  }

  onSubmit() {
    if (this.employeeForm.valid) {
      this.employeeService.updateEmployee(this.employeeId, this.employeeForm.value).subscribe({
        next: (res) => {
          this.messageService.add({
            severity: 'success',
            summary: 'Updated',
            detail: 'Employee updated successfully'
          });
          setTimeout(() => {
            this.router.navigate(['/home/app-dashboard/employees']);
          }, 1500);
        },
        error: () => {
          this.messageService.add({
            severity: 'error',
            summary: 'Error',
            detail: 'Failed to update employee'
          });
        }
      });
    } else {
      this.messageService.add({
        severity: 'warn',
        summary: 'Validation',
        detail: 'Please fill all required fields'
      });
    }
  }
}
