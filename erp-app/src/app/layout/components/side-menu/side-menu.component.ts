import { AfterViewInit, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { LocalStorageItems } from 'src/app/shared/constants/local-storage-items';
import { HelperService } from 'src/app/shared/services/helper.service';
import { LocalStorageService } from 'src/app/shared/services/local-storage.service';
import { SubjectService } from 'src/app/shared/services/subject.service';
import { AuthService } from '../../../modules/authentication/services/auth.service';
import { ConfigService } from '../../../shared/services/config.service';
import { UserProfileDTO } from 'src/app/modules/user-management/models/user-profile.dto';
import { UserTypeEnum } from 'src/app/modules/user-management/models/user-type-enum';
import { UserProfileService } from 'src/app/modules/user-management/services/user.service';

@Component({
  selector: 'app-side-menu',
  templateUrl: './side-menu.component.html',
  styleUrls: ['./side-menu.component.css']
})
export class SideMenuComponent implements OnInit {
  role: any
  serverUrl: string;
  subscription: Subscription;
  userProfile: UserProfileDTO;
  imageUrl: string;
  imageSrc: string;
  userTypeEnum = UserTypeEnum;
  constructor(
    public authService: AuthService,
    private _configService: ConfigService,
    private router: Router,
    public helperService: HelperService,
    private userProfileService: UserProfileService,
    private subjectService: SubjectService,
    private localStorageService: LocalStorageService
  ) {
    this.subjectService.selectedUser.subscribe(res => {
      // this.userProfile = res;
      //  alert("Changed")
      this.getUserById(res.id);
    });
  }

  ngOnInit(): void {
    this.serverUrl = this._configService.getServerUrl();
    this.role = this.helperService.getRole();
    this.userProfile = this.localStorageService.getItem(LocalStorageItems.userProfile);
    this.authService.loggedUserProfile = this.userProfile;
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

  goToUrl(url: string, queryParams: string) {
    this.router.navigate([url], { queryParams: [queryParams], skipLocationChange: true });
  }
  public logOut() {
    this.authService.logOut();
    this.router.navigate(["/home"]);

  }
}
