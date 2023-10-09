import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomeRoutingModule } from './home-routing.module';
import { LandingPageComponent } from './Components/landing-page/landing-page.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { CarouselModule } from 'primeng/carousel'

@NgModule({
  declarations: [
    LandingPageComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,
    HomeRoutingModule,
    CarouselModule

  ], exports: [
    LandingPageComponent,
  ],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA
  ],
})
export class HomeModule { }
