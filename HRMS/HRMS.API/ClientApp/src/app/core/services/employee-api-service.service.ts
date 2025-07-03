import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Employee } from '../models/Employee';

@Injectable({
  providedIn: 'root'
})
export class EmployeeApiServiceService {


  private baseUrl='${environment.apiUrl}Employee';
  constructor(private http:HttpClient){}

  GetAllEmployee():Observable<Employee[]>{
    return this.http.get<Employee[]>(`${this.baseUrl}/GetAll`);

  }

  CreateEmployee(employeeData:any):Observable<any>{
    return this.http.post(`${this.baseUrl}/Create Employee`,employeeData);

  }

  getEmployeeById(id:number):Observable<Employee>{
    return this.http.get<Employee>(`${this.baseUrl}/${id}`);
     //return this.http.get<Employee>(`${this.baseUrl}/${id}`);
    // return this.http.put(`${this.baseUrl}/${id}`, employeeData);


  }


  updateEmployee(id:number,employeeData:any):Observable<any>{
    return this.http.put(`${this.baseUrl}/${id}`,employeeData);

  }
  
  





}
