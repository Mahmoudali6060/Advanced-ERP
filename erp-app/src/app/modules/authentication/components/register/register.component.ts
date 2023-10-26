import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { HelperService } from 'src/app/shared/services/helper.service';
import { LocalStorageService } from 'src/app/shared/services/local-storage.service';
import { LocalStorageItems } from 'src/app/shared/constants/local-storage-items';
import { RegisterModel } from '../../models/register.model';
import { RegisterRequestModel } from '../../models/register-request.model';
import { UserTypeEnum } from 'src/app/modules/user-management/models/user-type-enum';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  registerModel: RegisterModel = new RegisterModel();
  registerRequestModel: RegisterRequestModel = new RegisterRequestModel();
  userTypes: any;
  userTypeEnum = UserTypeEnum;
  constructor(private router: Router,
    private authService: AuthService,
    public translate: TranslateService,
    private localStorageService: LocalStorageService,
    private helperService: HelperService,
    private toasterService: ToastrService
  ) {

  }

  ngOnInit(): void {
    this.userTypes = this.helperService.enumSelector(this.userTypeEnum);

  }

  register(form: NgForm) {
    this.registerRequestModel.registerDTO = this.registerModel;
    this.authService.register(this.registerRequestModel).subscribe((response: any) => {
      if (response) {
        this.localStorageService.setItem(LocalStorageItems.token, response.token);
        this.localStorageService.setItem(LocalStorageItems.email, response.email);
        this.localStorageService.setItem(LocalStorageItems.userProfile, response);
        this.localStorageService.setItem(LocalStorageItems.role, response.role);
        this.localStorageService.setItem(LocalStorageItems.lang, response.defaultLanguage);
        this.helperService.useLanguage(response.defaultLanguage);
        this.authService.updateLoggedUserProfile();
        this.router.navigate(["/resetPassword", response.email]);
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
