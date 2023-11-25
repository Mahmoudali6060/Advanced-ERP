import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SharedModule } from '../shared/shared.module';
import { HttpClient } from '@angular/common/http';
import { LayoutRoutingModule } from './layout-routing.module';
import { SideMenuComponent } from './components/side-menu/side-menu.component';
import { FullLayoutComponent } from './components/full-layout/full-layout.component';
import { HeaderMobileComponent } from './components/header-mobile/header-mobile.component';
import { DatabaseModule } from '../modules/database/database.module';
import { AuthService } from '../modules/authentication/services/auth.service';

@NgModule({
  declarations: [
    HeaderComponent,
    HeaderMobileComponent,
    FooterComponent,
    SideMenuComponent,
    FullLayoutComponent,
  ],
  imports: [
    LayoutRoutingModule,
    SharedModule,
    DatabaseModule,
  ],
  providers: [
    AuthService,
  ]
})
export class LayoutModule { }
