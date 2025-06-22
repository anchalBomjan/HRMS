
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ForbiddenComponent } from './shared/components/forbidden/forbidden.component';
import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';
import { roleGuard } from './core/guards/role.guard';

const routes: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },

  {
    path: 'dashboard',
    loadChildren: () =>
      import('./features/dashboard/dashboard.module').then(m => m.DashboardModule),
    canActivate: [authGuard]
  },
  {
    path: 'user-management',
    loadChildren: () =>
      import('./features/user-management/user-management.module').then(m => m.UserManagementModule),
    canActivate: [authGuard, roleGuard],
    data: { roles: ['admin'] }
  },
  { path: 'forbidden', component: ForbiddenComponent },
  { path: '**', redirectTo: 'dashboard' }
];



@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ]
})
export class AppRoutingModule { }
