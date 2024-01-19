import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PurchasesBillTempFormComponent } from './components/purchases-bill-temp-form/purchases-bill-temp-form.component';
import { PurchasesBillTempListComponent } from './components/purchases-bill-temp-list/purchases-bill-temp-list.component';
import { PurchasesBillReturnedFormComponent } from './components/purchases-bill-returned-form/purchases-bill-returned-form.component';
import { PurchasesBillReturnedListComponent } from './components/purchases-bill-returned-list/purchases-bill-returned-list.component';
import { PurchasesBillListComponent } from './components/purchases-bill-list/purchases-bill-list.component';
import { PurchasesBillFormComponent } from './components/purchases-bill-form/purchases-bill-form.component';
import { PurchasesBillSearchComponent } from './components/purchases-bill-search/purchases-bill-search.component';
import { AuthGuard } from '../authentication/services/auth.guard';
import { Privileges } from 'src/app/shared/enums/privileges.enum';

const routes: Routes = [
  { path: 'purchases-bill-list', component: PurchasesBillListComponent, canActivate: [AuthGuard], data: { privilegeId: Privileges.Purchases.PurchasesBills.View } },
  { path: 'purchases-bill-form-view/:id', component: PurchasesBillFormComponent , canActivate: [AuthGuard], data: { privilegeId: Privileges.Purchases.PurchasesBills.View }},
  { path: 'purchases-bill-search', component: PurchasesBillSearchComponent, canActivate: [AuthGuard], data: { privilegeId: Privileges.Purchases.PurchasesBills.View } },
  { path: 'purchases-bill-form', component: PurchasesBillFormComponent , canActivate: [AuthGuard], data: { privilegeId: Privileges.Purchases.PurchasesBills.Add }},
  { path: 'purchases-bill-form/:id', component: PurchasesBillFormComponent, canActivate: [AuthGuard], data: { privilegeId: Privileges.Purchases.PurchasesBills.Edit } },

  { path: 'purchases-bill-temp-list', component: PurchasesBillTempListComponent , canActivate: [AuthGuard], data: { privilegeId: Privileges.Purchases.TempPurchasesBills.View }},
  { path: 'purchases-bill-temp-form', component: PurchasesBillTempFormComponent, canActivate: [AuthGuard], data: { privilegeId: Privileges.Purchases.TempPurchasesBills.Add } },
  { path: 'purchases-bill-temp-form/:id', component: PurchasesBillTempFormComponent, canActivate: [AuthGuard], data: { privilegeId: Privileges.Purchases.TempPurchasesBills.Edit } },

  { path: 'purchases-bill-returned-list', component: PurchasesBillReturnedListComponent, canActivate: [AuthGuard], data: { privilegeId: Privileges.Purchases.ReturnedPurchasesBills.View } },
  { path: 'purchases-bill-returned-form', component: PurchasesBillReturnedFormComponent, data: { privilegeId: Privileges.Purchases.ReturnedPurchasesBills.Add }  },
  { path: 'purchases-bill-new-returned-form', component: PurchasesBillReturnedFormComponent, data: { privilegeId: Privileges.Purchases.ReturnedPurchasesBills.Add }  },
  { path: 'purchases-bill-new-returned-form/:id', component: PurchasesBillReturnedFormComponent , data: { privilegeId: Privileges.Purchases.ReturnedPurchasesBills.Add } },
  { path: 'purchases-bill-returned-form/:id', component: PurchasesBillReturnedFormComponent , data: { privilegeId: Privileges.Purchases.ReturnedPurchasesBills.Edit } },


];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PurchasesBillRoutingModule {
}
