import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { AuthModule } from '../authentication/auth.module';
import { InputSwitchModule } from 'primeng/inputswitch';
import { ConfigurationsModule } from '../configurations/configurations.module';
import { TreasuryService } from './services/treasury.service';
import { AccountingRoutingModule } from './accounting-routing.module';
import { TreasuryFormComponent } from './components/treasury/treasury-form/treasury-form.component';
import { TreasuryListComponent } from './components/treasury/treasury-list/treasury-list.component';
import { ClientVendorService } from '../setup/services/client-vendor.service';
import { ReportService } from '../report/services/report.service';

@NgModule({
  imports: [
    AccountingRoutingModule,
    SharedModule,
    ConfigurationsModule,
    AuthModule,
    InputSwitchModule
  ],

  exports: [

  ],

  declarations: [
    TreasuryListComponent,
    TreasuryFormComponent
  ],

  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  providers: [
    TreasuryService,
    ClientVendorService,
    ReportService
  ]
})
export class AccountingModule {
}
