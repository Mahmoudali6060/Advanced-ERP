import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PurchasesBillListComponent } from './components/purchases-bill/purchases-bill-list/purchases-bill-list.component';
import { PurchasesBillFormComponent } from './components/purchases-bill/purchases-bill-form/purchases-bill-form.component';
import { PurchasesBillSearchComponent } from './components/purchases-bill/purchases-bill-search/purchases-bill-search.component';
import { PurchasesBillTempFormComponent } from './components/purchases-bill-temp-form/purchases-bill-temp-form.component';
import { PurchasesBillTempListComponent } from './components/purchases-bill-temp-list/purchases-bill-temp-list.component';

const routes: Routes = [
  { path: 'purchases-bill-list', component: PurchasesBillListComponent },
  { path: 'purchases-bill-form', component: PurchasesBillFormComponent },
  { path: 'purchases-bill-form/:id', component: PurchasesBillFormComponent },
  { path: 'purchases-bill-search', component: PurchasesBillSearchComponent },
  { path: 'purchases-bill-temp-form', component: PurchasesBillTempFormComponent },
  { path: 'purchases-bill-temp-form/:id', component: PurchasesBillTempFormComponent },
  { path: 'purchases-bill-temp-list', component: PurchasesBillTempListComponent },



];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PurchasesBillRoutingModule {
}
