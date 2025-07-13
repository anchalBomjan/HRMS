

import { Component } from '@angular/core';
import { IStockAssignmentViewModel } from 'src/app/core/models/stockassignmentViewModel';
import { AssignstockapiService } from 'src/app/core/services/assignstockapi.service';

@Component({
  selector: 'app-assignstockgetbyuseridlist',
  templateUrl: './assignstockgetbyuseridlist.component.html',
  styleUrls: ['./assignstockgetbyuseridlist.component.scss']
})
export class AssignstockgetbyuseridlistComponent {
  employeeId: number | null = null;
  assignedStocks: IStockAssignmentViewModel[] = [];
  searched = false;

  constructor(private assignStockService: AssignstockapiService) {}

  fetchAssignedStocks(): void {
    if (!this.employeeId) return;

    this.assignStockService.getStocksByEmployeeId(this.employeeId).subscribe({
      next: (data) => {
        this.assignedStocks = data;
        this.searched = true;
      },
      error: (err) => {
        console.error('Error fetching stocks:', err);
        this.assignedStocks = [];
        this.searched = true;
      }
    });
  }
}
