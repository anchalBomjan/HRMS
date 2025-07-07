// import { Component } from '@angular/core';
// import { IStockAssignmentDTO } from 'src/app/core/models/stock-assignment-dto.model';
// import { AssignstockapiService } from 'src/app/core/services/assignstockapi.service';

// @Component({
//   selector: 'app-assign-stock-list',
//   templateUrl: './assign-stock-list.component.html',
//   styleUrls: ['./assign-stock-list.component.scss']
// })
// export class AssignStockListComponent {
//   stockAssignments: IStockAssignmentDTO[] = [];

//   constructor(private assignService: AssignstockapiService) {
//     this.loadAssignments();
//   }

//   loadAssignments(): void {
//     this.assignService.getAllStockAssignments().subscribe(data => {
//       this.stockAssignments = data;
//       console.log('stockassigned',this.stockAssignments)
//     });
//   }

// }
import { Component } from '@angular/core';
import { IStockAssignmentDTO } from 'src/app/core/models/stock-assignment-dto.model';
import { AssignstockapiService } from 'src/app/core/services/assignstockapi.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { Employee } from 'src/app/core/models/Employee';
import { Stock } from 'src/app/core/models/stock';
import { EmployeeApiServiceService } from 'src/app/core/services/employee-api-service.service';
import { StockApiService } from 'src/app/core/services/stock-api.service';

@Component({
  selector: 'app-assign-stock-list',
  templateUrl: './assign-stock-list.component.html',
  styleUrls: ['./assign-stock-list.component.scss'],
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
    private stockApiService:StockApiService,
    private employeeApiService:EmployeeApiServiceService,
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
    this.isEditMode = false;
    this.selectedAssignmentId = null;
    this.dialogVisible = true;
  }

  openEditDialog(assignment: IStockAssignmentDTO): void {
    this.assignForm.patchValue({
      employeeId: assignment.id,
      stockId: assignment.assignedQuantity,
      assignedQuantity: assignment.assignedQuantity,
    });

    this.selectedAssignmentId = assignment.id;
    this.isEditMode = true;
    this.dialogVisible = true;
  }

  submit(): void {
    const formData = this.assignForm.value;

    if (this.isEditMode && this.selectedAssignmentId !== null) {
      const updateData = { id: this.selectedAssignmentId, ...formData };
      this.assignService.updateStockAssignment(this.selectedAssignmentId, updateData).subscribe(() => {
        this.messageService.add({ severity: 'success', summary: 'Updated successfully' });
        this.dialogVisible = false;
        this.loadAll();
      });
    } else {
      this.assignService.assignStock(formData).subscribe(() => {
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
