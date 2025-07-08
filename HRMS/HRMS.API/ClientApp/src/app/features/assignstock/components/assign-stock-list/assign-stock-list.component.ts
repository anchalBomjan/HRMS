import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { Employee } from 'src/app/core/models/Employee';
import { Stock } from 'src/app/core/models/stock';
import { IStockAssignmentDTO } from 'src/app/core/models/stock-assignment-dto.model';
import { AssignstockapiService } from 'src/app/core/services/assignstockapi.service';
import { EmployeeApiServiceService } from 'src/app/core/services/employee-api-service.service';
import { StockApiService } from 'src/app/core/services/stock-api.service';

@Component({
  selector: 'app-assign-stock-list',
  templateUrl: './assign-stock-list.component.html',
  styleUrls: ['./assign-stock-list.component.scss']
})
export class AssignStockListComponent {

  stockAssignments: IStockAssignmentDTO[] = [];
  employees: Employee[] = [];
  stocks: Stock[] = [];

  assignForm: FormGroup;
  dialogVisible = false;
  isEditMode = false;
  selectedAssignmentId: number | null = null;

  constructor(
    private assignService: AssignstockapiService,
    private stockApiService: StockApiService,
    private employeeApiService: EmployeeApiServiceService,
    private fb: FormBuilder,
    private messageService: MessageService
  ) {
    this.assignForm = this.fb.group({
      employeeId: [null, Validators.required],
      stockId: [null, Validators.required],
      assignedQuantity: [1, [Validators.required, Validators.min(1)]],
    });

    this.loadAll();
  }

  loadAll(): void {
    this.assignService.getAllStockAssignments().subscribe(data => this.stockAssignments = data);
    this.employeeApiService.GetAllEmployee().subscribe(data => this.employees = data);
    this.stockApiService.getAllStocks().subscribe(data => this.stocks = data);
  }

  openCreateDialog(): void {
    this.assignForm.reset();
    this.assignForm.get('employeeId')?.enable();
    this.assignForm.get('stockId')?.enable();

    this.isEditMode = false;
    this.selectedAssignmentId = null;
    this.dialogVisible = true;
  }

  openEditDialog(assignment: IStockAssignmentDTO): void {
    this.assignForm.patchValue({
      assignedQuantity: assignment.assignedQuantity
    });

    // Disable employeeId and stockId fields (not editable)
    this.assignForm.get('employeeId')?.disable();
    this.assignForm.get('stockId')?.disable();

    this.selectedAssignmentId = assignment.id;
    this.isEditMode = true;
    this.dialogVisible = true;
  }

  submit(): void {
    if (this.isEditMode && this.selectedAssignmentId !== null) {
      const updatePayload = {
        id: this.selectedAssignmentId,
        assignedQuantity: this.assignForm.get('assignedQuantity')?.value
      };

      this.assignService.updateStockAssignment(this.selectedAssignmentId, updatePayload).subscribe(() => {
        this.messageService.add({ severity: 'success', summary: 'Updated successfully' });
        this.dialogVisible = false;
        this.loadAll();
      });
    } else {
      this.assignForm.get('employeeId')?.enable();
      this.assignForm.get('stockId')?.enable();

      const createPayload = this.assignForm.value;

      this.assignService.assignStock(createPayload).subscribe(() => {
        this.messageService.add({ severity: 'success', summary: 'Created successfully' });
        this.dialogVisible = false;
        this.loadAll();
      });
    }
  }

  deleteAssignment(id: number): void {
    if (confirm('Are you sure you want to delete this assignment?')) {
      this.assignService.deleteStockAssignment(id).subscribe(() => {
        this.messageService.add({ severity: 'success', summary: 'Deleted successfully' });
        this.loadAll();
      });
    }
  }

}
