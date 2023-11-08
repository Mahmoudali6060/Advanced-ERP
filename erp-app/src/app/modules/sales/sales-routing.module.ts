import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SalesBillListComponent } from './components/sales-bill/sales-bill-list/sales-bill-list.component';
import { SalesBillFormComponent } from './components/sales-bill/sales-bill-form/sales-bill-form.component';
import { SalesBillSearchComponent } from './components/sales-bill/sales-bill-search/sales-bill-search.component';

const routes: Routes = [
  { path: 'sales-bill-list', component: SalesBillListComponent },
  { path: 'sales-bill-form', component: SalesBillFormComponent },
  { path: 'sales-bill-form/:id', component: SalesBillFormComponent },
  { path: 'sales-bill-search', component: SalesBillSearchComponent },



];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SalesRoutingModule {
}
