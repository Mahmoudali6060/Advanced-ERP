import { NgModule, ModuleWithProviders, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule, registerLocaleData, DatePipe } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import { TranslateModule } from '@ngx-translate/core';
import arSaLocale from '@angular/common/locales/ar-SA';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { ScrollToModule } from '@nicky-lenaers/ngx-scroll-to';
import { NgSelectModule } from '@ng-select/ng-select';
import { OrderModule } from 'ngx-order-pipe';
import { NgbActiveModal, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { MaterialModule } from '../shared/modules/material.module';
import { ConfirmationDialogComponent } from '../shared/components/confirmation-dialog/confirmation-dialog.component';
import { AuthGuardService } from './guards/auth-guard.service';
import { HelperService } from './services/helper.service';
import { JwtHelperService, JWT_OPTIONS } from '@auth0/angular-jwt';
import { HttpClientModule } from '@angular/common/http';
import { ChangeLangagueComponent } from './components/change-langague/change-langague.component';
import { UserProfileService } from '../modules/user/services/user.service';
import { DialogService } from './services/confirmation-dialog.service';
import { PaginationComponent } from './components/pagination/pagination.component';
import { PagerService } from './services/pager.service';
import { LoaderComponent } from './components/loader/loader.component';
import { NgxSpinnerModule } from 'ngx-spinner';
import {CarouselModule} from 'primeng/carousel'
import {TabViewModule} from 'primeng/tabview';
import { CountryService } from '../modules/configurations/services/country.service';
import { StateService } from '../modules/configurations/services/state.service';
import { CityService } from '../modules/configurations/services/city.service';
import { PortService } from '../modules/configurations/services/port.service';
import { ComboBoxComponent } from './components/combo-box/combo-box.component';

@NgModule({

  imports: [
    CommonModule,
    FormsModule,
    SweetAlert2Module.forRoot(),
    ScrollToModule.forRoot(),
    TranslateModule,
    HttpClientModule,
    NgSelectModule,
    OrderModule,
    NgbModule,
    MaterialModule,
    ReactiveFormsModule,
    NgxSpinnerModule,
    CarouselModule,
    TabViewModule,
    //DropDownsModule

  ],

  exports: [
    CommonModule,
    FormsModule,
    TranslateModule,
    HttpClientModule,
    SweetAlert2Module,
    ScrollToModule,
    NgSelectModule,
    OrderModule,
    NgbModule,
    PaginationComponent,
    MaterialModule,
    ChangeLangagueComponent,
    LoaderComponent,
    ComboBoxComponent
    // ModalBasicComponent
  ],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA
  ],
  declarations: [
    ConfirmationDialogComponent,
    PaginationComponent,
    ChangeLangagueComponent,
    LoaderComponent,
    ComboBoxComponent
    ],
  entryComponents: [
    ConfirmationDialogComponent,
  ],
  providers: [
    DatePipe,
    AuthGuardService,
    HelperService,
    { provide: JWT_OPTIONS, useValue: JWT_OPTIONS },
    JwtHelperService,
    UserProfileService,
    DialogService,
    PagerService,
    CountryService,
    StateService,
    CityService,
    PortService,
    
  ],
})


export class SharedModule {
  // static forRoot(): ModuleWithProviders<SharedModule> {
  //   return {
  //     ngModule: SharedModule
  //   };
  // }
}
registerLocaleData(arSaLocale);
