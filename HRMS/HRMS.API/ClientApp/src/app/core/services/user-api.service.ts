import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserDetailsResponseDTO } from '../models/user-details-response.dto';
import { AssignUserRole } from '../models/assign-user-role';
import { IUserDTO } from '../models/user-response-Dto';
import { IUserEditDTO } from '../models/usereditDto';

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

 
  deleteUser(id: string): Observable<number> {
    return this.http.delete<number>(`${this.baseUrl}/Delete/${id}`);
  }
 
  // assignRoles(data:AssignUserRole): Observable<number> {
  //   return this.http.post<number>(`${this.baseUrl}/AssignRoles`, data);
  // }


  assignRoles(data: AssignUserRole): Observable<any> {
    return this.http.post('https://localhost:44372/api/User/AssignRoles', data);
  }
  // this will use by user only to update  fullname and email
  editUserProfile(id: string, data:IUserEditDTO ): Observable<number> {
    return this.http.put<number>(`${this.baseUrl}/EditUserProfile/${id}`, data);
  }
 
  updateUserRole(data: AssignUserRole): Observable<number> {
    return this.http.put<number>(`${this.baseUrl}/EditUserRole`, data);
  }
  

}
