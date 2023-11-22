import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TreasuryListComponent } from './components/treasury/treasury-list/treasury-list.component';
import { TreasuryFormComponent } from './components/treasury/treasury-form/treasury-form.component';

const routes: Routes = [
  { path: 'treasury-list', component: TreasuryListComponent },
  { path: 'treasury-form', component: TreasuryFormComponent },
  { path: 'treasury-form/:id', component: TreasuryFormComponent },

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AccountingRoutingModule {
}
