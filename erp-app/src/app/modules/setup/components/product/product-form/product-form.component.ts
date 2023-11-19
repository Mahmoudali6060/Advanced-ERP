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
import { ProductDTO } from '../../../models/product.dto';
import { ProductService } from '../../../services/product.service';
import { CategoryDTO } from '../../../models/category.dto';
import { CategoryService } from '../../../services/category.service';

@Component({
	selector: 'app-product-form',
	templateUrl: './product-form.component.html',
	styleUrls: ['./product-form.component.css']
})
export class ProductFormComponent {

	productDTO: ProductDTO = new ProductDTO();
	imageSrc!: string;
	serverUrl: string;
	viewMode: boolean;
	categoryList: Array<CategoryDTO> = new Array<CategoryDTO>();
	constructor(
		private productService: ProductService,
		private categoryService: CategoryService,
		private route: ActivatedRoute,
		private toasterService: ToastrService,
		private _configService: ConfigService,
		private router: Router) {
	}

	ngOnInit() {
		this.imageSrc = "assets/images/icon/avatar-big-01.jpg";
		this.productDTO = new ProductDTO();
		const id = this.route.snapshot.paramMap.get('id');
		if (id) {
			this.getProductById(id);
			if (this.router.url.includes('/view/')) {
				this.viewMode = true;
			}
		}
		else {
			this.productDTO.actualQuantity = 0;
		}
		this.getAllCategories();
	}

	getAllCategories() {
		this.categoryService.getAllLite().subscribe((res: any) => {
			this.categoryList = res.list;
		})
	}

	getProductById(productId: any) {
		this.productService.getById(productId).subscribe((res: any) => {
			this.productDTO = res;
			this.serverUrl = this._configService.getServerUrl();
			this.imageSrc = this.serverUrl + "wwwroot/Images/Products/" + this.productDTO.imageUrl;
			// if (!this.productDTO.imageUrl) {
			// 	this.imageSrc = "assets/images/icon/avatar-big-01.jpg";
			// }
		})
	}
	handleChange(event: boolean) {
		// this.productDTO.isActive = event.target
	}

	back() {
		this.router.navigateByUrl('setup/product-list-view');
	}
	validattion(productDTO: ProductDTO): boolean {
		// if (!productDTO.firstName || isNullOrUndefined(productDTO.firstName)) {
		// 	this.toasterService.error(this.translate.instant("Errors.FirstNameIsRequired"));
		// 	return false;
		//   }
		//   if (!productDTO.lastName || isNullOrUndefined(productDTO.lastName)) {
		// 	this.toasterService.error(this.translate.instant("Errors.LastNameIsRequired"));
		// 	return false;
		//   }
		//   if (!productDTO.mobile || isNullOrUndefined(productDTO.mobile)) {
		// 	this.toasterService.error(this.translate.instant("Errors.MobileIsRequired"));
		// 	return false;
		//   }
		//   if (!productDTO.productName || isNullOrUndefined(productDTO.productName)) {
		// 	this.toasterService.error(this.translate.instant("Errors.ProductNameIsRequired"));
		// 	return false;
		//   }
		//   if (!productDTO.email || isNullOrUndefined(productDTO.email)) {
		// 	this.toasterService.error(this.translate.instant("Errors.EmailIsRequired"));
		// 	return false;
		//   }
		return true;

	}

	save(form: NgForm) {
		if (this.validattion(this.productDTO)) {
			if (this.productDTO.id) {
				this.productService.update(this.productDTO).subscribe(res => {
					this.toasterService.success("success");
					this.back();
				})
			}
			else {
				this.productService.add(this.productDTO).subscribe(res => {
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
				this.productDTO.imageBase64 = this.imageSrc;

			};
		}
	}
}
