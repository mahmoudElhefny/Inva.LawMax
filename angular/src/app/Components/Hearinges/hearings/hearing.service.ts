import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Hearing, HearingCreate } from './hearing';
import { GetPaginationTypeInput } from '../../Cases/case-list/case-list/GetCaseTypeInput';

@Injectable({
  providedIn: 'root'
})
export class HearingService {
  private apiUrl = 'https://localhost:44330/api/HearingController/Hearing';
  constructor(private http: HttpClient) { }
  getHearings(paginationInput:GetPaginationTypeInput): Observable<any> {
    let params = new HttpParams()
      .set('SkipCount', paginationInput.skipCount.toString())
      .set('MaxResultCount', paginationInput.maxResultCount.toString());
    return this.http.get(this.apiUrl,{params});
  }

  createHearing(hearing: HearingCreate): Observable<Hearing> {
    return this.http.post<Hearing>(this.apiUrl, hearing);
  }

  updateHearing(hearing: HearingCreate): Observable<Hearing> {
    return this.http.put<Hearing>(`${this.apiUrl}/${hearing.id}`, hearing);
  }

  deleteHearing(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
