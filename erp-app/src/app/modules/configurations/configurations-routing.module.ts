import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserFormComponent } from '../user/components/user-form/user-form.component';
import { AboutUsFormComponent } from './components/about-us-form/about-us-form.component';
import { AboutUsComponent } from './components/about-us/about-us.component';
import { AdvertismentFormComponent } from './components/advertisment-form/advertisment-form.component';
import { AdvertismentComponent } from './components/advertisment/advertisment.component';
import { ContactUsListComponent } from './components/contact-us-list/contact-us-list.component';


const routes: Routes = [
  { path: 'contact-us-list', component: ContactUsListComponent },
  { path: 'about-us', component: AboutUsComponent },
  { path: 'edit-about-us/:id', component: AboutUsFormComponent },
  { path: 'advertisment', component: AdvertismentComponent },
  { path: 'add-advertisment', component: AdvertismentFormComponent },
  { path: 'edit-advertisment/:id', component: AdvertismentFormComponent },

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class ConfigurationsRoutingModule {
}
