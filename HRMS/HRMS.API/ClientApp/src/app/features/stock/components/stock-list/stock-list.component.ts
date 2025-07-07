
// import { Component, ChangeDetectorRef } from '@angular/core';
// import { FormBuilder, FormGroup, Validators, AbstractControl, ValidationErrors } from '@angular/forms';
// import { StockApiService } from 'src/app/core/services/stock-api.service';
// import { Stock } from 'src/app/core/models/stock';
// import { StockType } from 'src/app/core/models/stock-type.enum';
// import { MessageService } from 'primeng/api';

import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { MessageService } from "primeng/api";
import { Stock } from "src/app/core/models/stock";
import { StockType } from "src/app/core/models/stock-type.enum";
import { StockApiService } from "src/app/core/services/stock-api.service";

// @Component({
//   selector: 'app-stock-list',
//   templateUrl: './stock-list.component.html',
//   styleUrls: ['./stock-list.component.scss'],
//   providers: [MessageService]
// })
// export class StockListComponent {
//   stocks: Stock[] = [];
//   stockForm: FormGroup;
//   dialogVisible = false;
//   isEditMode = false;
//   selectedStockId?: number;

//   stockTypes = [
//     {label:'Unknown',value:StockType.UnKnown},
//     { label: 'Asset', value: StockType.Asset },
//     { label: 'Consumable', value: StockType.Consumable }
//   ];

//   constructor(
//     private fb: FormBuilder,
//     private stockService: StockApiService,
//     private messageService: MessageService,
//     private cd: ChangeDetectorRef
//   ) {
//     this.stockForm = this.fb.group({
//       name: ['', Validators.required],
//       description: [''],
//       stockType: [null, [Validators.required, this.forbidUnknownValidator]],
//       quantity: [0, [Validators.required, Validators.min(0)]],
//       isDozen: [false],
//       isAdditive: [true] 

//     });

//     this.loadStocks();
//   }

//   forbidUnknownValidator(control: AbstractControl): ValidationErrors | null {
//     return control.value === StockType.UnKnown ? { unknownSelected: true } : null;
//   }

//   // loadStocks() {
//   //   this.stockService.getAllStocks().subscribe({
//   //     next: (data) => (this.stocks = data),
//   //     error: () =>
//   //       this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to load stocks' })
//   //   });
//   // }
//   loadStocks() {
//     this.stockService.getAllStocks().subscribe({
//       next: (data) => {
//         this.stocks = data.map(stock => ({
//           ...stock,
//           stockType: this.mapStockType(stock.stockType)
//         }));
//       },
//       error: () =>
//         this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to load stocks' })
//     });
//   }
//   mapStockType(type: any): StockType {
//     if (typeof type === 'string') {
//       switch (type.toLowerCase()) {
//         case 'asset': return StockType.Asset;
//         case 'consumable': return StockType.Consumable;
//         default: return StockType.UnKnown;
//       }
//     }
//     return type; // already numeric
//   }
//   getStockTypeLabel(type: StockType): string {
//     return this.stockTypes.find(t => t.value === type)?.label ?? 'Unknown';
//   }

//   showCreateDialog() {
//     this.isEditMode = false;
//     this.stockForm.reset({
//       name: '',
//       description: '',
//       stockType: null,
//       quantity: 0,
//       isDozen: false
//     });

//     setTimeout(() => {
//       this.dialogVisible = true;
//       this.cd.detectChanges();
//     }, 0);
//   }

//   showEditDialog(stock: Stock) {
//     this.isEditMode = true;
//     this.selectedStockId = stock.id;
  
//     const stockTypeValue = stock.stockType === StockType.UnKnown ? null : stock.stockType;
  
//     this.stockForm.patchValue({
//       ...stock,
//       stockType: stockTypeValue
//     });
  
//     console.log('Editing stock', stock);
//     console.log('Patched form:', this.stockForm.value);
  
//     setTimeout(() => {
//       this.dialogVisible = true;
//       this.cd.detectChanges();
//     }, 0);
//   }

//   // saveStock() {
//   //   if (this.stockForm.invalid) {
//   //     this.stockForm.markAllAsTouched();
//   //     return;
//   //   }

//   //   const formValue: Stock = this.stockForm.value;

//   //   console.log('create stock', formValue);
   
//   //   if (this.isEditMode && this.selectedStockId) {
//   //     this.stockService.updateStock(this.selectedStockId, formValue).subscribe({
//   //       next: () => {
//   //         this.messageService.add({ severity: 'success', summary: 'Updated', detail: 'Stock updated successfully' });
//   //         this.dialogVisible = false;
//   //         this.loadStocks();
//   //       },
//   //       error: () =>
//   //         this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to update stock' })
//   //     });
//   //   } else {
//   //     this.stockService.createStock(formValue).subscribe({
//   //       next: () => {
//   //         this.messageService.add({ severity: 'success', summary: 'Created', detail: 'Stock created successfully' });
//   //         this.dialogVisible = false;
//   //         this.loadStocks();
//   //       },
//   //       error: () =>
//   //         this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to create stock' })
//   //     });
//   //   }
//   // }
//   saveStock() {
//     if (this.stockForm.invalid) {
//       this.stockForm.markAllAsTouched();
//       return;
//     }
  
