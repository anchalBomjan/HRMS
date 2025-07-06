import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { Observable } from 'rxjs';
import { Stock } from '../models/stock';

@Injectable({
  providedIn: 'root'
})
export class StockApiService {


  private baseUrl = `${environment.apiUrl}/Stock`;

  constructor(private http: HttpClient) {}

  getAllStocks(): Observable<Stock[]> {
    return this.http.get<Stock[]>(`${this.baseUrl}`);
  }

  getStockById(id: number): Observable<Stock> {
    return this.http.get<Stock>(`${this.baseUrl}/${id}`);
  }

  createStock(stock: Stock): Observable<number> {
    return this.http.post<number>(`${this.baseUrl}`, stock);
  }

  updateStock(id: number, stock: Stock): Observable<string> {
    return this.http.put<string>(`${this.baseUrl}/${stock.id}`, stock);
  }
  // updateStock(id: number, stock: Stock): Observable<string> {
  //   // FIX: Use route parameter instead of stock.id
  //   return this.http.put<string>(`${this.baseUrl}/${id}`, stock);
  // }

  deleteStock(id: number): Observable<string> {
    return this.http.delete(`${this.baseUrl}/${id}`, { responseType: 'text' as 'text' });
  }
  
}
