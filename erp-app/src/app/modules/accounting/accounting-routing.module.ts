import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TreasuryListComponent } from './components/treasury/treasury-list/treasury-list.component';
import { TreasuryFormComponent } from './components/treasury/treasury-form/treasury-form.component';
import { AuthGuard } from '../authentication/services/auth.guard';
import { Privileges } from 'src/app/shared/enums/privileges.enum';

const routes: Routes = [
  { path: 'treasury-list', component: TreasuryListComponent, canActivate: [AuthGuard], data: { privilegeId: Privileges.Accounting.Treasuries.View }  },
  { path: 'treasury-form-view/:id', component: TreasuryFormComponent , canActivate: [AuthGuard], data: { privilegeId: Privileges.Accounting.Treasuries.View } },
  { path: 'treasury-form', component: TreasuryFormComponent , canActivate: [AuthGuard], data: { privilegeId: Privileges.Accounting.Treasuries.Add } },
  { path: 'treasury-form/:id', component: TreasuryFormComponent , canActivate: [AuthGuard], data: { privilegeId: Privileges.Accounting.Treasuries.Edit } },



];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AccountingRoutingModule {
}
