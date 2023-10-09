import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from 'src/app/modules/authentication/components/register/register.component';
import { ResetPasswordComponent } from './components/reset-password/reset-password.component';
import { ForgotPasswordComponent } from './components/forgot-password/forgot-password.component';
import { RequestFollowingComponent } from './components/request-following/request-following.component';

const routes: Routes = [
  // { path: '', component: LoginComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register/:companyCategory', component: RegisterComponent },
  { path: 'resetPassword', component: ResetPasswordComponent },
  { path: 'resetPassword/:email', component: ResetPasswordComponent },
  { path: 'forgotpassword', component: ForgotPasswordComponent },
  { path: 'request-following/:id/:userId', component: RequestFollowingComponent },

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthRoutingModule {
}