//     const formValue: Stock = this.stockForm.value;
  
//     if (this.isEditMode && this.selectedStockId) {
//       const updateData: Stock = { ...formValue, id: this.selectedStockId }; // ✅ Ensure ID is included
  
//       this.stockService.updateStock(this.selectedStockId, updateData).subscribe({
//         next: () => {
//           this.messageService.add({ severity: 'success', summary: 'Updated', detail: 'Stock updated successfully' });
//           this.dialogVisible = false;
//           this.loadStocks();
//         },
//         error: () =>
//           this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to update stock' })
//       });
//     } else {
//       this.stockService.createStock(formValue).subscribe({
//         next: () => {
//           this.messageService.add({ severity: 'success', summary: 'Created', detail: 'Stock created successfully' });
//           this.dialogVisible = false;
//           this.loadStocks();
//         },
//         error: () =>
//           this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to create stock' })
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
//         error: () =>
//           this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to delete stock' })
//       });
//     }
//   }

//   hideDialog() {
//     this.dialogVisible = false;
//   }
// }

// this below code is spliting
// import { Component } from '@angular/core';
// import { FormBuilder, FormGroup, Validators } from '@angular/forms';
// import { StockApiService } from 'src/app/core/services/stock-api.service';
// import { Stock } from 'src/app/core/models/stock';
// import { StockType } from 'src/app/core/models/stock-type.enum';
// import { MessageService } from 'primeng/api';

// @Component({
//   selector: 'app-stock-list',
//   templateUrl: './stock-list.component.html',
//   styleUrls: ['./stock-list.component.scss'],
//   providers: [MessageService]
// })
// export class StockListComponent {
//   stocks: Stock[] = [];
//   stockForm: FormGroup;
//   dialogVisible = false;
//   isEditMode = false;
//   selectedStockId?: number;

//   // Corrected stock types for dropdown
//   stockTypes = [
//     { label: 'Unknown', value: StockType.UnKnown },
//     { label: 'Asset', value: StockType.Asset },
//     { label: 'Consumable', value: StockType.Consumable }
//   ];
//   stockTypeLabels: { [key: number]: string } = {
//     0: 'Unknown',
//     1: 'Asset',
//     2: 'Consumable'
//   };
  
  

//   constructor(
//     private fb: FormBuilder,
//     private stockService: StockApiService,
//     private messageService: MessageService
//   ) {
//     this.stockForm = this.fb.group({
//       name: ['', Validators.required],
//       description: [''],
//      stockType: [null, Validators.required],

//       quantity: [0, [Validators.required, Validators.min(0)]],
//       isDozen: [false],
//       isAdditive: [true]
//     });

//     this.loadStocks();
//   }

//   loadStocks() {
//     this.stockService.getAllStocks().subscribe({
//       next: (data) => {
//         this.stocks = data.map(stock => ({
//           ...stock,
//           stockType: this.mapStockType(stock.stockType)
//         }));
//       },
//       error: () =>
//         this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to load stocks' })
//     });
//   }
//   mapStockType(type: any): StockType {
//     if (typeof type === 'string') {
//       switch (type.toLowerCase()) {
//         case 'asset': return StockType.Asset;
//         case 'consumable': return StockType.Consumable;
//         default: return StockType.UnKnown;
//       }
//     }
//     return type; // already numeric
//   }
//   getStockTypeLabel(type: StockType): string {
//     return this.stockTypes.find(t => t.value === type)?.label ?? 'Unknown';
//   }
//   showCreateDialog() {
//     this.isEditMode = false;
//     this.stockForm.reset({
//       stockType: null,
//       quantity: 0,
//       isDozen: false,
//       isAdditive: true
//     });
//     this.dialogVisible = true;
//   }

//   showEditDialog(stock: Stock) {
//     this.isEditMode = true;
//     this.selectedStockId = stock.id;
    
//     this.stockForm.patchValue({
//       ...stock,
//       // Ensure stock type is valid
//       stockType: stock.stockType === StockType.UnKnown ? null : stock.stockType
//     });
    
//     this.dialogVisible = true;
//   }

//   // saveStock() {
//   //   if (this.stockForm.invalid) {
//   //     this.stockForm.markAllAsTouched();
//   //     return;
//   //   }

//   //   const formValue: Stock = this.stockForm.value;
    
//   //   if (this.isEditMode && this.selectedStockId) {
//   //     // Ensure ID is included for edit operation
//   //     const updateData = { ...formValue, id: this.selectedStockId };
      
