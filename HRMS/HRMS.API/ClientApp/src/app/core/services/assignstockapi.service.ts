import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { HttpClient } from '@angular/common/http';
import { ICreateStockAssignment } from '../models/createstockassignment';
import { Observable } from 'rxjs';
import { IUpdateStockAssignment } from '../models/updatestockassignment';
import { IEmployeeAssignmentViewModel } from '../models/employeeassignmentViewModel';
import { IStockAssignmentViewModel } from '../models/stockassignmentViewModel';
import { IStockAssignmentDTO } from '../models/stock-assignment-dto.model';

@Injectable({
  providedIn: 'root'
})
export class AssignstockapiService {


  private baseUrl = `${environment.apiUrl}/StockAssignments`;

  constructor(private http:HttpClient) { }



  assignStock(data:ICreateStockAssignment):Observable<number>{
    
    return this.http.post<number>(this.baseUrl,data);
  }

  updateStockAssignment(id:number,data:IUpdateStockAssignment):Observable<number>{
    return this.http.put<number>(`${this.baseUrl}/${id}`,data)
  }

 deleteStockAssignment(id: number): Observable<number> {
  return this.http.delete<number>(`${this.baseUrl}/${id}`);
 }

 getStockAssignmentById(id: number): Observable<IStockAssignmentDTO> {
  return this.http.get<IStockAssignmentDTO>(`${this.baseUrl}/${id}`);
}

getEmployeesByStockId(stockId: number): Observable<IEmployeeAssignmentViewModel[]> {
  return this.http.get<IEmployeeAssignmentViewModel[]>(`${this.baseUrl}/stock/${stockId}/employees`);
}


getStocksByEmployeeId(employeeId: number): Observable<IStockAssignmentViewModel[]> {
  return this.http.get<IStockAssignmentViewModel[]>(`${this.baseUrl}/employee/${employeeId}/stocks`);
}

getAllStockAssignments(): Observable<IStockAssignmentDTO[]> {
  return this.http.get<IStockAssignmentDTO[]>(this.baseUrl);
}

}
