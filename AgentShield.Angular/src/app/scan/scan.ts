import { Component, ChangeDetectorRef } from '@angular/core';
import { DecimalPipe } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { finalize } from 'rxjs';

import {
  ScanService,
  ScanResult
} from '../services/scan';

@Component({
  selector: 'app-scan',
  standalone: true,
  imports: [FormsModule, DecimalPipe],
  templateUrl: './scan.html',
  styleUrl: './scan.css'
})
export class Scan {

  content = '';

  scanning = false;
  scanned = false;

  result: ScanResult = {
    threatDetected: false,
    attackTypes: [],
    severity: '',
    confidence: 0,
    action: '',
    explanation: ''
  };

  errorMessage = '';

  constructor(
    private scanService: ScanService,
    private cdr: ChangeDetectorRef
  ) {}

  scan(): void {

    console.log('SCAN BUTTON CLICKED');

    if (!this.content.trim()) {
      return;
    }

    this.scanning = true;
    this.scanned = false;
    this.errorMessage = '';

    this.cdr.detectChanges();

    this.scanService
      .scanContent(this.content)
      .pipe(
        finalize(() => {
          this.scanning = false;

          console.log('SCAN COMPLETED');

          this.cdr.detectChanges();
        })
      )
      .subscribe({

        next: (result) => {

          console.log('Scan result:', result);

          this.result = result;
          this.scanned = true;

          this.cdr.detectChanges();
        },

        error: (error) => {

          console.error('Scan API error:', error);

          this.errorMessage =
            'Unable to scan the content. Please make sure the AgentShield API is running.';

          this.scanned = false;

          this.cdr.detectChanges();
        }

      });
  }

  clear(): void {

    this.content = '';
    this.scanned = false;
    this.scanning = false;
    this.errorMessage = '';

    this.result = {
      threatDetected: false,
      attackTypes: [],
      severity: '',
      confidence: 0,
      action: '',
      explanation: ''
    };

    this.cdr.detectChanges();
  }
}