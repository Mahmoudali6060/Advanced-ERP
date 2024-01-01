import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PurchasesBillTempFormComponent } from './components/purchases-bill-temp-form/purchases-bill-temp-form.component';
import { PurchasesBillTempListComponent } from './components/purchases-bill-temp-list/purchases-bill-temp-list.component';
import { PurchasesBillReturnedFormComponent } from './components/purchases-bill-returned-form/purchases-bill-returned-form.component';
import { PurchasesBillReturnedListComponent } from './components/purchases-bill-returned-list/purchases-bill-returned-list.component';
import { PurchasesBillListComponent } from './components/purchases-bill-list/purchases-bill-list.component';
import { PurchasesBillFormComponent } from './components/purchases-bill-form/purchases-bill-form.component';
import { PurchasesBillSearchComponent } from './components/purchases-bill-search/purchases-bill-search.component';

const routes: Routes = [
  { path: 'purchases-bill-list', component: PurchasesBillListComponent },
  { path: 'purchases-bill-form', component: PurchasesBillFormComponent },
  { path: 'purchases-bill-form/:id', component: PurchasesBillFormComponent },
  { path: 'purchases-bill-form-view/:id', component: PurchasesBillFormComponent },
  { path: 'purchases-bill-search', component: PurchasesBillSearchComponent },
  { path: 'purchases-bill-temp-form', component: PurchasesBillTempFormComponent },
  { path: 'purchases-bill-temp-form/:id', component: PurchasesBillTempFormComponent },
  { path: 'purchases-bill-temp-list', component: PurchasesBillTempListComponent },
  { path: 'purchases-bill-new-returned-form', component: PurchasesBillReturnedFormComponent },
  { path: 'purchases-bill-new-returned-form/:id', component: PurchasesBillReturnedFormComponent },
  { path: 'purchases-bill-returned-form', component: PurchasesBillReturnedFormComponent },
  { path: 'purchases-bill-returned-form/:id', component: PurchasesBillReturnedFormComponent },
  { path: 'purchases-bill-returned-list', component: PurchasesBillReturnedListComponent },


];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PurchasesBillRoutingModule {
}
