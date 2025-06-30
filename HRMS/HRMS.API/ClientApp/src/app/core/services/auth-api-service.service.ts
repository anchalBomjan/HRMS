import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IForgotPasswordRequest } from '../models/IForgotPasswordRequest ';
import { IResetPasswordRequest } from '../models/IResetPasswordRequest ';
import { ILoginRequest } from '../models/ILoginRequest ';

@Injectable({
  providedIn: 'root'
})
export class AuthApiServiceService {

  private baseUrl = 'https://localhost:44386/api/Auth'; // adjust if needed

  constructor(private http: HttpClient) {}

  login(data: ILoginRequest): Observable<any> {
    return this.http.post(`${this.baseUrl}/Login`, data);
  }

  forgotPassword(data: IForgotPasswordRequest): Observable<any> {
    return this.http.post(`${this.baseUrl}/forget-Password`, data);
  }

  resetPassword(data: IResetPasswordRequest): Observable<any> {
    return this.http.post(`${this.baseUrl}/reset-password`, data);
  }





}
