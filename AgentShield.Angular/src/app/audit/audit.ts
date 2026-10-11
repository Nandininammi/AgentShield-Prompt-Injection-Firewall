import {Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { DatePipe } from '@angular/common';
import {
  AuditService,
  SecurityAuditLog
} from '../services/audit.service';

@Component({
  selector: 'app-audit',
  standalone: true,
  imports: [DatePipe],
  templateUrl: './audit.html',
  styleUrl: './audit.css'
})
export class Audit implements OnInit {

  logs: SecurityAuditLog[] = [];
  loading = true;
  errorMessage = '';

  constructor(private auditService: AuditService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.loadAuditLogs();
    
  } 

loadAuditLogs(): void {
  this.loading = true;
  this.errorMessage = '';

  this.auditService.getAuditLogs().subscribe({
    next: (data) => {
      this.logs = data;
      this.loading = false;

      console.log('Logs received:', this.logs.length);
      console.log('Loading:', this.loading);

      this.cdr.detectChanges();
    },
    error: (error) => {
      console.error('Audit API error:', error);
      this.errorMessage = 'Unable to load audit logs.';
      this.loading = false;

      this.cdr.detectChanges();
    }
  });
}


}