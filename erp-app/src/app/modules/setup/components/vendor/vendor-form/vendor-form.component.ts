import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ConfigService } from 'src/app/shared/services/config.service';
import { NgForm } from '@angular/forms';
import { VendorDTO } from '../../../models/vendor.dto';
import { VendorService } from '../../../services/vendor.service';
import { CategoryDTO } from '../../../models/category.dto';
import { CategoryService } from '../../../services/category.service';

@Component({
	selector: 'app-vendor-form',
	templateUrl: './vendor-form.component.html',
	styleUrls: ['./vendor-form.component.css']
})
export class VendorFormComponent {

	vendorDTO: VendorDTO = new VendorDTO();
	imageSrc!: string;
	serverUrl: string;
	viewMode: boolean;
	categoryList: Array<CategoryDTO> = new Array<CategoryDTO>();
	constructor(
		private vendorService: VendorService,
		private categoryService: CategoryService,
		private route: ActivatedRoute,
		private toasterService: ToastrService,
		private _configService: ConfigService,
		private router: Router) {
	}

	ngOnInit() {
		this.imageSrc = "assets/images/icon/avatar-big-01.jpg";
		this.vendorDTO = new VendorDTO();
		const id = this.route.snapshot.paramMap.get('id');
		if (id) {
			this.getVendorById(id);
			if (this.router.url.includes('/view/')) {
				this.viewMode = true;
			}
		}
	}

	getVendorById(vendorId: any) {
		this.vendorService.getById(vendorId).subscribe((res: any) => {
			this.vendorDTO = res;
			this.serverUrl = this._configService.getServerUrl();
			this.imageSrc = this.serverUrl + "wwwroot/Images/Vendors/" + this.vendorDTO.imageUrl;
			if (this.vendorDTO.clientId)
				this.vendorDTO.isClient = true;
			// if (!this.vendorDTO.imageUrl) {
			// 	this.imageSrc = "assets/images/icon/avatar-big-01.jpg";
			// }
		})
	}
	handleChange(event: boolean) {
		// this.vendorDTO.isActive = event.target
	}

	back() {
		this.router.navigateByUrl('setup/vendor-list');
	}
	validattion(vendorDTO: VendorDTO): boolean {
		// if (!vendorDTO.firstName || isNullOrUndefined(vendorDTO.firstName)) {
		// 	this.toasterService.error(this.translate.instant("Errors.FirstNameIsRequired"));
		// 	return false;
		//   }
		//   if (!vendorDTO.lastName || isNullOrUndefined(vendorDTO.lastName)) {
		// 	this.toasterService.error(this.translate.instant("Errors.LastNameIsRequired"));
		// 	return false;
		//   }
		//   if (!vendorDTO.mobile || isNullOrUndefined(vendorDTO.mobile)) {
		// 	this.toasterService.error(this.translate.instant("Errors.MobileIsRequired"));
		// 	return false;
		//   }
		//   if (!vendorDTO.vendorName || isNullOrUndefined(vendorDTO.vendorName)) {
		// 	this.toasterService.error(this.translate.instant("Errors.VendorNameIsRequired"));
		// 	return false;
		//   }
		//   if (!vendorDTO.email || isNullOrUndefined(vendorDTO.email)) {
		// 	this.toasterService.error(this.translate.instant("Errors.EmailIsRequired"));
		// 	return false;
		//   }
		return true;

	}

	save(form: NgForm) {
		if (this.validattion(this.vendorDTO)) {
			if (this.vendorDTO.id) {
				this.vendorService.update(this.vendorDTO).subscribe(res => {
					if (res) {
						this.toasterService.success("success");
						this.back();
					}
				})
			}
			else {
				this.vendorService.add(this.vendorDTO).subscribe(res => {
					if (res) {
						this.toasterService.success("success");
						this.back();
					}
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
				this.vendorDTO.imageBase64 = this.imageSrc;

			};
		}
	}
}
