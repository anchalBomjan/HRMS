// import { Component } from '@angular/core';
// import { FormBuilder, FormGroup, Validators } from '@angular/forms';
// import { MessageService } from 'primeng/api';
// import { Stock } from 'src/app/core/models/stock';
// import { StockType } from 'src/app/core/models/stock-type.enum';
// import { StockApiService } from 'src/app/core/services/stock-api.service';

// @Component({
//   selector: 'app-stock-list',
//   templateUrl: './stock-list.component.html',
//   styleUrls: ['./stock-list.component.scss'],

// })
// export class StockListComponent {

//   stocks: Stock[] = [];
//   stockForm: FormGroup;
//   dialogVisible = false;
//   isEditMode = false;
//   selectedStockId?: number;

//   stockTypes = Object.values(StockType);

//   constructor(
//     private stockService: StockApiService,
//     private fb: FormBuilder,
//     private messageService: MessageService
//   ) {
//     this.stockForm = this.fb.group({
//       name: ['', Validators.required],
//       description: [''],
//       stockType: [StockType.UnKnown, Validators.required],
//       quantity: [0, [Validators.required, Validators.min(0)]],
//       isDozen: [false]  
//     });

//     this.loadStocks();
//   }

//   loadStocks() {
//     this.stockService.getAllStocks().subscribe({
//       next: data => this.stocks = data,
//       error: err => this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to load stocks' })
//     });
//   }

//   showCreateDialog() {
//     this.isEditMode = false;
//     this.stockForm.reset({ stockType: StockType.UnKnown, quantity: 0, isDozen: false });
//     this.dialogVisible = true;
//   }

//   showEditDialog(stock: Stock) {
//     this.isEditMode = true;
//     this.selectedStockId = stock.id;
//     this.stockForm.patchValue(stock);
//     this.dialogVisible = true;
//   }

//   saveStock() {
//     if (this.stockForm.invalid) {
//       this.stockForm.markAllAsTouched();
//       return;
//     }

//     const formValue = this.stockForm.value as Stock;

//     if (this.isEditMode && this.selectedStockId != null) {
//       this.stockService.updateStock(this.selectedStockId, formValue).subscribe({
//         next: (res) => {
//           console.log('updated data',res)
//           this.messageService.add({ severity: 'success', summary: 'Updated', detail: 'Stock updated successfully' });
//           this.dialogVisible = false;
//           this.loadStocks();
//         },
//         error: () => {
//           this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to update stock' });
//         }
//       });
//     } else {
//       this.stockService.createStock(formValue).subscribe({
//         next: (res) => {
//           console.log('created data',res);
//           this.messageService.add({ severity: 'success', summary: 'Created', detail: 'Stock created successfully' });
//           this.dialogVisible = false;
//           this.loadStocks();
//         },
//         error: () => {
//           this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to create stock' });
//         }
//       });
//     }
//   }

//   confirmDelete(stockId: number) {
//     if (confirm('Are you sure you want to delete this stock?')) {
//       this.stockService.deleteStock(stockId).subscribe({
//         next: () => {
//           this.messageService.add({ severity: 'success', summary: 'Deleted', detail: 'Stock deleted successfully' });
//           this.loadStocks();
//         },
//         error: () => {
//           this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to delete stock' });
//         }
//       });
//     }
//   }

//   hideDialog() {
//     this.dialogVisible = false;
//   }

// }




import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
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
    { label: 'Asset', value: StockType.Asset },
    { label: 'Consumable', value: StockType.Consumable }
  ];

  constructor(
    private fb: FormBuilder,
    private stockService: StockApiService,
    private messageService: MessageService
  ) {
    this.stockForm = this.fb.group({
      name: ['', Validators.required],
      description: [''],
      stockType: [null, Validators.required],
      quantity: [0, [Validators.required, Validators.min(0)]],
      isDozen: [false]
    });

    this.loadStocks();
  }

  loadStocks() {
    this.stockService.getAllStocks().subscribe({
      next: (data) => (this.stocks = data),
      error: () => this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to load stocks' })
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
    this.dialogVisible = true;
  }

  showEditDialog(stock: Stock) {
    this.isEditMode = true;
    this.selectedStockId = stock.id;
    this.stockForm.patchValue(stock);
    this.dialogVisible = true;
  }

  saveStock() {
    if (this.stockForm.invalid) {
      this.stockForm.markAllAsTouched();
      return;
    }

    const formValue: Stock = this.stockForm.value;

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

