import { Routes } from '@angular/router';
import { Dashboard } from './dashboard/dashboard';
import { Scan } from './scan/scan';
import { Audit } from './audit/audit';

export const routes: Routes = [
  {
    path: '',
    component: Dashboard
  },
  {
    path: 'scan',
    component: Scan
  },
  {
  path: 'audit',
  component: Audit
}
];
