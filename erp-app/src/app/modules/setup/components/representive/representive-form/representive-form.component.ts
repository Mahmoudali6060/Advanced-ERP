import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { ConfigService } from 'src/app/shared/services/config.service';
import { NgForm } from '@angular/forms';
import { RepresentiveService } from '../../../services/representive.service';
import { CategoryDTO } from '../../../models/category.dto';
import { CategoryService } from '../../../services/category.service';
import { RepresentiveDTO } from '../../../models/representive.dto';
import { LabelValuePair } from 'src/app/shared/enums/label-value-pair';
import { HelperService } from 'src/app/shared/services/helper.service';
import { RepresentiveTypeEnum } from 'src/app/shared/enums/representive-type.enum';

@Component({
	selector: 'app-representive-form',
	templateUrl: './representive-form.component.html',
	styleUrls: ['./representive-form.component.css']
})
export class RepresentiveFormComponent {

	representiveDTO: RepresentiveDTO = new RepresentiveDTO();
	imageSrc!: string;
	serverUrl: string;
	viewMode: boolean;
	categoryList: Array<CategoryDTO> = new Array<CategoryDTO>();
	representiveTypelist: LabelValuePair[];

	constructor(
		private representiveService: RepresentiveService,
		private categoryService: CategoryService,
		private route: ActivatedRoute,
		private toasterService: ToastrService,
		private _configService: ConfigService,
		private helperService: HelperService,
		private router: Router) {
	}

	ngOnInit() {
		this.representiveTypelist = this.helperService.enumSelector(RepresentiveTypeEnum);
		this.representiveDTO = new RepresentiveDTO();
		const id = this.route.snapshot.paramMap.get('id');
		if (id) {
			this.getRepresentiveById(id);
			if (this.router.url.includes('/view/')) {
				this.viewMode = true;
			}
		}

	}



	getRepresentiveById(representiveId: any) {
		this.representiveService.getById(representiveId).subscribe((res: any) => {
			this.representiveDTO = res;
		})
	}

	handleChange(event: boolean) {
		// this.representiveDTO.isActive = event.target
	}

	back() {
		this.router.navigateByUrl('setup/representive-list');
	}
	validattion(representiveDTO: RepresentiveDTO): boolean {
		// if (!representiveDTO.firstName || isNullOrUndefined(representiveDTO.firstName)) {
		// 	this.toasterService.error(this.translate.instant("Errors.FirstNameIsRequired"));
		// 	return false;
		//   }
		//   if (!representiveDTO.lastName || isNullOrUndefined(representiveDTO.lastName)) {
		// 	this.toasterService.error(this.translate.instant("Errors.LastNameIsRequired"));
		// 	return false;
		//   }
		//   if (!representiveDTO.mobile || isNullOrUndefined(representiveDTO.mobile)) {
		// 	this.toasterService.error(this.translate.instant("Errors.MobileIsRequired"));
		// 	return false;
		//   }
		//   if (!representiveDTO.representiveName || isNullOrUndefined(representiveDTO.representiveName)) {
		// 	this.toasterService.error(this.translate.instant("Errors.RepresentiveNameIsRequired"));
		// 	return false;
		//   }
		//   if (!representiveDTO.email || isNullOrUndefined(representiveDTO.email)) {
		// 	this.toasterService.error(this.translate.instant("Errors.EmailIsRequired"));
		// 	return false;
		//   }
		return true;

	}

	save(form: NgForm) {
		if (this.validattion(this.representiveDTO)) {
			if (this.representiveDTO.id) {
				this.representiveService.update(this.representiveDTO).subscribe(res => {
					this.toasterService.success("success");
					this.back();
				})
			}
			else {
				this.representiveService.add(this.representiveDTO).subscribe(res => {
					this.toasterService.success("success");
					this.back();
				})
			}
		}
	}


}
