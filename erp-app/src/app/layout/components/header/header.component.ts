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
@Component({
  selector: 'app-header',
  templateUrl: './header.component.html'
})
export class HeaderComponent implements OnInit{
  profile: UserProfileDTO;
  role: any
  serverUrl: string;
  subscription: Subscription;
  userProfile: UserProfileDTO;
  imageUrl: string;
  imageSrc: string;
  constructor(
    public authService: AuthService,
    private router: Router,
    private userProfileService:UserProfileService,
    private localStorageService:LocalStorageService,
    private _configService: ConfigService,
    private helperService: HelperService,

    ) {
  }
  ngOnInit(): void {
    this.serverUrl = this._configService.getServerUrl();
    this.role = this.helperService.getRole();
    console.log("role", this.role)
    this.userProfile = this.localStorageService.getItem(LocalStorageItems.userProfile);
    if (this.userProfile) this.getUserById(this.userProfile.id);
  }
  getUserById(userId: number) {
    this.userProfileService.getById(userId).subscribe((res: any) => {
      this.userProfile = res;
      if (res) {
        this.imageUrl = this.userProfile.imageUrl;
        this.serverUrl = this._configService.getServerUrl();
        this.imageSrc = this.serverUrl + "wwwroot/Images/Users/" + this.userProfile.imageUrl;
        if (!this.userProfile.imageUrl) {
          this.imageSrc = "assets/images/icon/avatar-big-01.jpg";
        }
      }
    })

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
