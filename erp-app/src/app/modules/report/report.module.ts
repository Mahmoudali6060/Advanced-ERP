import { NgModule } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { ReportService } from 'src/app/modules/report/services/report.service';
import { CommonModule } from '@angular/common';
import { AccountStatementAllClientsComponent } from './components/client/account-statement-all-clients/account-statement-all-clients.component';
import { ReportRoutingModule } from './report-routing.module';
import { ClientVendorService } from '../setup/services/client-vendor.service';
import { AccountStatementAllVendorsComponent } from './components/vendor/account-statement-all-vendors/account-statement-all-vendors.component';
import { AccountStatementSingleClientComponent } from './components/client/account-statement-single-client/account-statement-single-client.component';
import { SalesBillService } from '../sales/services/sales-bill.service';
import { AccountStatementSingleVendorComponent } from './components/vendor/account-statement-single-vendor/account-statement-single-vendor.component';
import { PurchasesBillService } from '../purchases/services/purchases-bill.service';
import { ProductListMinusReportComponent } from './components/product/product-list-minus-report/product-list-minus-report.component';
import { ProductListReportComponent } from './components/product/product-list-report/product-list-report.component';
import { ProductService } from '../setup/services/product.service';
import { ProductListLowQuantityReportComponent } from './components/product/product-list-low-quantity-report/product-list-low-quantity-report.component';
import { AccountStatementService } from '../accounting/services/account-statement.service';
import { CategoryService } from '../setup/services/category.service';

@NgModule({
  imports: [
    ReportRoutingModule,
    CommonModule,
    SharedModule
  ],
  declarations: [
    AccountStatementAllClientsComponent,
    AccountStatementSingleClientComponent,
    AccountStatementAllVendorsComponent,
    AccountStatementSingleVendorComponent,
    ProductListReportComponent,
    ProductListMinusReportComponent,
    ProductListLowQuantityReportComponent
  ],
  providers: [
    ReportService,
    ClientVendorService,
    SalesBillService,
    PurchasesBillService,
    ProductService,
    AccountStatementService,
    CategoryService
  ]
})

export class ReportModule {
}
