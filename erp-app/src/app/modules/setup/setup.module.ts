import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { ProductListComponent } from './components/product/product-list/product-list.component';
import { ProductFormComponent } from './components/product/product-form/product-form.component';
import { ProductService } from './services/product.service';
import { AuthModule } from '../authentication/auth.module';
import { InputSwitchModule } from 'primeng/inputswitch';
import { CompanyService } from './services/company.service';
import { ConfigurationsModule } from '../configurations/configurations.module';
import { CountryService } from '../configurations/services/country.service';
import { SetupRoutingModule } from './setup-routing.module';
import { CategoryListComponent } from './components/category/category-list/category-list.component';
import { CategoryFormComponent } from './components/category/category-form/category-form.component';
import { CategoryService } from './services/category.service';
import { ClientFormComponent } from './components/client/client-form/client-form.component';
import { ClientListComponent } from './components/client/client-list/client-list.component';
import { VendorFormComponent } from './components/vendor/vendor-form/vendor-form.component';
import { VendorListComponent } from './components/vendor/vendor-list/vendor-list.component';
import { ClientVendorService } from './services/client-vendor.service';
import { CompanyFormComponent } from './components/company/company-form/company-form.component';
import { CompanyListComponent } from './components/company/company-list/company-list.component';
import { ProductListViewComponent } from './components/product/product-list-view/product-list-view.component';
import { ProductListChangePriceComponent } from './components/product/product-list-change-price/product-list-change-price.component';
import { ProductListChangeQuantityComponent } from './components/product/product-list-change-quantity/product-list-change-quantity.component';

@NgModule({
  imports: [
    SetupRoutingModule,
    SharedModule,
    ConfigurationsModule,
    AuthModule,
    InputSwitchModule
  ],
  exports: [

  ],
  declarations: [
    ProductListComponent,
    ProductListViewComponent,
    ProductListChangePriceComponent,
    ProductListChangeQuantityComponent,
    ProductFormComponent,
    CategoryListComponent,
    CategoryFormComponent,
    ClientFormComponent,
    ClientListComponent,
    VendorFormComponent,
    VendorListComponent,
    CompanyListComponent,
    CompanyFormComponent
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  providers: [
    ProductService,
    CountryService,
    CompanyService,
    CategoryService,
    ClientVendorService
  ]
})
export class SetupModule {
}
