import { Component, EventEmitter, Output, ViewChild, Inject } from '@angular/core';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { TranslateService } from '@ngx-translate/core';
import { LocalStorageItems } from '../../constants/local-storage-items';
import { HelperService } from '../../services/helper.service';
import { LocalStorageService } from '../../services/local-storage.service';
import { UserProfileDTO } from 'src/app/modules/user-management/models/user-profile.dto';
import { UserProfileService } from 'src/app/modules/user-management/services/user.service';

@Component({
  selector: 'app-change-langague',
  templateUrl: './change-langague.component.html',
  styleUrls: ['./change-langague.component.css']
})

export class ChangeLangagueComponent {

  constructor(public translate: TranslateService,
    public helperService: HelperService,
    private userProfileService: UserProfileService,
    private localStorageService: LocalStorageService) {

  }

  useLanguage(language: string): void {
    this.helperService.useLanguage(language);
    this.updateUserLanguage(language);
    this.translate.currentLang = language;

  }

  updateUserLanguage(language: string) {
    let userProfileDTO = this.localStorageService.getItem(LocalStorageItems.userProfile) as UserProfileDTO;
    if (userProfileDTO) {
      userProfileDTO.defaultLanguage = language;
      this.localStorageService.updateItem(LocalStorageItems.userProfile, userProfileDTO);
      this.userProfileService.update(userProfileDTO).subscribe(res => {

      })
    }
  }

}
