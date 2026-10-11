import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface ScanRequest {
  content: string;
}

export interface ScanResult {
  threatDetected: boolean;
  attackTypes: string[];
  severity: string;
  confidence: number;
  action: string;
  explanation: string;
}

@Injectable({
  providedIn: 'root'
})
export class ScanService {

  private apiUrl = 'https://localhost:7088/api/Scan';

  constructor(private http: HttpClient) {}

  scanContent(content: string): Observable<ScanResult> {

    const request: ScanRequest = {
      content: content
    };

    return this.http.post<ScanResult>(
      this.apiUrl,
      request
    );
  }
}