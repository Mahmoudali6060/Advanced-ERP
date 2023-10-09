import { Component, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { NgForm } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthService } from '../../services/auth.service';
import { LocalStorageService } from '../../../../shared/services/local-storage.service';
import { LocalStorageItems } from '../../../../shared/constants/local-storage-items';
import { HelperService } from '../../../../shared/services/helper.service';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';
import { UserTypeEnum } from 'src/app/modules/user/models/user-type-enum';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']

})
export class LoginComponent {
  invalidLogin: boolean = false;//For showing or error message in case invalid username or password
  clicked = false;
  lang: string;
  strUrl: string;
  userTypeEnum: any;

  constructor(private router: Router,
    private authService: AuthService,
    private localStorageService: LocalStorageService,
    private helperService: HelperService,
    public translate: TranslateService,
    private toasterService: ToastrService) {

  }
  switchLang($event: any) {
    if ($event.target.innerHTML === "English") {
      this.lang = 'en';
    }
    else {
      this.lang = 'ar';
    }
  }
  login(form: NgForm) {
    this.userTypeEnum = UserTypeEnum;
    let credentials = JSON.stringify(form.value);
    this.authService.login(credentials).subscribe((response: any) => {
      if (response) {
        this.localStorageService.setItem(LocalStorageItems.token, response.token);
        this.localStorageService.setItem(LocalStorageItems.email, response.email);
        this.localStorageService.setItem(LocalStorageItems.userProfile, response);
        this.localStorageService.setItem(LocalStorageItems.role, response.role);
        this.localStorageService.setItem(LocalStorageItems.lang, response.defaultLanguage);
        this.helperService.useLanguage(response.defaultLanguage);
        this.invalidLogin = false;
        this.authService.updateLoggedUserProfile();
        if (response.isFirstLogin == true) {
          this.strUrl = `${environment.urlAddress4200}` + "resetPassword?email=" + response.email
          this.router.navigate(["/resetPassword", response.email]);
        }
        else {
          if (response.userTypeId === UserTypeEnum.Admin  ) {
            this.router.navigate(["/dashboard"]);
          } else {
            this.router.navigate(["user/profile"]);
          }
        }
      }
      else {
        this.toasterService.error(this.translate.instant("Errors.InvalidUsernameOrPassword"));
      }
    });
  }
  back() {
    this.router.navigate(['/home']);
  }
}
