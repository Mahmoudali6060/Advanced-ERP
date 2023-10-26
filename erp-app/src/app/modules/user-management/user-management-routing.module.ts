import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProfileComponent } from './components/profile/profile.component';
import { UserFormComponent } from './components/user-form/user-form.component';
import { UserListComponent } from './components/user-list/user-list.component';

const routes: Routes = [
  { path: '', component: UserListComponent },
  { path: 'user-list', component: UserListComponent },
  { path: 'user-form', component: UserFormComponent },
  { path: 'edit/:id', component: UserFormComponent },
  { path: 'view/:id', component: UserFormComponent },
  { path: 'profile', component: ProfileComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserManagementRoutingModule {
}
