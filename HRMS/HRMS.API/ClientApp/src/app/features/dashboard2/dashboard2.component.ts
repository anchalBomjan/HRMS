import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Chart, registerables } from 'chart.js';
import { StockApiService } from 'src/app/core/services/stock-api.service';

@Component({
  selector: 'app-dashboard2',
  templateUrl: './dashboard2.component.html',
  styleUrls: ['./dashboard2.component.scss']
})
export class Dashboard2Component  implements OnInit {
  @ViewChild('pieCanvas') pieCanvas!: ElementRef;
  @ViewChild('barCanvas') barCanvas!: ElementRef;

  constructor(private stockService: StockApiService) {
    Chart.register(...registerables);  // Register Chart.js components
  }

  ngOnInit(): void {
    this.stockService.getAllStocks().subscribe(data => {
      const labels = data.map(s => s.name);
      const quantities = data.map(s => s.quantity);
  
      new Chart(this.pieCanvas.nativeElement, {
        type: 'pie',
        data: {
          labels,
          datasets: [{
            label: 'Stock Quantity (Pie)',
            data: quantities,
            backgroundColor: ['#FF6384', '#36A2EB', '#FFCE56', '#4BC0C0', '#9966FF']
          }]
        },
        options: {
          responsive: true,
          plugins: {
            legend: {
              position: 'bottom'
            }
          }
        }
      });
    });
  
    this.stockService.getStockSummary().subscribe(summary => {
      const labels = summary.map(s => s.stockType);
      const totalQty = summary.map(s => s.totalQuantity);
      const usedQty = summary.map(s => s.usedQuantity);
  
      new Chart(this.barCanvas.nativeElement, {
        type: 'bar',
        data: {
          labels,
          datasets: [
            {
              label: 'Total Quantity',
              data: totalQty,
              backgroundColor: '#36A2EB'
            },
            {
              label: 'Used Quantity',
              data: usedQty,
              backgroundColor: '#FF6384'
            }
          ]
        },
        options: {
          responsive: true,
          scales: {
            y: {
              beginAtZero: true
            }
          },
          plugins: {
            legend: {
              display: true,
              position: 'top'
            }
          }
        }
      });
    });
  }
  
  
}
