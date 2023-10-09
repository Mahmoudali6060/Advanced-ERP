import { NgModule } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { SetupRoutingModule } from './setup-routing.module';
import { CityService } from './services/city.service';
import { CountryService } from './services/country.service';
import { StateService } from './services/state.service';
import { ContactUsListComponent } from './components/contact-us-list/contact-us-list.component';
import { AboutUsComponent } from './components/about-us/about-us.component';
import { AboutUsFormComponent } from './components/about-us-form/about-us-form.component';
import { AdvertismentComponent } from './components/advertisment/advertisment.component';
import { AdvertismentFormComponent } from './components/advertisment-form/advertisment-form.component';
import { PortService } from './services/port.service';


@NgModule({
  imports: [
    SetupRoutingModule,
    SharedModule,
  ],

  declarations: [
  ContactUsListComponent,
  AboutUsComponent,
  AboutUsFormComponent,
  AdvertismentComponent,
  AdvertismentFormComponent,
  ],

  providers: [
    CountryService,
    StateService,
    CityService,
    PortService
  ]

})

export class SetupModule {
}
