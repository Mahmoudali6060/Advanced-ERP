import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { SharedModule } from '../../shared.module';
import { ProductService } from 'src/app/modules/setup/services/product.service';
import { ClientService } from 'src/app/modules/setup/services/client.service';
import { VendorService } from 'src/app/modules/setup/services/vendor.service';
import { ProductFormPopupComponent } from './components/product-form-popup/product-form-popup.component';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  imports: [
    SharedModule,
  ],
  exports: [
    ProductFormPopupComponent
  ],
  declarations: [
    ProductFormPopupComponent
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  providers: [
    ProductService,
    ClientService,
    VendorService,
    NgbActiveModal
  ]
})
export class SetupSharedModule {
}
