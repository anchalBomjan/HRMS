
import { Component, ChangeDetectorRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators, AbstractControl, ValidationErrors } from '@angular/forms';
import { StockApiService } from 'src/app/core/services/stock-api.service';
import { Stock } from 'src/app/core/models/stock';
import { StockType } from 'src/app/core/models/stock-type.enum';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-stock-list',
  templateUrl: './stock-list.component.html',
  styleUrls: ['./stock-list.component.scss'],
  providers: [MessageService]
})
export class StockListComponent {
  stocks: Stock[] = [];
  stockForm: FormGroup;
  dialogVisible = false;
  isEditMode = false;
  selectedStockId?: number;

  stockTypes = [
    {label:'Unknown',value:StockType.Consumable},
    { label: 'Asset', value: StockType.Asset },
    { label: 'Consumable', value: StockType.Consumable }
  ];

  constructor(
    private fb: FormBuilder,
    private stockService: StockApiService,
    private messageService: MessageService,
    private cd: ChangeDetectorRef
  ) {
    this.stockForm = this.fb.group({
      name: ['', Validators.required],
      description: [''],
      stockType: [null, [Validators.required, this.forbidUnknownValidator]],
      quantity: [0, [Validators.required, Validators.min(0)]],
      isDozen: [false],
      isAdditive: [true] 

    });

    this.loadStocks();
  }

  forbidUnknownValidator(control: AbstractControl): ValidationErrors | null {
    return control.value === StockType.UnKnown ? { unknownSelected: true } : null;
  }

  loadStocks() {
    this.stockService.getAllStocks().subscribe({
      next: (data) => (this.stocks = data),
      error: () =>
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to load stocks' })
    });
  }

  showCreateDialog() {
    this.isEditMode = false;
    this.stockForm.reset({
      name: '',
      description: '',
      stockType: null,
      quantity: 0,
      isDozen: false
    });

    setTimeout(() => {
      this.dialogVisible = true;
      this.cd.detectChanges();
    }, 0);
  }

  showEditDialog(stock: Stock) {
    this.isEditMode = true;
    this.selectedStockId = stock.id;

    // If editing, make sure Unknown is not accepted:
    let stockTypeValue = stock.stockType === StockType.UnKnown ? null : stock.stockType;

    this.stockForm.patchValue({
      ...stock,
      stockType: stockTypeValue
    });

    setTimeout(() => {
      this.dialogVisible = true;
      this.cd.detectChanges();
    }, 0);
  }

  saveStock() {
    if (this.stockForm.invalid) {
      this.stockForm.markAllAsTouched();
      return;
    }

    const formValue: Stock = this.stockForm.value;

    console.log('create stock', formValue);
   
    if (this.isEditMode && this.selectedStockId) {
      this.stockService.updateStock(this.selectedStockId, formValue).subscribe({
        next: () => {
          this.messageService.add({ severity: 'success', summary: 'Updated', detail: 'Stock updated successfully' });
          this.dialogVisible = false;
          this.loadStocks();
        },
        error: () =>
          this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to update stock' })
      });
    } else {
      this.stockService.createStock(formValue).subscribe({
        next: () => {
          this.messageService.add({ severity: 'success', summary: 'Created', detail: 'Stock created successfully' });
          this.dialogVisible = false;
          this.loadStocks();
        },
        error: () =>
          this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to create stock' })
      });
    }
  }

  confirmDelete(stockId: number) {
    if (confirm('Are you sure you want to delete this stock?')) {
      this.stockService.deleteStock(stockId).subscribe({
        next: () => {
          this.messageService.add({ severity: 'success', summary: 'Deleted', detail: 'Stock deleted successfully' });
          this.loadStocks();
        },
        error: () =>
          this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to delete stock' })
      });
    }
  }

  hideDialog() {
    this.dialogVisible = false;
  }
}

