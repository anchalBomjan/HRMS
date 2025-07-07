import { Component, EventEmitter, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { StockType } from 'src/app/core/models/stock-type.enum';
import { StockApiService } from 'src/app/core/services/stock-api.service';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-stock-create',
  templateUrl: './stock-create.component.html',
  styleUrls: ['./stock-create.component.scss'],
})
export class StockCreateComponent {
  stockForm: FormGroup;

  @Output() stockCreated = new EventEmitter<void>(); // notify parent on success
  @Output() cancelCreate = new EventEmitter<void>(); // notify parent on cancel

  stockTypes = [
    { label: 'Unknown', value: StockType.UnKnown },
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
      isDozen: [false],
      isAdditive: [true]
    });
  }

  onSubmit() {
    if (this.stockForm.invalid) {
      this.stockForm.markAllAsTouched();
      return;
    }

    const payload = this.stockForm.value;

    this.stockService.createStock(payload).subscribe({
      next: () => {
        this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Stock created successfully' });
        this.stockCreated.emit(); // Notify parent to reload & close dialog
        this.stockForm.reset({
          name: '',
          description: '',
          stockType: null,
          quantity: 0,
          isDozen: false,
          isAdditive: true
        });
      },
      error: () => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to create stock' });
      }
    });
  }

  onCancel() {
    this.cancelCreate.emit();
  }
}
