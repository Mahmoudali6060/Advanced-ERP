import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './components/dashboard.component';
import { AuthGuardService } from '../../shared/guards/auth-guard.service';

const routes: Routes = [
  { path: '', component: DashboardComponent },
  {path :'**', component: DashboardComponent},
  { path: 'dashboard', component: DashboardComponent }

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardRoutingModule {
}
