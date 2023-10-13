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

const routes: Routes = [
  { path: 'product-list', component: ProductListComponent },
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

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SetupRoutingModule {
}
