import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { Employee } from 'src/app/core/models/Employee';
import { Stock } from 'src/app/core/models/stock';
import { IStockAssignmentDTO } from 'src/app/core/models/stock-assignment-dto.model';
import { AssignstockapiService } from 'src/app/core/services/assignstockapi.service';

@Component({
  selector: 'app-assign-stock-create',
  templateUrl: './assign-stock-create.component.html',
  styleUrls: ['./assign-stock-create.component.scss']
})
export class AssignStockCreateComponent {

  @Input() visible = false;
  @Input() mode: 'create' | 'edit' = 'create';
  @Input() assignment: IStockAssignmentDTO | null = null;
  @Input() employees: Employee[] = [];
  @Input() stocks: Stock[] = [];
  @Output() formSaved = new EventEmitter<void>();
  @Output() dialogClosed = new EventEmitter<void>();

  assignForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private assignService: AssignstockapiService,
    private messageService: MessageService
  ) {
    this.assignForm = this.fb.group({
      employeeId: [null, Validators.required],
      stockId: [null, Validators.required],
      assignedQuantity: [1, [Validators.required, Validators.min(1)]],
    });
  }

  ngOnChanges() {
    if (this.assignment && this.mode === 'edit') {
      this.assignForm.patchValue({
        assignedQuantity: this.assignment.assignedQuantity,
        employeeId: this.assignment.id,
        stockId: this.assignment.id,
      });
      this.assignForm.get('employeeId')?.disable();
      this.assignForm.get('stockId')?.disable();
    } else {
      this.assignForm.reset();
      this.assignForm.get('employeeId')?.enable();
      this.assignForm.get('stockId')?.enable();
    }
  }

  submit() {
    if (this.assignForm.invalid) return;
       // Before sending to backend in your submit method, convert to numbers explicitly:    
      // [optionValue]="employee.id" <!-- explicitly ensure number type --> same like for stockId
               

    const payload = this.assignForm.getRawValue();

    if (this.mode === 'edit' && this.assignment?.id) {
      this.assignService.updateStockAssignment(this.assignment.id, {
        id: this.assignment.id,
        assignedQuantity: payload.assignedQuantity
      }).subscribe({
        next: () => {
          this.messageService.add({ severity: 'success', summary: 'Updated successfully' });
          this.formSaved.emit();
        },
        error: () => {
          this.messageService.add({ severity: 'error', summary: 'Update Failed' });
        }
      });
    } else {
      this.assignService.assignStock(payload).subscribe({
        next: () => {
          this.messageService.add({ severity: 'success', summary: 'Stock Assigned' });
          this.assignForm.reset();
          this.formSaved.emit();
        },
        error: () => {
          this.messageService.add({ severity: 'error', summary: 'Creation Failed' });
        }
      });
    }
  }

  onCancel(): void {
    this.assignForm.reset();
    this.dialogClosed.emit();
  } 
}
