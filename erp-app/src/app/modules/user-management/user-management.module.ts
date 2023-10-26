import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { UserListComponent } from './components/user/user-list/user-list.component';
import { UserFormComponent } from './components/user/user-form/user-form.component';
import { UserProfileService } from './services/user.service';
import { ProfileComponent } from './components/user/profile/profile.component';
import { AuthModule } from '../authentication/auth.module';
import { InputSwitchModule } from 'primeng/inputswitch';
import { CompanyService } from './services/company.service';
import { ConfigurationsModule } from '../configurations/configurations.module';
import { CountryService } from '../configurations/services/country.service';
import { UserManagementRoutingModule } from './user-management-routing.module';
import { RoleFormComponent } from './components/role/role-form/role-form.component';
import { RoleListComponent } from './components/role/role-list/role-list.component';
import { RoleService } from './services/role.service';

@NgModule({
  imports: [
    UserManagementRoutingModule,
    SharedModule,
    ConfigurationsModule,
    AuthModule,
    InputSwitchModule
  ],
  exports: [

  ],
  declarations: [
    UserListComponent,
    UserFormComponent,
    ProfileComponent,
    RoleFormComponent,
    RoleListComponent
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  providers: [
    UserProfileService,
    CountryService,
    CompanyService,
    RoleService
  ]
})
export class UserManagementModule {
}
