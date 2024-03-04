import { Component, OnInit } from '@angular/core';
declare var $: any;
import { TranslateService } from '@ngx-translate/core';
import { AuthService } from 'src/app/modules/authentication/services/auth.service';
import { Router } from '@angular/router';
import { DatabaseBackupComponent } from 'src/app/modules/database/components/database-backup/database-backup.component';
import { LocalStorageService } from 'src/app/shared/services/local-storage.service';
import { LocalStorageItems } from 'src/app/shared/constants/local-storage-items';
import { Subscription } from 'rxjs';
import { ConfigService } from 'src/app/shared/services/config.service';
import { HelperService } from 'src/app/shared/services/helper.service';
import { UserProfileService } from 'src/app/modules/user-management/services/user.service';
import { UserProfileDTO } from 'src/app/modules/user-management/models/user-profile.dto';
import { Title } from '@angular/platform-browser';
@Component({
  selector: 'app-header',
  templateUrl: './header.component.html'
})
export class HeaderComponent implements OnInit {
  profile: UserProfileDTO;
  role: any
  serverUrl: string;
  subscription: Subscription;
  imageUrl: string;
  imageSrc: string;
  constructor(
    public authService: AuthService,
    private router: Router,
    private userProfileService: UserProfileService,
    private localStorageService: LocalStorageService,
    private _configService: ConfigService,
    public helperService: HelperService,
    private titleService: Title,
    public translate: TranslateService

  ) {
  }
  ngOnInit(): void {
    this.serverUrl = this._configService.getServerUrl();
    this.role = this.helperService.getRole();
    console.log("role", this.role)
  }


  public gotToURL(url: string, title: string) {
    this.router.navigateByUrl(url);
    this.titleService.setTitle(this.translate.instant(title));
  }

  public toggleSideMenu() {

    if (!$('body').hasClass('layout-fullwidth')) {
      $('body').addClass('layout-fullwidth');

    } else {
      $('body').removeClass('layout-fullwidth');
      $('body').removeClass('layout-default'); // also remove default behaviour if set
    }

    $(this).find('.lnr').toggleClass('lnr-arrow-left-circle lnr-arrow-right-circle');

    if ($(window).innerWidth() < 1025) {
      if (!$('body').hasClass('offcanvas-active')) {
        $('body').addClass('offcanvas-active');
      } else {
        $('body').removeClass('offcanvas-active');
      }
    }

  }

  public switchLanguage(language: string) {

  }

  //#region Open Modal
  public openBackupModal(id?: number) {

  }
  //#endregion

  public logOut() {
    this.authService.logOut();
    this.router.navigate(['/home']);
  }
}
