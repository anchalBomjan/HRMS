import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { Employee } from 'src/app/core/models/Employee';
import { EmployeeApiServiceService } from 'src/app/core/services/employee-api-service.service';
@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.scss']
})
export class EmployeeListComponent {

  employees:Employee[]=[]


  constructor(
    private employeeService:EmployeeApiServiceService,
    private messageservice:MessageService,
    public router:Router
    )
  {

    this.loadEmployee();

  }
  loadEmployee(){
    // this.employeeService.GetAllEmployee().subscribe(data=> this.employees=data );
 
    this.employeeService.GetAllEmployee().subscribe({
      next:(data)=>{
        this.employees=data;
        console.log('fetch employees:',data);
      }
    })
    
  }

  editEmployee(id: number) {
    this.router.navigate(['/home/app-dashboard/employees/edit', id]);
  }
  
  deleteEmployee(id: number) {
    if (confirm('Are you sure you want to delete this employee?')) {
      this.employeeService.deleteEmployee(id).subscribe({
        next: (res: string) => {
          this.messageservice.add({
            severity: 'success',
            summary: 'Deleted',
            detail: res,  // res is a string like "Employee with ID 5 deleted successfully."
          });
  
          this.loadEmployee(); // refresh the list
        },
        error: (err) => {
          this.messageservice.add({
            severity: 'error',
            summary: 'Error',
            detail: err.error?.message || 'Failed to delete employee.'
          });
        }
      });
    }
  }
  
}
