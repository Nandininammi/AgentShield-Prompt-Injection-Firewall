
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface SecurityAuditLog {
  id: string;
  timestamp: string;
  content: string;
  threatDetected: boolean;
  attackTypes: string[];
  severity: string;
  confidence: number;
  action: string;
  detectionMethod: string;
  explanation: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuditService {

  private apiUrl = 'https://localhost:7088/api/Audit';

  constructor(private http: HttpClient) {}

  getAuditLogs(): Observable<SecurityAuditLog[]> {
    return this.http.get<SecurityAuditLog[]>(this.apiUrl);
  }
}
