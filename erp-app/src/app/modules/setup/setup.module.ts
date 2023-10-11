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
    ProductFormComponent,
    CategoryListComponent,
    CategoryFormComponent
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  providers: [
    ProductService,
    CountryService,
    CompanyService,
    CategoryService
  ]
})
export class SetupModule {
}
