import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { Location } from '@angular/common';
import { ConfigService } from 'src/app/shared/services/config.service';
import { NgForm } from '@angular/forms';
import { HelperService } from 'src/app/shared/services/helper.service';
import { SubjectService } from 'src/app/shared/services/subject.service';
import { LocalStorageService } from 'src/app/shared/services/local-storage.service';
import { CompanyService } from '../../../services/company.service';
import { CategoryDTO } from '../../../models/category.dto';
import { CategoryService } from '../../../services/category.service';
import { CompanyDTO } from '../../../models/company-dto';
import { SettingDTO } from '../../../models/settings-dto';
import { AuthService } from 'src/app/modules/authentication/services/auth.service';

@Component({
	selector: 'app-company-form',
	templateUrl: './company-form.component.html',
	styleUrls: ['./company-form.component.css']
})
export class CompanyFormComponent {

	companyDTO: CompanyDTO = new CompanyDTO();
	imageSrc!: string;
	serverUrl: string;
	viewMode: boolean;
	categoryList: Array<CategoryDTO> = new Array<CategoryDTO>();
	salesBillInstructions: string;
	purchasesBillInstructions: string;
	constructor(
		private companyService: CompanyService,
		private categoryService: CategoryService,
		private route: ActivatedRoute,
		private toasterService: ToastrService,
		private _configService: ConfigService,
		private location: Location,
		private router: Router,
		private authService: AuthService) {
	}

	ngOnInit() {
		this.imageSrc = "assets/images/icon/avatar-big-01.jpg";
		this.companyDTO.settingDTO = new SettingDTO();

		console.log(this.companyDTO);// = new CompanyDTO();
		const id = this.route.snapshot.paramMap.get('id');
		if (id) {
			this.getCompanyById(id);
			if (this.router.url.includes('/view/')) {
				this.viewMode = true;
			}
		}

	}



	getCompanyById(companyId: any) {
		this.companyService.getById(companyId).subscribe((res: any) => {
			this.companyDTO = res;
			this.authService.loggedUserProfile.companyDTO = this.companyDTO;
			this.authService.updateLoggedUserProfile(this.authService.loggedUserProfile)
			this.salesBillInstructions = this.companyDTO.settingDTO?.salesBillInstructions;
			this.purchasesBillInstructions = this.companyDTO.settingDTO?.purchasesBillInstructions;
			this.serverUrl = this._configService.getServerUrl();
			this.imageSrc = this.serverUrl + "wwwroot/Images/Companies/" + this.companyDTO.imageUrl;
		})
	}
	handleChange(event: boolean) {
		// this.companyDTO.isActive = event.target
	}

	back() {
		// this.location.back();
		this.router.navigateByUrl('dashboard');
	}
	validattion(companyDTO: CompanyDTO): boolean {
		// if (!companyDTO.firstName || isNullOrUndefined(companyDTO.firstName)) {
		// 	this.toasterService.error(this.translate.instant("Errors.FirstNameIsRequired"));
		// 	return false;
		//   }
		//   if (!companyDTO.lastName || isNullOrUndefined(companyDTO.lastName)) {
		// 	this.toasterService.error(this.translate.instant("Errors.LastNameIsRequired"));
		// 	return false;
		//   }
		//   if (!companyDTO.mobile || isNullOrUndefined(companyDTO.mobile)) {
		// 	this.toasterService.error(this.translate.instant("Errors.MobileIsRequired"));
		// 	return false;
		//   }
		//   if (!companyDTO.companyName || isNullOrUndefined(companyDTO.companyName)) {
		// 	this.toasterService.error(this.translate.instant("Errors.CompanyNameIsRequired"));
		// 	return false;
		//   }
		//   if (!companyDTO.email || isNullOrUndefined(companyDTO.email)) {
		// 	this.toasterService.error(this.translate.instant("Errors.EmailIsRequired"));
		// 	return false;
		//   }
		return true;

	}

	save(form: NgForm) {
		if (!this.companyDTO.settingDTO) this.companyDTO.settingDTO = new SettingDTO();
		this.companyDTO.settingDTO.companyId = this.companyDTO.id;
		this.companyDTO.settingDTO.purchasesBillInstructions = this.purchasesBillInstructions;
		this.companyDTO.settingDTO.salesBillInstructions = this.salesBillInstructions;

		if (this.validattion(this.companyDTO)) {
			if (this.companyDTO.id) {
				this.companyService.update(this.companyDTO).subscribe(res => {
					this.toasterService.success("success");
					this.getCompanyById(res);
					this.back();
				})
			}
			else {
				this.companyService.add(this.companyDTO).subscribe(res => {
					this.toasterService.success("success");
					this.back();
				})
			}
		}
	}


	onFileChange(event: any) {
		const reader = new FileReader();
		if (event.target.files && event.target.files.length) {
			const [file] = event.target.files;
			reader.readAsDataURL(file);

			reader.onload = () => {
				this.imageSrc = reader.result as string;
				this.companyDTO.imageBase64 = this.imageSrc;
			};
		}
	}
}
