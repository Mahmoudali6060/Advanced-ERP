import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './modules/authentication/services/auth.guard';
import { LandingPageComponent } from './modules/home/Components/landing-page/landing-page.component';
import { LoginComponent } from './modules/authentication/components/login/login.component';

const routes: Routes = [
  // { path: '', loadChildren: () => import('./modules/home/home.module').then(m => m.HomeModule) },
  { path: '', loadChildren: () => import('./modules/authentication/auth.module').then(m => m.AuthModule)},
  { path: '', loadChildren: () => import('./layout/layout.module').then(m => m.LayoutModule)},

   { path: '**', redirectTo: '/login' },
  {path :'**', component: LoginComponent},
  {path :'login', component: LoginComponent}


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
