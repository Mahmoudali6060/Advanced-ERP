import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { ConfigService } from 'src/app/shared/services/config.service';
import { NgForm } from '@angular/forms';
import { CategoryDTO } from '../../../models/category.dto';
import { CategoryService } from '../../../services/category.service';
import { LabelValuePair } from 'src/app/shared/enums/label-value-pair';
import { HelperService } from 'src/app/shared/services/helper.service';
import { UnitOfMeasurementDTO } from '../../../models/unit-of-measurement.dto';
import { UnitOfMeasurementService } from '../../../services/unit-of-measurement.service';

@Component({
	selector: 'app-unit-of-measurement-form',
	templateUrl: './unit-of-measurement-form.component.html',
	styleUrls: ['./unit-of-measurement-form.component.css']
})
export class UnitOfMeasurementFormComponent {

	unitOfMeasurementDTO: UnitOfMeasurementDTO = new UnitOfMeasurementDTO();
	imageSrc!: string;
	serverUrl: string;
	viewMode: boolean;

	constructor(
		private unitOfMeasurementService: UnitOfMeasurementService,
		private categoryService: CategoryService,
		private route: ActivatedRoute,
		private toasterService: ToastrService,
		private _configService: ConfigService,
		private helperService: HelperService,
		private router: Router) {
	}

	ngOnInit() {
		this.unitOfMeasurementDTO = new UnitOfMeasurementDTO();
		const id = this.route.snapshot.paramMap.get('id');
		if (id) {
			this.getUnitOfMeasurementById(id);
			if (this.router.url.includes('/view/')) {
				this.viewMode = true;
			}
		}

	}



	getUnitOfMeasurementById(unitOfMeasurementId: any) {
		this.unitOfMeasurementService.getById(unitOfMeasurementId).subscribe((res: any) => {
			this.unitOfMeasurementDTO = res;
		})
	}

	handleChange(event: boolean) {
		// this.unitOfMeasurementDTO.isActive = event.target
	}

	back() {
		this.router.navigateByUrl('setup/unit-of-measurement-list');
	}
	validattion(unitOfMeasurementDTO: UnitOfMeasurementDTO): boolean {
		// if (!unitOfMeasurementDTO.firstName || isNullOrUndefined(unitOfMeasurementDTO.firstName)) {
		// 	this.toasterService.error(this.translate.instant("Errors.FirstNameIsRequired"));
		// 	return false;
		//   }
		//   if (!unitOfMeasurementDTO.lastName || isNullOrUndefined(unitOfMeasurementDTO.lastName)) {
		// 	this.toasterService.error(this.translate.instant("Errors.LastNameIsRequired"));
		// 	return false;
		//   }
		//   if (!unitOfMeasurementDTO.mobile || isNullOrUndefined(unitOfMeasurementDTO.mobile)) {
		// 	this.toasterService.error(this.translate.instant("Errors.MobileIsRequired"));
		// 	return false;
		//   }
		//   if (!unitOfMeasurementDTO.unitOfMeasurementName || isNullOrUndefined(unitOfMeasurementDTO.unitOfMeasurementName)) {
		// 	this.toasterService.error(this.translate.instant("Errors.UnitOfMeasurementNameIsRequired"));
		// 	return false;
		//   }
		//   if (!unitOfMeasurementDTO.email || isNullOrUndefined(unitOfMeasurementDTO.email)) {
		// 	this.toasterService.error(this.translate.instant("Errors.EmailIsRequired"));
		// 	return false;
		//   }
		return true;

	}

	save(form: NgForm) {
		if (this.validattion(this.unitOfMeasurementDTO)) {
			if (this.unitOfMeasurementDTO.id) {
				this.unitOfMeasurementService.update(this.unitOfMeasurementDTO).subscribe(res => {
					this.toasterService.success("success");
					this.back();
				})
			}
			else {
				this.unitOfMeasurementService.add(this.unitOfMeasurementDTO).subscribe(res => {
					this.toasterService.success("success");
					this.back();
				})
			}
		}
	}


}
