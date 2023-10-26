import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../modules/authentication/services/auth.guard';
import { FullLayoutComponent } from './components/full-layout/full-layout.component';

export const routes: Routes = [
  {
    path: '',
    component: FullLayoutComponent,
    children: [
      { path: 'dashboard', loadChildren: () => import('../modules/dashboard/dashboard.module').then(m => m.DashboardModule) },
      { path: 'user', canActivate: [AuthGuard], loadChildren: () => import('../modules/user-management/user-management.module').then(m => m.UserManagementModule) },
      { path: 'configurations', canActivate: [AuthGuard], loadChildren: () => import('../modules/configurations/configurations.module').then(m => m.ConfigurationsModule) },
      { path: 'setup', canActivate: [AuthGuard], loadChildren: () => import('../modules/setup/setup.module').then(m => m.SetupModule) },
      { path: 'purchases-bill', canActivate: [AuthGuard], loadChildren: () => import('../modules/purchases/purchases.module').then(m => m.PurchasesBillModule) },
      { path: 'sales-bill', canActivate: [AuthGuard], loadChildren: () => import('../modules/sales/sales.module').then(m => m.SalesModule) },

    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LayoutRoutingModule { }
