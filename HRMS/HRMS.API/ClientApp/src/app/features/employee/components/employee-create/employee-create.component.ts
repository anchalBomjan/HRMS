import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { EmployeeApiServiceService } from 'src/app/core/services/employee-api-service.service';

@Component({
  selector: 'app-employee-create',
  templateUrl: './employee-create.component.html',
  styleUrls: ['./employee-create.component.scss']
})
export class EmployeeCreateComponent {
  employeeForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private employeeService: EmployeeApiServiceService,
    private router: Router,
    private messageService: MessageService
  ) {
    this.employeeForm = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', Validators.required]
    });
  }

  onSubmit() {
    if (this.employeeForm.valid) {
      this.employeeService.CreateEmployee(this.employeeForm.value).subscribe({
        next: (res) => {

          console.log('employee posting data',res)
          this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Employee created successfully' });
          this.router.navigate(['/app-dashboard/employees']);
        },
        error: () => {
          this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to create employee' });
        }
      });
    } else {
      this.messageService.add({ severity: 'warn', summary: 'Validation', detail: 'Please fill all required fields' });
    }
  }
}
