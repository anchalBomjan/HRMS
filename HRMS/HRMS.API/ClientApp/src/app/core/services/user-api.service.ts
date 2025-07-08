import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserDetailsResponseDTO } from '../models/user-details-response.dto';
import { AssignUserRole } from '../models/assign-user-role';
import { IUserDTO } from '../models/user-response-Dto';

@Injectable({
  providedIn: 'root'
})
export class UserApiService {


  private baseUrl=`${environment.apiUrl}/User`;

  constructor(private http:HttpClient) { }

  getAllUsers():Observable<IUserDTO[]>{
    return this.http.get<IUserDTO[]>(`${this.baseUrl}/GetAll`)
  }

  getAllUserDetails():Observable<UserDetailsResponseDTO[]>{
    return this.http.get<UserDetailsResponseDTO[]>(`${this.baseUrl}/GetAllUserDetails`)
  }

  deleteUser(id:string):Observable<number>{
    return this.http.delete<number>(`$t{htis.baseUrl}/Delete/${id}`);
  }

  assignRoles(data:AssignUserRole): Observable<number> {
    return this.http.post<number>(`${this.baseUrl}/AssignRoles`, data);
  }


}
