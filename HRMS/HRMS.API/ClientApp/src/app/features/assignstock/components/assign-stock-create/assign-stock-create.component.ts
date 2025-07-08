import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { Employee } from 'src/app/core/models/Employee';
import { ICreateStockAssignment } from 'src/app/core/models/createstockassignment';
import { Stock } from 'src/app/core/models/stock';
import { AssignstockapiService } from 'src/app/core/services/assignstockapi.service';
import { EmployeeApiServiceService } from 'src/app/core/services/employee-api-service.service';
import { StockApiService } from 'src/app/core/services/stock-api.service';

@Component({
  selector: 'app-assign-stock-create',
  templateUrl: './assign-stock-create.component.html',
  styleUrls: ['./assign-stock-create.component.scss']
})
export class AssignStockCreateComponent {



  assignForm: FormGroup;
  employees: Employee[] = [];
  stocks: Stock[] = [];

  constructor(
    private fb: FormBuilder,
    private messageService: MessageService,
    private assignStockApi: AssignstockapiService,
    private employeeApiService: EmployeeApiServiceService,
    private stockApiService: StockApiService
  ) {
    this.assignForm = this.fb.group({
      employeeId: [null, Validators.required],
      stockId: [null, Validators.required],
      assignedQuantity: [null, [Validators.required, Validators.min(1)]]
    });

    this.loadEmployees();
    this.loadStocks();
  }

  loadEmployees(): void {
    this.employeeApiService.GetAllEmployee().subscribe({
      next: (res) => (this.employees = res),
      error: () =>
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to load employees' })
    });
  }

  loadStocks(): void {
    this.stockApiService.getAllStocks().subscribe({
      next: (res) => (this.stocks = res),
      error: () =>
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to load stocks' })
    });
  }

  submitAssignment(): void {
    if (this.assignForm.invalid) {
      this.messageService.add({ severity: 'warn', summary: 'Warning', detail: 'Please fill required fields' });
      return;
    }

    const payload: ICreateStockAssignment = this.assignForm.value;

    this.assignStockApi.assignStock(payload).subscribe({
      next: () => {
        this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Stock assigned successfully' });
        this.assignForm.reset();
      },
      error: (err) => {
        console.error(err);
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to assign stock' });
      }
    });
  }
 

}
