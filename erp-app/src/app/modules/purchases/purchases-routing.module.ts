import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PurchasesBillListComponent } from './components/purchases-bill/purchases-bill-list/purchases-bill-list.component';
import { PurchasesBillFormComponent } from './components/purchases-bill/purchases-bill-form/purchases-bill-form.component';

const routes: Routes = [
  { path: 'purchases-bill-list', component: PurchasesBillListComponent },
  { path: 'purchases-bill-form', component: PurchasesBillFormComponent },
  { path: 'purchases-bill-form/:id', component: PurchasesBillFormComponent },

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PurchasesBillRoutingModule {
}
