import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';


const routes:Routes=[

  {
    path: 'auth',
    loadChildren: () =>
      import('./features/auth/auth.module').then((m) => m.AuthModule),
  },
  {
    path: '',
    loadChildren: () =>
      import('./features/layout/layout.module').then((m) => m.LayoutModule),
  },
  {
    path:'dashboard',
    loadChildren:()=>
     import('./features/dashboard/dashboard.module').then((m)=>m.DashboardModule)

  },

  {
    path: '**',               
    redirectTo: 'auth/login', 
    pathMatch: 'full'      
  },
]

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forRoot(routes)]
,
exports:[RouterModule]

})
export class AppRoutingModule { }
