import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { AuthRoutingModule } from './auth-routing.module';
import { LoginComponent } from './components/login/login.component';
import { SharedModule } from '../../shared/shared.module';
import { AuthService } from './services/auth.service';
import { RegisterComponent } from 'src/app/modules/authentication/components/register/register.component';
import { ResetPasswordComponent } from './components/reset-password/reset-password.component';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { ForgotPasswordComponent } from './components/forgot-password/forgot-password.component';
import { RequestFollowingComponent } from './components/request-following/request-following.component';
import {TabViewModule} from 'primeng/tabview';
import {ButtonModule} from 'primeng/button';
import { ConfigurationsModule } from '../configurations/configurations.module';

@NgModule({
  imports: [
    AuthRoutingModule,
    SharedModule,
    CommonModule ,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    ConfigurationsModule,
    TabViewModule,
    ButtonModule
    
  ],
  declarations: [
    LoginComponent,
    RegisterComponent,
    ResetPasswordComponent,
    ForgotPasswordComponent,
    RequestFollowingComponent
  ],
    exports: [
      ResetPasswordComponent,
      RegisterComponent
  ],
  schemas: [ 
    CUSTOM_ELEMENTS_SCHEMA
  ],
  providers: [
  ]
})

export class AuthModule {
}
