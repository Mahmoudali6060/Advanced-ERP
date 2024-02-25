import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { DashboardComponent } from './components/dashboard.component';
import { SharedModule } from '../../shared/shared.module';
import { AuthGuardService } from 'src/app/shared/guards/auth-guard.service';
import {ChartModule} from 'primeng/chart';
import { DashboardService } from './services/dashboard.service';

@NgModule({
  imports: [
    DashboardRoutingModule,
    SharedModule,
    ChartModule
  ],
  declarations: [
    DashboardComponent
  ],
  providers: [
    DashboardService
  ],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA
  ],
})

export class DashboardModule {
}
