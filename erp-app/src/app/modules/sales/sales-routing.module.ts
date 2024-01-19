import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SalesBillListComponent } from './components/sales-bill/sales-bill-list/sales-bill-list.component';
import { SalesBillFormComponent } from './components/sales-bill/sales-bill-form/sales-bill-form.component';
import { SalesBillSearchComponent } from './components/sales-bill/sales-bill-search/sales-bill-search.component';
import { SalesBillTempFormComponent } from './components/sales-bill/sales-bill-temp-form/sales-bill-temp-form.component';
import { SalesBillTempListComponent } from './components/sales-bill/sales-bill-temp-list/sales-bill-temp-list.component';
import { PendingChangesGuard } from 'src/app/shared/guards/pending-changes-guard.service';
import { SalesBillReturnedFormComponent } from './components/sales-bill/sales-bill-returned-form/sales-bill-returned-form.component';
import { SalesBillReturnedListComponent } from './components/sales-bill/sales-bill-returned-list/sales-bill-returned-list.component';
import { AuthGuard } from '../authentication/services/auth.guard';
import { Privileges } from 'src/app/shared/enums/privileges.enum';

const routes: Routes = [
  { path: 'sales-bill-list', component: SalesBillListComponent, canActivate: [AuthGuard], data: { privilegeId: Privileges.Sales.SalesBills.View } },
  { path: 'sales-bill-form', component: SalesBillFormComponent, canActivate: [AuthGuard], data: { privilegeId: Privileges.Sales.SalesBills.Add }, canDeactivate: [PendingChangesGuard] },
  { path: 'sales-bill-form/:id', component: SalesBillFormComponent, canActivate: [AuthGuard], data: { privilegeId: Privileges.Sales.SalesBills.Edit }, canDeactivate: [PendingChangesGuard] },
  { path: 'sales-bill-form-view/:id', component: SalesBillFormComponent, canActivate: [AuthGuard], data: { privilegeId: Privileges.Sales.SalesBills.View }, canDeactivate: [PendingChangesGuard] },
  { path: 'sales-bill-search', component: SalesBillSearchComponent, canActivate: [AuthGuard], data: { privilegeId: Privileges.Sales.SalesBills.View } },

  { path: 'sales-bill-temp-list', component: SalesBillTempListComponent, canActivate: [AuthGuard], data: { privilegeId: Privileges.Sales.TempSalesBills.View } },
  { path: 'sales-bill-temp-form', component: SalesBillTempFormComponent, canActivate: [AuthGuard], data: { privilegeId: Privileges.Sales.TempSalesBills.Add } },
  { path: 'sales-bill-temp-form/:id', component: SalesBillTempFormComponent, canActivate: [AuthGuard], data: { privilegeId: Privileges.Sales.TempSalesBills.Edit } },

  { path: 'sales-bill-returned-list', component: SalesBillReturnedListComponent, canActivate: [AuthGuard], data: { privilegeId: Privileges.Sales.ReturnedSalesBills.View } },
  { path: 'sales-bill-returned-form', component: SalesBillReturnedFormComponent, canActivate: [AuthGuard], data: { privilegeId: Privileges.Sales.ReturnedSalesBills.Add } },
  { path: 'sales-bill-new-returned-form', component: SalesBillReturnedFormComponent, canActivate: [AuthGuard], data: { privilegeId: Privileges.Sales.ReturnedSalesBills.Add } },
  { path: 'sales-bill-new-returned-form/:id', component: SalesBillReturnedFormComponent, canActivate: [AuthGuard], data: { privilegeId: Privileges.Sales.ReturnedSalesBills.Add } },
  { path: 'sales-bill-returned-form/:id', component: SalesBillReturnedFormComponent, canActivate: [AuthGuard], data: { privilegeId: Privileges.Sales.ReturnedSalesBills.Edit } },



];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SalesRoutingModule {
}
