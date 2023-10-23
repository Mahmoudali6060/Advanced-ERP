import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { AuthModule } from '../authentication/auth.module';
import { InputSwitchModule } from 'primeng/inputswitch';
import { ConfigurationsModule } from '../configurations/configurations.module';
import { CountryService } from '../configurations/services/country.service';
import { PurchasesBillRoutingModule } from './purchases-routing.module';
import { PurchasesBillListComponent } from './components/purchases-bill/purchases-bill-list/purchases-bill-list.component';
import { PurchasesBillFormComponent } from './components/purchases-bill/purchases-bill-form/purchases-bill-form.component';
import { ProductService } from '../setup/services/product.service';
import { CategoryService } from '../setup/services/category.service';
import { ClientVendorService } from '../setup/services/client-vendor.service';
import { PurchasesBillService } from './services/purchases-bill.service';

@NgModule({
  imports: [
    PurchasesBillRoutingModule,
    SharedModule,
    ConfigurationsModule,
    AuthModule,
    InputSwitchModule
  ],
  exports: [

  ],
  declarations: [
    PurchasesBillListComponent,
    PurchasesBillFormComponent

  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  providers: [
    ProductService,
    CategoryService,
    ClientVendorService,
    PurchasesBillService
  ]
})
export class PurchasesBillModule {
}
