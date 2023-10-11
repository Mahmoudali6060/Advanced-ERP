import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { UserProfileDTO } from '../../models/user-profile.dto';
import { UserProfileService } from '../../services/user.service';
import { Location } from '@angular/common';
import { ConfigService } from 'src/app/shared/services/config.service';
import { NgForm } from '@angular/forms';
import { UserTypeEnum } from '../../models/user-type-enum';
import { HelperService } from 'src/app/shared/services/helper.service';
import { SubjectService } from 'src/app/shared/services/subject.service';
import { LocalStorageService } from 'src/app/shared/services/local-storage.service';
import { LocalStorageItems } from 'src/app/shared/constants/local-storage-items';
import { isNullOrUndefined } from 'util';

@Component({
	selector: 'app-user-form',
	templateUrl: './user-form.component.html',
	styleUrls: ['./user-form.component.css']
})
export class UserFormComponent {

	userProfileDTO: UserProfileDTO = new UserProfileDTO();
	imageSrc!: string;
	serverUrl: string;
	phonePatern = "^((\\+91-?)|0)?[0-9]{10}$";
	mobNumberPattern = "^((\\+91-?)|0)?[0-9]{10}$";
	userTypeEnum: any
	userTypes: any
	types: any
	userProfile: UserProfileDTO;
	viewMode: boolean;
	constructor(private userProfileService: UserProfileService,
		private route: ActivatedRoute,
		private toasterService: ToastrService,
		private location: Location, private _configService: ConfigService,
		private helperService: HelperService,
		private translate: TranslateService,
		private subjectService: SubjectService,
		private localStorageService: LocalStorageService,
		private router: Router) {
	}

	ngOnInit() {
		this.userTypeEnum = UserTypeEnum;
		this.userProfile = this.localStorageService.getItem(LocalStorageItems.userProfile);
		this.userTypes = this.helperService.enumSelector(this.userTypeEnum);

		this.imageSrc = "assets/images/icon/avatar-big-01.jpg";
		this.userProfileDTO = new UserProfileDTO();
		const id = this.route.snapshot.paramMap.get('id');
		if (id) {
			this.getUserById(id);
			if (this.router.url.includes('/view/')) {
				this.viewMode = true;
			}
		}
	}
	getUserById(userId: any) {
		this.userProfileService.getById(userId).subscribe((res: any) => {
			this.userProfileDTO = res;
			this.serverUrl = this._configService.getServerUrl();
			this.imageSrc = this.serverUrl + "wwwroot/Images/Users/" + this.userProfileDTO.imageUrl;
			if (!this.userProfileDTO.imageUrl) {
				this.imageSrc = "assets/images/icon/avatar-big-01.jpg";
			}
		})
	}
	handleChange(event: boolean){
		// this.userProfileDTO.isActive = event.target
	}

	cancel() {

			this.router.navigateByUrl('user/user-list');
	}
	validattion(userProfileDTO: UserProfileDTO): boolean {
		if (!userProfileDTO.firstName || isNullOrUndefined(userProfileDTO.firstName)) {
			this.toasterService.error(this.translate.instant("Errors.FirstNameIsRequired"));
			return false;
		  }
		  if (!userProfileDTO.lastName || isNullOrUndefined(userProfileDTO.lastName)) {
			this.toasterService.error(this.translate.instant("Errors.LastNameIsRequired"));
			return false;
		  }
		  if (!userProfileDTO.mobile || isNullOrUndefined(userProfileDTO.mobile)) {
			this.toasterService.error(this.translate.instant("Errors.MobileIsRequired"));
			return false;
		  }
		  if (!userProfileDTO.userName || isNullOrUndefined(userProfileDTO.userName)) {
			this.toasterService.error(this.translate.instant("Errors.UserNameIsRequired"));
			return false;
		  }
		  if (!userProfileDTO.email || isNullOrUndefined(userProfileDTO.email)) {
			this.toasterService.error(this.translate.instant("Errors.EmailIsRequired"));
			return false;
		  }
		// if (!userProfileDTO.role) {
		// 	this.toasterService.error(this.translate.instant("Errors.RoleIsRequired"));
		// 	return false;
		// }
		return true;
	}
	save(frm: NgForm) {
		// if (this.validattion(this.userProfileDTO)) {
		this.userProfileDTO.role = this.userTypeEnum[this.userProfileDTO.userTypeId]
		if (this.validattion(this.userProfileDTO)) {

			if (this.userProfileDTO.id) {
				this.userProfileService.update(this.userProfileDTO).subscribe(res => {
					// this.localStorageService.setItem(LocalStorageItems.lang , this.userProfileDTO.defaultLanguage)
					this.toasterService.success("success");
					if (this.userProfile.id == this.userProfileDTO.id) {
						this.sendUserProfile();
					}
					this.cancel();
				})
			}
			else {
				this.userProfileDTO.password = "P@ssw0rd"
				//this.userProfileDTO.userTypeId = UserTypeEnum.Operator; //by default
				this.userProfileService.add(this.userProfileDTO).subscribe(res => {
					this.toasterService.success("success");
					this.cancel();
				})
			}
		}
	}

	
	sendUserProfile(): void {
		// send message to subscribers via observable subject
		this.subjectService.sendUserProfile(this.userProfileDTO);
	}
	onFileChange(event: any) {

		const reader = new FileReader();
		if (event.target.files && event.target.files.length) {
			const [file] = event.target.files;
			reader.readAsDataURL(file);

			reader.onload = () => {
				this.imageSrc = reader.result as string;
				this.userProfileDTO.imageBase64 = this.imageSrc;

			};

		}
	}
}
