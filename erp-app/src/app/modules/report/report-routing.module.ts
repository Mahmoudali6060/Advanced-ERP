import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountStatementAllClientsComponent } from './components/client/account-statement-all-clients/account-statement-all-clients.component';
import { AccountStatementAllVendorsComponent } from './components/vendor/account-statement-all-vendors/account-statement-all-vendors.component';
import { AccountStatementSingleClientComponent } from './components/client/account-statement-single-client/account-statement-single-client.component';
import { AccountStatementSingleVendorComponent } from './components/vendor/account-statement-single-vendor/account-statement-single-vendor.component';

const routes: Routes = [
  { path: 'account-statement-all-clients', component: AccountStatementAllClientsComponent },
  { path: 'account-statement-single-client', component: AccountStatementSingleClientComponent },
  { path: 'account-statement-all-vendors', component: AccountStatementAllVendorsComponent },
  { path: 'account-statement-single-vendor', component: AccountStatementSingleVendorComponent },


];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReportRoutingModule {
}
