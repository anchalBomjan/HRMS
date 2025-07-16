import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccessDeniedComponent } from './shared/components/access-denied/access-denied.component';
import { authGuard } from './core/guards/auth.guard';
import { roleGuard } from './core/guards/role.guard';

const routes: Routes = [
  { path: '', redirectTo: 'auth/login', pathMatch: 'full' },

  {
    path: 'auth',
    loadChildren: () =>
      import('./features/auth/auth.module').then((m) => m.AuthModule)
  },
  // {
  //   path: 'hr',
  //   loadChildren: () => import('./features/hr/hr.module').then(m => m.HrModule),
  //   canActivate: [authGuard],
  //   data: { expectedRole: 'HR' }
  // },
  {
    path: 'home',
    loadChildren: () => import('./features/home/home.module').then(m => m.HomeModule),
    canActivate: [authGuard],
    data: { expectedRole: 'HR' }
  },

  {
    path: 'access-denied',
    component: AccessDeniedComponent
  },
  // {
  //   path: '**',
  //   redirectTo: 'auth/login'

  // }
];

@NgModule({
  //imports: [RouterModule.forRoot(routes)],
     imports:  [
    RouterModule.forRoot(routes, {
      enableTracing: true //  This will log all router events to the console
    })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {}
