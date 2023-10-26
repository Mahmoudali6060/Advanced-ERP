import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { PrimeNGConfig } from 'primeng/api';
import { LocalStorageItems } from './shared/constants/local-storage-items';
import { LocalStorageService } from './shared/services/local-storage.service';
import { UserProfileDTO } from './modules/user-management/models/user-profile.dto';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'base';
  typeSelected: string;
  constructor(private primengConfig: PrimeNGConfig,private translate: TranslateService,
    private localStorageService: LocalStorageService,
) {
  this.typeSelected = 'ball-clip-rotate-multiple';
    let userProfileDTO = this.localStorageService.getItem(LocalStorageItems.userProfile) as UserProfileDTO;
   // if (userProfileDTO) {
    //  this.helpserService.useLanguage(userProfileDTO.defaultLanguage);
   // }
 //   else {
      this.primengConfig.ripple = true;
      translate.setDefaultLang('en');
      translate.currentLang = 'en';
  //  }

  }
}
