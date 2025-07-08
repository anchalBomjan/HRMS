import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CreateRole } from '../models/role-create';
import { RoleDTO } from '../models/role';

@Injectable({
  providedIn: 'root'
})
export class RoleApiService {

  private baseUrl=`${environment.apiUrl}/Role`;
  constructor(private http:HttpClient) 
  {}

  createRole(data:CreateRole):Observable<number>{
    return this.http.post<number>(`${this.baseUrl}/create`,data);
  }

  getAllRoles():Observable<RoleDTO[]>{
    return this.http.get<RoleDTO[]>(`${this.baseUrl}/GetAll`);

  }

  getRoleById(id:string):Observable<RoleDTO>{


    return this.http.get<RoleDTO>(`${this.baseUrl}/${id}`);
  }

  deleteRole(id:string):Observable<number>{
    return this.http.delete<number>(`${this.baseUrl}/Delete/${id}`);

  
  }

  editRole(id:string ,data:RoleDTO):Observable<number>{
    return this.http.put<number>(`${this.baseUrl}/Edit/${id}`,data)
  }
}
