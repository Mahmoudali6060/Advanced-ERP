import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ResetPasswordComponent } from 'src/app/modules/authentication/components/reset-password/reset-password.component';
import { ResetPasswordDTO } from 'src/app/modules/authentication/models/reset-password-dto';
import { CityModel } from 'src/app/modules/setup/models/city.model';
import { CountryModel } from 'src/app/modules/setup/models/country.model';
import { StateModel } from 'src/app/modules/setup/models/state.model';
import { CityService } from 'src/app/modules/setup/services/city.service';
import { CountryService } from 'src/app/modules/setup/services/country.service';
import { StateService } from 'src/app/modules/setup/services/state.service';
import { LocalStorageItems } from 'src/app/shared/constants/local-storage-items';
import { LabelValuePair } from 'src/app/shared/enums/label-value-pair';
import { ConfigService } from 'src/app/shared/services/config.service';
import { HelperService } from 'src/app/shared/services/helper.service';
import { LocalStorageService } from 'src/app/shared/services/local-storage.service';
import { SubjectService } from 'src/app/shared/services/subject.service';
import { isNullOrUndefined } from 'util';
import { CompanyDTO } from '../../models/company-dto';
import { ShippingLineProductDTO } from '../../models/shipping-line-product-dto';
import { UserProfileDTO } from '../../models/user-profile.dto';
import { UserTypeEnum } from '../../models/user-type-enum';
import { UserProfileService } from '../../services/user.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  edit: boolean;
  ischangePassword: boolean;
  resetPassDto: ResetPasswordDTO;
  @ViewChild(ResetPasswordComponent) resetPasswordComponent: ResetPasswordComponent;
  @Output() onSave: EventEmitter<boolean> = new EventEmitter<boolean>();
  profile: UserProfileDTO;
  imageSrc: string;
  serverUrl: string;
  loggedProfile: UserProfileDTO;
  imageUrl: string;
  userTypeEnum: any;
  userTypes: LabelValuePair[];
  subscription: Subscription;
  messages: any[] = [];
  viewMode: boolean;
  countryList: Array<CountryModel> = new Array<CountryModel>();
  stateList: Array<StateModel> = new Array<StateModel>();
  cityList: Array<CityModel> = new Array<CityModel>();
  mobNumberPattern = "^((\\+91-?)|0)?[0-9]{10}$";
  categoryId: any;
  selectedProduct: ShippingLineProductDTO;
  productId: any;
  shippingLineProduct: ShippingLineProductDTO = new ShippingLineProductDTO();
  constructor(private subjectService: SubjectService,
    private helperService: HelperService,
    private userProfileService: UserProfileService,
    private _configService: ConfigService,
    private toasterService: ToastrService,
    private localStorageService: LocalStorageService,
    private countryService: CountryService,
    private stateService: StateService,
    private cityService: CityService,
    private translate: TranslateService

  ) {

  }

  ngOnInit(): void {
    this.viewMode = true
    this.edit = false;
    this.ischangePassword = false;
    this.profile = new UserProfileDTO();
    this.userTypeEnum = UserTypeEnum;
    this.userTypes = this.helperService.enumSelector(this.userTypeEnum);
    this.loggedProfile = this.localStorageService.getItem(LocalStorageItems.userProfile);
    console.log("loggedProfile", this.loggedProfile)
    this.getProfileById(this.loggedProfile.id);
    this.getAllCountryList();

  }
  getProfileById(profileId: any) {
    this.userProfileService.getById(profileId).subscribe((res: any) => {
      this.profile = res;
      console.log("profile", this.profile)
      this.imageUrl = this.profile.imageUrl;
      this.serverUrl = this._configService.getServerUrl();
      this.imageSrc = this.serverUrl + "wwwroot/Images/Users/" + this.profile.imageUrl;
      if (!this.profile.imageUrl) {
        this.imageSrc = "assets/images/icon/avatar-big-01.jpg";
      }
      if (this.profile && this.profile.companyDTO != null) {
        this.getAllStateListByCountryId(this.profile.companyDTO.countryId);
        this.getAllCityListByStateId(this.profile.companyDTO.stateId);
      }
    })

  }
  EditProfile() {
    this.viewMode = false;
  }
  save(frm: NgForm) {
    if (this.validattion(this.profile)) {
      this.userProfileService.update(this.profile).subscribe(res => {
        if (res) {
          // this.localStorageService.setItem(LocalStorageItems.lang , this.profile.defaultLanguage);
          if (this.loggedProfile.id == this.profile.id) {
            this.sendUserProfile();
          }
          this.getProfileById(this.profile.id)
          this.toasterService.success("success");
          if (this.loggedProfile.id == this.profile.id) {
          }
          this.viewMode = true;
        }
      })
    }
  }
  sendUserProfile(): void {
    // send message to subscribers via observable subject
    this.subjectService.sendUserProfile(this.profile);
  }
  validattion(profile: UserProfileDTO): boolean {

    if (!profile.firstName || isNullOrUndefined(profile.firstName)) {
      this.toasterService.error(this.translate.instant("Errors.FirstNameIsRequired"));
      return false;
    }
    if (!profile.lastName || isNullOrUndefined(profile.lastName)) {
      this.toasterService.error(this.translate.instant("Errors.LastNameIsRequired"));
      return false;
    }
    if (!profile.mobile || isNullOrUndefined(profile.mobile)) {
      this.toasterService.error(this.translate.instant("Errors.MobileIsRequired"));
      return false;
    }
    if (!profile.userName || isNullOrUndefined(profile.userName)) {
      this.toasterService.error(this.translate.instant("Errors.UserNameIsRequired"));
      return false;
    }
    if (!profile.email || isNullOrUndefined(profile.email)) {
      this.toasterService.error(this.translate.instant("Errors.EmailIsRequired"));
      return false;
    }

    if (!profile.companyDTO.companyNameEn || isNullOrUndefined(profile.companyDTO.companyNameEn)) {
      this.toasterService.error(this.translate.instant("Errors.companyNameEnIsRequired"));
      return false;
    }
    if (!profile.companyDTO.companyNameAr || isNullOrUndefined(profile.companyDTO.companyNameAr)) {
      this.toasterService.error(this.translate.instant("Errors.companyNameArIsRequired"));
      return false;
    }
    if (!profile.companyDTO.countryId) {
      this.toasterService.error(this.translate.instant("Errors.CountryIsRequired"));
      return false;
    }
    if (!profile.companyDTO.stateId) {
      this.toasterService.error(this.translate.instant("Errors.StateIsRequired"));
      return false;
    }
    if (!profile.companyDTO.cityId) {
      this.toasterService.error(this.translate.instant("Errors.CityIsRequired"));
      return false;
    }
    if (!profile.companyDTO.contactPerson || isNullOrUndefined(profile.companyDTO.contactPerson)) {
      this.toasterService.error(this.translate.instant("Errors.InvalidcontactPerson"));
      return false;
    }
    if (!profile.companyDTO.contactTelephone || isNullOrUndefined(profile.companyDTO.contactTelephone)) {
      this.toasterService.error(this.translate.instant("Errors.InvalidcontactTelephone"));
      return false;
    }
    if (!profile.companyDTO.addressDetails || isNullOrUndefined(profile.companyDTO.addressDetails)) {
      this.toasterService.error(this.translate.instant("Errors.AddressDetailsIsRequired"));
      return false;
    }
    if (!profile.companyDTO.companyTypeId) {
      this.toasterService.error(this.translate.instant("Errors.companyTypeIsRequired"));
      return false;
    }
    if (profile.companyDTO.lstShippingLineProduct.length === 0) {
      this.toasterService.error(this.translate.instant("Errors.PleaseSelectAtLeastOneProduct"));
      return false;
    }
    return true;
  }






  changePassword() {
    this.ischangePassword = true;
  }
  closeChangePasswordCard() {
    this.ischangePassword = false;
  }
  close() {
    this.viewMode = true;
    // this.getProfileById(this.loggedProfile.id);
  }
  ConfirmeChangePassword() {
    this.resetPasswordComponent.resetPassword;
    this.closeChangePasswordCard();
  }
  onFileChange(event: any) {

    const reader = new FileReader();
    if (event.target.files && event.target.files.length) {
      const [file] = event.target.files;
      reader.readAsDataURL(file);

      reader.onload = () => {
        this.imageSrc = reader.result as string;
        this.profile.imageBase64 = this.imageSrc;

      };

    }
  }

  //#region DropdownLists
  getAllCountryList() {
    this.countryService.getAllLite().subscribe((response: any) => {
      this.countryList = response.list;
    });
  }

  getAllStateListByCountryId(countryId: number) {
    this.stateService.getAllLiteByCountryId(countryId).subscribe((response: any) => {
      this.stateList = response.list;
    });
  }

  getAllCityListByStateId(stateId: number) {
    this.cityService.getAllLiteByStateId(stateId).subscribe((response: any) => {
      this.cityList = response.list;
    });
  }

  onCountrySelected(countryId: any) {
    this.getAllStateListByCountryId(this.profile.companyDTO.countryId);
  }

  onStateSelected(stateId: any) {
    this.getAllCityListByStateId(this.profile.companyDTO.stateId);
  }
  //#endregion


}
