import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductListComponent } from './components/product/product-list/product-list.component';
import { ProductFormComponent } from './components/product/product-form/product-form.component';
import { CategoryListComponent } from './components/category/category-list/category-list.component';
import { CategoryFormComponent } from './components/category/category-form/category-form.component';
import { ClientListComponent } from './components/client/client-list/client-list.component';
import { ClientFormComponent } from './components/client/client-form/client-form.component';
import { VendorListComponent } from './components/vendor/vendor-list/vendor-list.component';
import { VendorFormComponent } from './components/vendor/vendor-form/vendor-form.component';
import { CompanyListComponent } from './components/company/company-list/company-list.component';
import { CompanyFormComponent } from './components/company/company-form/company-form.component';
import { ProductListViewComponent } from './components/product/product-list-view/product-list-view.component';
import { ProductListChangePriceComponent } from './components/product/product-list-change-price/product-list-change-price.component';
import { ProductListChangeQuantityComponent } from './components/product/product-list-change-quantity/product-list-change-quantity.component';
import { RepresentiveFormComponent } from './components/representive/representive-form/representive-form.component';
import { RepresentiveListComponent } from './components/representive/representive-list/representive-list.component';
import { UnitOfMeasurementListComponent } from './components/unit-of-measurement/unit-of-measurement-list/unit-of-measurement-list.component';
import { UnitOfMeasurementFormComponent } from './components/unit-of-measurement/unit-of-measurement-form/unit-of-measurement-form.component';
import { ProductTrackingComponent } from './components/product/product-tracking/product-tracking.component';
import { Privileges } from 'src/app/shared/enums/privileges.enum';
import { AuthGuard } from '../authentication/services/auth.guard';

const routes: Routes = [
  { path: 'product-list-view', component: ProductListViewComponent, canActivate: [AuthGuard],data: { privilegeId: Privileges.Setup.Products.View } },
  { path: 'product-list-change-price', component: ProductListChangePriceComponent ,canActivate: [AuthGuard], data: { privilegeId: Privileges.Setup.Products.ChangePrice }},
  { path: 'product-list-change-quantity', component: ProductListChangeQuantityComponent , canActivate: [AuthGuard],data: { privilegeId: Privileges.Setup.Products.ChangeQuantity }},
  { path: 'product-form', component: ProductFormComponent ,canActivate: [AuthGuard], data: { privilegeId: Privileges.Setup.Products.Add }},
  { path: 'product-form/:id', component: ProductFormComponent,canActivate: [AuthGuard], data: { privilegeId: Privileges.Setup.Products.Edit } },
  { path: 'product-tracking/:productId', component: ProductTrackingComponent ,canActivate: [AuthGuard], data: { privilegeId: Privileges.Setup.Products.ViewProductTracking }},
  { path: 'product-tracking', component: ProductTrackingComponent ,canActivate: [AuthGuard], data: { privilegeId: Privileges.Setup.Products.ViewProductTracking }},

  { path: 'category-list', component: CategoryListComponent, canActivate: [AuthGuard],data: { privilegeId: Privileges.Setup.Categories.View } },
  { path: 'category-form', component: CategoryFormComponent,canActivate: [AuthGuard], data: { privilegeId: Privileges.Setup.Categories.Add } },
  { path: 'category-form/:id', component: CategoryFormComponent ,canActivate: [AuthGuard], data: { privilegeId: Privileges.Setup.Categories.Edit }},

  { path: 'client-list', component: ClientListComponent,canActivate: [AuthGuard], data: { privilegeId: Privileges.Setup.Clients.View } },
  { path: 'client-form', component: ClientFormComponent, canActivate: [AuthGuard],data: { privilegeId: Privileges.Setup.Clients.Add } },
  { path: 'client-form/:id', component: ClientFormComponent ,canActivate: [AuthGuard], data: { privilegeId: Privileges.Setup.Clients.Edit }},

  { path: 'vendor-list', component: VendorListComponent ,canActivate: [AuthGuard], data: { privilegeId: Privileges.Setup.Vendors.View }},
  { path: 'vendor-form', component: VendorFormComponent, canActivate: [AuthGuard],data: { privilegeId: Privileges.Setup.Vendors.Add } },
  { path: 'vendor-form/:id', component: VendorFormComponent , canActivate: [AuthGuard],data: { privilegeId: Privileges.Setup.Vendors.Edit }},
  
  { path: 'company-list', component: CompanyListComponent,canActivate: [AuthGuard], data: { privilegeId: Privileges.Setup.Companies.View } },
  { path: 'company-form', component: CompanyFormComponent,canActivate: [AuthGuard], data: { privilegeId: Privileges.Setup.Representives.Add }},
  { path: 'company-form/:id', component: CompanyFormComponent,canActivate: [AuthGuard], data: { privilegeId: Privileges.Setup.Representives.Edit } },
  
  { path: 'representive-list', component: RepresentiveListComponent,canActivate: [AuthGuard], data: { privilegeId: Privileges.Setup.Representives.View } },
  { path: 'representive-form', component: RepresentiveFormComponent , canActivate: [AuthGuard],data: { privilegeId: Privileges.Setup.Representives.Add }},
  { path: 'representive-form/:id', component: RepresentiveFormComponent , canActivate: [AuthGuard],data: { privilegeId: Privileges.Setup.Representives.Add }},

  { path: 'unit-of-measurement-list', component: UnitOfMeasurementListComponent , canActivate: [AuthGuard],data: { privilegeId: Privileges.Setup.UnitOfMeasurements.View }},
  { path: 'unit-of-measurement-form', component: UnitOfMeasurementFormComponent,canActivate: [AuthGuard], data: { privilegeId: Privileges.Setup.UnitOfMeasurements.Add } },
  { path: 'unit-of-measurement-form/:id', component: UnitOfMeasurementFormComponent, canActivate: [AuthGuard],data: { privilegeId: Privileges.Setup.UnitOfMeasurements.Edit } },

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SetupRoutingModule {
}
