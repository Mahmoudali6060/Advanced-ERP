import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { UserRoutingModule } from './user-routing.module';
import { UserListComponent } from './components/user-list/user-list.component';
import { UserFormComponent } from './components/user-form/user-form.component';
import { UserProfileService } from './services/user.service';
import { ProfileComponent } from './components/profile/profile.component';
import { AuthModule } from '../authentication/auth.module';
import {InputSwitchModule} from 'primeng/inputswitch';
import { CompanyService } from './services/company.service';
import { ConfigurationsModule } from '../configurations/configurations.module';
import { CountryService } from '../configurations/services/country.service';

@NgModule({
  imports: [
    UserRoutingModule,
    SharedModule,
    ConfigurationsModule,
    AuthModule,
    InputSwitchModule
  ],
  exports:[
    
  ],
  declarations: [
    UserListComponent,
    UserFormComponent,
    ProfileComponent,
  ],
   schemas: [ CUSTOM_ELEMENTS_SCHEMA ],
  providers: [
    UserProfileService,
    CountryService,
    CompanyService
  ]
})
export class UserModule {
}
