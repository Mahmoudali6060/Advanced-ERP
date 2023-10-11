import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ConfigService } from 'src/app/shared/services/config.service';
import { NgForm } from '@angular/forms';
import { CategoryDTO } from '../../../models/category.dto';
import { CategoryService } from '../../../services/category.service';

@Component({
	selector: 'app-category-form',
	templateUrl: './category-form.component.html',
	styleUrls: ['./category-form.component.css']
})
export class CategoryFormComponent {

	categoryDTO: CategoryDTO = new CategoryDTO();
	viewMode: boolean;
	constructor(private categoryService: CategoryService,
		private route: ActivatedRoute,
		private toasterService: ToastrService,
		private _configService: ConfigService,
		private router: Router) {
	}

	ngOnInit() {
		this.categoryDTO = new CategoryDTO();
		const id = this.route.snapshot.paramMap.get('id');
		if (id) {
			this.getCategoryById(id);
			if (this.router.url.includes('/view/')) {
				this.viewMode = true;
			}
		}
	}
	getCategoryById(categoryId: any) {
		this.categoryService.getById(categoryId).subscribe((res: any) => {
			this.categoryDTO = res;
		})
	}

	handleChange(event: any) {
		this.categoryDTO.isActive = event.target;
	}

	back() {
		this.router.navigateByUrl('setup/category-list');
	}
	validattion(categoryDTO: CategoryDTO): boolean {
		// if (!categoryDTO.firstName || isNullOrUndefined(categoryDTO.firstName)) {
		// 	this.toasterService.error(this.translate.instant("Errors.FirstNameIsRequired"));
		// 	return false;
		//   }
		//   if (!categoryDTO.lastName || isNullOrUndefined(categoryDTO.lastName)) {
		// 	this.toasterService.error(this.translate.instant("Errors.LastNameIsRequired"));
		// 	return false;
		//   }
		//   if (!categoryDTO.mobile || isNullOrUndefined(categoryDTO.mobile)) {
		// 	this.toasterService.error(this.translate.instant("Errors.MobileIsRequired"));
		// 	return false;
		//   }
		//   if (!categoryDTO.categoryName || isNullOrUndefined(categoryDTO.categoryName)) {
		// 	this.toasterService.error(this.translate.instant("Errors.CategoryNameIsRequired"));
		// 	return false;
		//   }
		//   if (!categoryDTO.email || isNullOrUndefined(categoryDTO.email)) {
		// 	this.toasterService.error(this.translate.instant("Errors.EmailIsRequired"));
		// 	return false;
		//   }
		return true;

	}

	save(form: NgForm) {
		if (this.validattion(this.categoryDTO)) {
			if (this.categoryDTO.id) {
				this.categoryService.update(this.categoryDTO).subscribe(res => {
					this.toasterService.success("success");
					this.back();
				})
			}
			else {
				this.categoryService.add(this.categoryDTO).subscribe(res => {
					this.toasterService.success("success");
					this.back();
				})
			}
		}
	}
}
