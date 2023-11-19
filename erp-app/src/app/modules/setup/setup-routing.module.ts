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

const routes: Routes = [
  { path: 'product-list-view', component: ProductListViewComponent },
  { path: 'product-list-change-price', component: ProductListChangePriceComponent },
  { path: 'product-list-change-quantity', component: ProductListChangeQuantityComponent },
  { path: 'product-form', component: ProductFormComponent },
  { path: 'product-form/:id', component: ProductFormComponent },
  { path: 'category-list', component: CategoryListComponent },
  { path: 'category-form', component: CategoryFormComponent },
  { path: 'category-form/:id', component: CategoryFormComponent },
  { path: 'client-list', component: ClientListComponent },
  { path: 'client-form', component: ClientFormComponent },
  { path: 'client-form/:id', component: ClientFormComponent },
  { path: 'vendor-list', component: VendorListComponent },
  { path: 'vendor-form', component: VendorFormComponent },
  { path: 'vendor-form/:id', component: VendorFormComponent },
  { path: 'company-list', component: CompanyListComponent },
  { path: 'company-form', component: CompanyFormComponent },
  { path: 'company-form/:id', component: CompanyFormComponent },

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SetupRoutingModule {
}
