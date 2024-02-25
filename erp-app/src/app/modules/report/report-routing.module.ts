import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountStatementAllClientsComponent } from './components/client/account-statement-all-clients/account-statement-all-clients.component';
import { AccountStatementAllVendorsComponent } from './components/vendor/account-statement-all-vendors/account-statement-all-vendors.component';
import { AccountStatementSingleClientComponent } from './components/client/account-statement-single-client/account-statement-single-client.component';
import { AccountStatementSingleVendorComponent } from './components/vendor/account-statement-single-vendor/account-statement-single-vendor.component';
import { ProductListMinusReportComponent } from './components/product/product-list-minus-report/product-list-minus-report.component';
import { ProductListLowQuantityReportComponent } from './components/product/product-list-low-quantity-report/product-list-low-quantity-report.component';
import { AuthGuard } from '../authentication/services/auth.guard';
import { Privileges } from 'src/app/shared/enums/privileges.enum';
import { ProductListReportComponent } from './components/product/product-list-report/product-list-report.component';

const routes: Routes = [
  { path: 'account-statement-all-clients', component: AccountStatementAllClientsComponent, canActivate: [AuthGuard], data: { privilegeId: Privileges.Reports.AccountStatementAllClients.View } },
  { path: 'account-statement-single-client', component: AccountStatementSingleClientComponent, canActivate: [AuthGuard], data: { privilegeId: Privileges.Reports.AccountStatementSingleClient.View } },
  { path: 'account-statement-all-vendors', component: AccountStatementAllVendorsComponent, canActivate: [AuthGuard], data: { privilegeId: Privileges.Reports.AccountStatementAllVendors.View } },
  { path: 'account-statement-single-vendor', component: AccountStatementSingleVendorComponent, canActivate: [AuthGuard], data: { privilegeId: Privileges.Reports.AccountStatementSingleVendor.View } },
  { path: 'product-list-report', component: ProductListReportComponent, canActivate: [AuthGuard], data: { privilegeId: Privileges.Setup.Products.View } },
  { path: 'product-list-minus-report', component: ProductListMinusReportComponent, canActivate: [AuthGuard], data: { privilegeId: Privileges.Reports.ProductMinusQuantity.View } },
  { path: 'product-list-low-quantity-report', component: ProductListLowQuantityReportComponent, canActivate: [AuthGuard], data: { privilegeId: Privileges.Reports.ProductLowQuantity.View } },

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReportRoutingModule {
}
