import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProfileComponent } from './components/user/profile/profile.component';
import { UserFormComponent } from './components/user/user-form/user-form.component';
import { UserListComponent } from './components/user/user-list/user-list.component';
import { RoleListComponent } from './components/role/role-list/role-list.component';
import { RoleFormComponent } from './components/role/role-form/role-form.component';

const routes: Routes = [
  { path: '', component: UserListComponent },
  { path: 'user-list', component: UserListComponent },
  { path: 'user-form', component: UserFormComponent },
  { path: 'edit/:id', component: UserFormComponent },
  { path: 'view/:id', component: UserFormComponent },
  { path: 'profile', component: ProfileComponent },
  { path: 'role-list', component: RoleListComponent },
  { path: 'role-form', component: RoleFormComponent },
  { path: 'role-form/:id', component: RoleFormComponent },

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserManagementRoutingModule {
}
