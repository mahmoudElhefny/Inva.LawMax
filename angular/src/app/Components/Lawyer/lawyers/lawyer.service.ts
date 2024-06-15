import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Lawyer, LawyerCreate } from './lawyer';
import { GetPaginationTypeInput } from '../../Cases/case-list/case-list/GetCaseTypeInput';

@Injectable({
  providedIn: 'root'
})
export class LawyerService {
  getAllLawyers(): Observable<any> {
    return this.http.get<Lawyer>(`${this.apiUrl}/GetAllLawyers`);
  }
  private apiUrl = 'https://localhost:44330/api/Lawyer';
  constructor(private http: HttpClient) { }

  getLawyers(_getpaginationTypeInput:GetPaginationTypeInput): Observable<any> {
    let params = new HttpParams()
    .set('SkipCount', _getpaginationTypeInput.skipCount.toString())
    .set('MaxResultCount', _getpaginationTypeInput.maxResultCount.toString());

  return this.http.get(`${this.apiUrl}`, { params });
  }

  createLawyer(lawyer: LawyerCreate): Observable<any> {
    return this.http.post<Lawyer>(`${this.apiUrl}/Create`, lawyer);
  }

  updateLawyer(lawyer: LawyerCreate): Observable<any> {
    return this.http.put<Lawyer>(`${this.apiUrl}/${lawyer.id}`, lawyer);
  }

  deleteLawyer(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
