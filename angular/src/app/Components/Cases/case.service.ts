import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Case, CaseCreate } from './case';
import { GetTenantsInput } from '@abp/ng.tenant-management/proxy';

@Injectable({
  providedIn: 'root'
})
export class CaseService {

  private readonly API_URL = 'https://localhost:44330/api/Categry';

  constructor(private http: HttpClient) { }

  getCase(id:string):Observable<any>{
    return this.http.get(`${this.API_URL}/Categry/${id}`);
  }

  getCases(paginationInput:GetTenantsInput): Observable<any> {
    let params = new HttpParams()
      .set('SkipCount', paginationInput.skipCount.toString())
      .set('MaxResultCount', paginationInput.maxResultCount.toString());
    return this.http.get(`${this.API_URL}/Categry`, { params });
  }

  countCases(): Observable<number> {
    return this.http.get<number>(`${this.API_URL}/count`);
  }

  createCase(caseData: CaseCreate): Observable<Case> {
    return this.http.post<Case>(`${this.API_URL}/Categry`, caseData);
  }
  updateCase(caseData: CaseCreate): Observable<Case> {
    return this.http.put<Case>(`${this.API_URL}/${caseData.id}`, caseData);
  }

  deleteCase(id: string): Observable<void> {
    return this.http.delete<void>(`${this.API_URL}/Categry/${id}`);
  }
  getAllCases():Observable<any>{
    return this.http.get(`${this.API_URL}/Categry/GetAllCases`);
  }
}
