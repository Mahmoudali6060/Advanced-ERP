import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './components/dashboard.component';
import { AuthGuardService } from '../../shared/guards/auth-guard.service';
import { AuthGuard } from '../authentication/services/auth.guard';
import { Privileges } from 'src/app/shared/enums/privileges.enum';

const routes: Routes = [
  { path: '', component: DashboardComponent },
  {path :'**', component: DashboardComponent},
  { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard], data: { privilegeId: Privileges.Dashboard.DashboardPage.View }  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardRoutingModule {
}
