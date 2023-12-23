import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SalesBillListComponent } from './components/sales-bill/sales-bill-list/sales-bill-list.component';
import { SalesBillFormComponent } from './components/sales-bill/sales-bill-form/sales-bill-form.component';
import { SalesBillSearchComponent } from './components/sales-bill/sales-bill-search/sales-bill-search.component';
import { SalesBillTempFormComponent } from './components/sales-bill/sales-bill-temp-form/sales-bill-temp-form.component';
import { SalesBillTempListComponent } from './components/sales-bill/sales-bill-temp-list/sales-bill-temp-list.component';
import { PendingChangesGuard } from 'src/app/shared/guards/pending-changes-guard.service';

const routes: Routes = [
  { path: 'sales-bill-list', component: SalesBillListComponent },
  { path: 'sales-bill-form', component: SalesBillFormComponent, canDeactivate: [PendingChangesGuard] },
  { path: 'sales-bill-form/:id', component: SalesBillFormComponent, canDeactivate: [PendingChangesGuard] },
  { path: 'sales-bill-form-view/:id', component: SalesBillFormComponent, canDeactivate: [PendingChangesGuard] },
  { path: 'sales-bill-search', component: SalesBillSearchComponent },
  { path: 'sales-bill-temp-form', component: SalesBillTempFormComponent },
  { path: 'sales-bill-temp-form/:id', component: SalesBillTempFormComponent },
  { path: 'sales-bill-temp-list', component: SalesBillTempListComponent },




];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SalesRoutingModule {
}
