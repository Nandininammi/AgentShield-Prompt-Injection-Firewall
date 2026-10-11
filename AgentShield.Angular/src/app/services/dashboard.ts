import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface DashboardSummary {
  totalScans: number;
  threatsBlocked: number;
  requestsAllowed: number;
  requestsForReview: number;
}

export interface AttackType {
  attackType: string;
  count: number;
}

@Injectable({
  providedIn: 'root'
})
export class DashboardService {

  private apiUrl = 'https://localhost:7088/api/Dashboard';

  constructor(private http: HttpClient) {}

  getSummary(): Observable<DashboardSummary> {
    return this.http.get<DashboardSummary>(
      `${this.apiUrl}/summary`
    );
  }

  getAttackTypes(): Observable<AttackType[]> {
    return this.http.get<AttackType[]>(
      `${this.apiUrl}/attack-types`
    );
  }
}