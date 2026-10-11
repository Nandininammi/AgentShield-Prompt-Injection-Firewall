
import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { DatePipe } from '@angular/common';

import {
  DashboardService,
  DashboardSummary,
  AttackType
} from '../services/dashboard';

import {
  AuditService,
  SecurityAuditLog
} from '../services/audit.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [DatePipe],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css'
})
export class Dashboard implements OnInit {

  summary: DashboardSummary = {
    totalScans: 0,
    threatsBlocked: 0,
    requestsAllowed: 0,
    requestsForReview: 0
  };

  attackTypes: AttackType[] = [];
  auditLogs: SecurityAuditLog[] = [];

  loading = true;
  errorMessage = '';

  constructor(
    private dashboardService: DashboardService,
    private auditService: AuditService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    console.log('AGENTSHIELD DASHBOARD LOADED');
    this.loadDashboard();
    this.loadRecentEvents();
  }

  loadRecentEvents(): void {
  this.auditService.getAuditLogs().subscribe({
    next: (data) => {
      console.log('RECENT SECURITY EVENTS:', data);
      this.auditLogs = data.slice(0, 5);
      this.cdr.detectChanges();
    },
    error: (error) => {
      console.error('AUDIT API ERROR:', error);
    }
  });
}

  loadDashboard(): void {

    this.loading = true;
    this.errorMessage = '';

    console.log('Calling Dashboard Summary API...');

    this.dashboardService.getSummary().subscribe({

      next: (data) => {

        console.log('SUMMARY API RESPONSE:', data);

        this.summary = data;

        // Stop loading as soon as the summary is available
        this.loading = false;
        this.cdr.detectChanges();
      },

      error: (error) => {

        console.error('SUMMARY API ERROR:', error);

        this.errorMessage =
          'Unable to load dashboard summary.';

        this.loading = false;
      }
    });


    console.log('Calling Attack Types API...');

    this.dashboardService.getAttackTypes().subscribe({

      next: (data) => {

        console.log('ATTACK TYPES API RESPONSE:', data);

        this.attackTypes = data;
        this.cdr.detectChanges();
      },

      error: (error) => {

        console.error('ATTACK TYPES API ERROR:', error);

        // Don't hide the summary just because attack statistics failed
        if (!this.errorMessage) {
          this.errorMessage =
            'Unable to load attack statistics.';
        }
      }
    });
  }
}