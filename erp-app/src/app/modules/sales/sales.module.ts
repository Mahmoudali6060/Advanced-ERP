import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { AuthModule } from '../authentication/auth.module';
import { InputSwitchModule } from 'primeng/inputswitch';
import { ConfigurationsModule } from '../configurations/configurations.module';
import { CountryService } from '../configurations/services/country.service';
import { SalesRoutingModule } from './sales-routing.module';
import { SalesBillListComponent } from './components/sales-bill/sales-bill-list/sales-bill-list.component';
import { SalesBillFormComponent } from './components/sales-bill/sales-bill-form/sales-bill-form.component';
import { ProductService } from '../setup/services/product.service';
import { CategoryService } from '../setup/services/category.service';
import { ClientVendorService } from '../setup/services/client-vendor.service';
import { SalesBillService } from './services/sales-bill.service';
import { SetupSharedModule } from 'src/app/shared/modules/setup-shared/setup-shared.module';

@NgModule({
  imports: [
    SalesRoutingModule,
    SharedModule,
    ConfigurationsModule,
    AuthModule,
    InputSwitchModule,
    SetupSharedModule
  ],
  exports: [

  ],
  declarations: [
    SalesBillListComponent,
    SalesBillFormComponent

  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  providers: [
    ProductService,
    CategoryService,
    ClientVendorService,
    SalesBillService
  ]
})
export class SalesModule {
}
