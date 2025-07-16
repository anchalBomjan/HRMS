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

  dialogVisible = false;
  deleteDialogVisible = false;
  isEditMode = false;

  currentAssignment: IStockAssignmentDTO | null = null;
  selectedAssignmentId: number | null = null;

  constructor(
    private assignService: AssignstockapiService,
    private stockApiService: StockApiService,
    private employeeApiService: EmployeeApiServiceService,
    private messageService: MessageService
  ) {
    this.loadAll();
  }

  loadAll(): void {
    this.assignService.getAllStockAssignments().subscribe(data => this.stockAssignments = data);
    this.employeeApiService.GetAllEmployee().subscribe(data => this.employees = data);
    this.stockApiService.getAllStocks().subscribe(data => this.stocks = data);
  }

  openCreateDialog() {
    this.currentAssignment = null;
    this.isEditMode = false;
    this.dialogVisible = true;
  }

  openEditDialog(assignment: IStockAssignmentDTO) {
    this.currentAssignment = assignment;
    this.isEditMode = true;
    this.dialogVisible = true;
  }

  openDeleteDialog(id: number) {
    this.selectedAssignmentId = id;
    this.deleteDialogVisible = true;
  }

  cancelDelete() {
    this.deleteDialogVisible = false;
    this.selectedAssignmentId = null;
  }

  confirmDelete() {
    if (this.selectedAssignmentId !== null) {
      this.assignService.deleteStockAssignment(this.selectedAssignmentId).subscribe(() => {
        this.messageService.add({ severity: 'success', summary: 'Deleted successfully' });
        this.deleteDialogVisible = false;
        this.selectedAssignmentId = null;
        this.loadAll();
      });
    }
  }

  onFormSaved() {
    this.dialogVisible = false;
    this.loadAll();
  }



}