//   //     this.stockService.updateStock(this.selectedStockId, updateData).subscribe({
//   //       next: (message) => {
//   //         this.showSuccess('Stock updated successfully');
//   //         this.dialogVisible = false;
//   //         this.loadStocks(); // Refresh data
//   //       },
//   //       error: (err) => this.showError('Failed to update stock', err)
//   //     });
//   //   } else {
//   //     this.stockService.createStock(formValue).subscribe({
//   //       next: () => {
//   //         this.showSuccess('Stock created successfully');
//   //         this.dialogVisible = false;
//   //         this.loadStocks();
//   //       },
//   //       error: (err) => this.showError('Failed to create stock', err)
//   //     });
//   //   }
//   // }
//   saveStock() {
//     if (this.stockForm.invalid) {
//       this.stockForm.markAllAsTouched();
//       return;
//     }
  
//     const rawFormValue = this.stockForm.value;
  
//     // ✅ Map `stockType` form field to `type` for backend
//     const payload: Stock = {
//       ...rawFormValue,
//       type: rawFormValue.stockType,
//       stockType: undefined, // Optional: remove this if not needed
//       id: this.isEditMode ? this.selectedStockId : undefined
//     };
  
//     if (this.isEditMode && this.selectedStockId) {
//       this.stockService.updateStock(this.selectedStockId, payload).subscribe({
//         next: () => {
//           this.showSuccess('Stock updated successfully');
//           this.dialogVisible = false;
//           this.loadStocks();
//         },
//         error: (err) => this.showError('Failed to update stock', err)
//       });
//     } else {
//       this.stockService.createStock(payload).subscribe({
//         next: () => {
//           this.showSuccess('Stock created successfully');
//           this.dialogVisible = false;
//           this.loadStocks();
//         },
//         error: (err) => this.showError('Failed to create stock', err)
//       });
//     }
//   }
  
//   confirmDelete(stockId: number) {
//     if (confirm('Are you sure you want to delete this stock?')) {
//       this.stockService.deleteStock(stockId).subscribe({
//         next: (message) => {
//           this.showSuccess('Stock deleted successfully');
//           // Update UI without full reload
//           this.stocks = this.stocks.filter(s => s.id !== stockId);
//         },
//         error: (err) => this.showError('Failed to delete stock', err)
//       });
//     }
//   }

//   hideDialog() {
//     this.dialogVisible = false;
//   }

//   private showSuccess(message: string) {
//     this.messageService.add({
//       severity: 'success',
//       summary: 'Success',
//       detail: message
//     });
//   }

//   private showError(summary: string, err?: any) {
//     const detail = err?.error?.message || 'Please try again later';
//     this.messageService.add({
//       severity: 'error',
//       summary,
//       detail
//     });
//   }
// }
import { Component } from '@angular/core';

@Component({
  selector: 'app-stock-list',
  templateUrl: './stock-list.component.html',
  styleUrls: ['./stock-list.component.scss'],
  providers: [MessageService]
})
export class StockListComponent {
  stocks: Stock[] = [];
  stockForm: FormGroup;
  selectedStockId?: number;

  createDialogVisible = false;
  editDialogVisible = false;

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
      //stockType: [null, Validators.required],
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
        this.stocks = data.map(stock => ({
          ...stock,
          stockType: this.mapStockType(stock.stockType)
        }));
      },
      error: () =>
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to load stocks' })
    });
  }
  // mapStockType(type: any): StockType {
  //   if (typeof type === 'string') {
  //     switch (type.toLowerCase()) {
  //       case 'asset': return StockType.Asset;
  //       case 'consumable': return StockType.Consumable;
  //       default: return StockType.UnKnown;
  //     }
  //   }
  //   return type; // already numeric
  // }
  mapStockType(type: any): StockType {
    if (typeof type === 'string') {
      switch (type.toLowerCase()) {
        case 'asset': return StockType.Asset;
        case 'consumable': return StockType.Consumable;
        default: return StockType.UnKnown;
      }
    }
    return type; // if number, return as is
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

  // EDIT
 
  showEditDialog(stock: Stock) {
    this.selectedStockId = stock.id;
    
    // REMOVE null conversion logic
    this.stockForm.patchValue({
      name: stock.name,
      description: stock.description,
      stockType: this.mapStockType(stock.stockType), // Direct assignment
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
      stockType: formValue.stockType,   // This should be a number (enum)
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
  

  // DELETE

  confirmDelete(stockId: number) {
    if (confirm('Are you sure you want to delete this stock?')) {
      this.stockService.deleteStock(stockId).subscribe({
        next: () => {
          this.showSuccess('Stock deleted successfully');
          this.stocks = this.stocks.filter(s => s.id !== stockId);
        },
        error: () => this.showError('Failed to delete stock')
      });
    }
  }

  // TOAST helpers

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
