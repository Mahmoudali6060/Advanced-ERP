import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProfileComponent } from './components/user/profile/profile.component';
import { UserFormComponent } from './components/user/user-form/user-form.component';
import { UserListComponent } from './components/user/user-list/user-list.component';
import { RoleListComponent } from './components/role/role-list/role-list.component';
import { RoleFormComponent } from './components/role/role-form/role-form.component';
import { AuthGuard } from '../authentication/services/auth.guard';
import { Privileges } from 'src/app/shared/enums/privileges.enum';

const routes: Routes = [
  { path: '', component: UserListComponent, canActivate: [AuthGuard], data: { privilegeId: Privileges.UserManagement.Users.View } },
  { path: 'user-list', component: UserListComponent, canActivate: [AuthGuard], data: { privilegeId: Privileges.UserManagement.Users.View } },
  { path: 'user-form', component: UserFormComponent, canActivate: [AuthGuard], data: { privilegeId: Privileges.UserManagement.Users.Add } },
  { path: 'user-form/:id', component: UserFormComponent, canActivate: [AuthGuard], data: { privilegeId: Privileges.UserManagement.Users.Edit } },
  { path: 'view/:id', component: UserFormComponent, canActivate: [AuthGuard], data: { privilegeId: Privileges.UserManagement.Users.View } },
  { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard], data: { privilegeId: Privileges.UserManagement.Users.View } },
  { path: 'role-list', component: RoleListComponent,data: { privilegeId: Privileges.UserManagement.Roles.View }  },
  { path: 'role-form', component: RoleFormComponent,data: { privilegeId: Privileges.UserManagement.Roles.Add }  },
  { path: 'role-form/:id', component: RoleFormComponent,data: { privilegeId: Privileges.UserManagement.Roles.Edit }  },

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserManagementRoutingModule {
}
