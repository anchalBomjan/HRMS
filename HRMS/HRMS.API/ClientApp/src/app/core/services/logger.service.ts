
import { Injectable } from '@angular/core';
import { MessageService } from 'primeng/api';

@Injectable({
  providedIn: 'root'
})
export class LoggerService {
  constructor(private messageService: MessageService) {}

  // Success message
  success(message: string, title: string = 'Success'): void {
    this.messageService.add({
      severity: 'success',
      summary: title,
      detail: message,
      life: 3000
    });
  }

  // Error message
  error(message: string, title: string = 'Error'): void {
    this.messageService.add({
      severity: 'error',
      summary: title,
      detail: message,
      life: 5000  // Longer display for errors
    });
  }

  // Warning message
  warn(message: string, title: string = 'Warning'): void {
    this.messageService.add({
      severity: 'warn',
      summary: title,
      detail: message
    });
  }

  // Information message
  info(message: string, title: string = 'Info'): void {
    this.messageService.add({
      severity: 'info',
      summary: title,
      detail: message
    });
  }
}
