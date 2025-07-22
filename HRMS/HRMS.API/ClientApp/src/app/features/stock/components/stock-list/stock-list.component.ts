
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { MessageService } from "primeng/api";
import { Stock } from "src/app/core/models/stock";
import { StockType } from "src/app/core/models/stock-type.enum";
import { StockApiService } from "src/app/core/services/stock-api.service";
import { Component } from '@angular/core';

@Component({
  selector: 'app-stock-list',
  templateUrl: './stock-list.component.html',
  styleUrls: ['./stock-list.component.scss'],
})
export class StockListComponent {

  stocks: Stock[] = [];
  stockForm: FormGroup;
  selectedStockId?: number;

  createDialogVisible = false;
  editDialogVisible = false;
  deleteDialogVisible = false;
  stockToDeleteId?: number;

  stockTypes = [
    { label: 'Unknown', value: StockType.UnKnown },
    { label: 'Asset', value: StockType.Asset },
    { label: 'Consumable', value: StockType.Consumable }
  ];

  stockTypeLabels: { [key: number]: string } = {
    0: 'Unknown',
    1: 'Asset',
    2: 'Consumable'
  };

  constructor(
    private fb: FormBuilder,
    private stockService: StockApiService,
    private messageService: MessageService
  ) {
    this.stockForm = this.fb.group({
      name: ['', Validators.required],
      description: [''],
      stockType: [StockType.UnKnown, Validators.required],
      quantity: [0, [Validators.required, Validators.min(0)]],
      isDozen: [false],
      isAdditive: [true]
    });

    this.loadStocks();
  }

  loadStocks() {
    this.stockService.getAllStocks().subscribe({
      next: (data) => {


        console.log('fetch Stock Data:',data);
        this.stocks = data.map(stock => ({
          ...stock,
          stockType: this.mapStockType(stock.stockType)
        }));
      },
      error: () =>
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to load stocks' })
    });
  }

  mapStockType(type: any): StockType {
    if (typeof type === 'string') {
      switch (type.toLowerCase()) {
        case 'asset': return StockType.Asset;
        case 'consumable': return StockType.Consumable;
        default: return StockType.UnKnown;
      }
    }
    return type;
  }

  getStockTypeLabel(type: StockType): string {
    return this.stockTypes.find(t => t.value === type)?.label ?? 'Unknown';
  }

  showCreateDialog() {
    this.createDialogVisible = true;
  }

  onStockCreated() {
    this.createDialogVisible = false;
    this.loadStocks();
  }

  onCancelCreate() {
    this.createDialogVisible = false;
  }

  // showEditDialog(stock: Stock) {
  //   this.selectedStockId = stock.id;
  //   this.stockForm.patchValue({
  //     name: stock.name,
  //     description: stock.description,
  //     stockType: this.mapStockType(stock.stockType),
  //     quantity: stock.quantity,
  //     isDozen: stock.isDozen,
  //     isAdditive: stock.isAdditive
  //   });
  //   this.editDialogVisible = true;
  // }
  showEditDialog(stock: Stock) {
    console.log('Editing stock:', stock); // Check values here
    this.selectedStockId = stock.id;
    this.stockForm.patchValue({
      name: stock.name,
      description: stock.description,
      stockType: this.mapStockType(stock.stockType),
      quantity: stock.quantity,
      isDozen: stock.isDozen,
      isAdditive: stock.isAdditive
    });
    this.editDialogVisible = true;
  }
  
  hideEditDialog() {
    this.editDialogVisible = false;
    this.stockForm.reset();
  }

  saveStock() {
    if (this.stockForm.invalid) {
      this.stockForm.markAllAsTouched();
      return;
    }

    const formValue = this.stockForm.value;
    const payload: Stock = {
      id: this.selectedStockId,
      name: formValue.name,
      description: formValue.description,
      stockType: formValue.stockType,
      quantity: formValue.quantity,
      isDozen: formValue.isDozen,
      isAdditive: formValue.isAdditive
    };
     console.log('Payload sent to backend:', payload);

    if (this.selectedStockId) {
      this.stockService.updateStock(this.selectedStockId, payload).subscribe({
        next: () => {
          this.showSuccess('Stock updated successfully');
          this.editDialogVisible = false;
          this.loadStocks();
        },
        error: () => this.showError('Failed to update stock')
      });
    }
  }

  openDeleteDialog(stockId: number) {
    this.stockToDeleteId = stockId;
    this.deleteDialogVisible = true;
  }

  confirmDelete() {
    if (!this.stockToDeleteId) return;

    this.stockService.deleteStock(this.stockToDeleteId).subscribe({
      next: () => {
        this.showSuccess('Stock deleted successfully');
        this.stocks = this.stocks.filter(s => s.id !== this.stockToDeleteId);
        this.deleteDialogVisible = false;
      },
      error: () => this.showError('Failed to delete stock')
    });
  }

  cancelDelete() {
    this.deleteDialogVisible = false;
    this.stockToDeleteId = undefined;
  }

  private showSuccess(message: string) {
    this.messageService.add({
      severity: 'success',
      summary: 'Success',
      detail: message
    });
  }

  private showError(message: string) {
    this.messageService.add({
      severity: 'error',
      summary: 'Error',
      detail: message
    });
  }



}
