import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Employee } from '../models/Employee';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EmployeeApiServiceService {
  private baseUrl = `${environment.apiUrl}/Employee`;

  constructor(private http:HttpClient){}

  GetAllEmployee():Observable<Employee[]>{
    return this.http.get<Employee[]>(`${this.baseUrl}/GetAll`);

  }

  CreateEmployee(employeeData:any):Observable<any>{
    return this.http.post(`${this.baseUrl}/Create Employee`,employeeData);

  }

  getEmployeeById(id:number):Observable<Employee>{
    return this.http.get<Employee>(`${this.baseUrl}/${id}`);
  
  }


  // updateEmployee(id:number,employeeData:any):Observable<any>{
  //   return this.http.put(`${this.baseUrl}/${id}`,employeeData);

  //  }
// if we are using plain text string  retun type in backend  command handler then we  have  says like this 
  // updateEmployee(id: number, employeeData: any): Observable<string> {
  //   return this.http.put(`${this.baseUrl}/${id}`, employeeData, { responseType: 'text' });
  // }
  // updateEmployee(id: number, employeeData: any): Observable<string> {
  //   return this.http.put(`${this.baseUrl}/${id}`, employeeData, { responseType: 'text' });
  // }
  

  updateEmployee(id: number, employeeData: any): Observable<{ message: string }> {
    return this.http.put<{ message: string }>(`${this.baseUrl}/${id}`, employeeData);
  }
  
 
  
  
  // deleteEmployee(id:number):Observable<{message:string}>{
  //   return  this.http.delete<{message:string}>(`${this.baseUrl}/${id}`);
  // }
  deleteEmployee(id: number): Observable<string> {
    return this.http.delete(`${this.baseUrl}/${id}`, { responseType: 'text' });
  }
  
}
