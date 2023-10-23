import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ConfigService } from 'src/app/shared/services/config.service';
import { NgForm } from '@angular/forms';
import { CategoryDTO } from '../../../models/category.dto';
import { CategoryService } from '../../../services/category.service';
import { ClientVendorService } from '../../../services/client-vendor.service';
import { ClientVendorDTO, ClientVendorTypeEnum } from '../../../models/client-vendor.dto';

@Component({
	selector: 'app-vendor-form',
	templateUrl: './vendor-form.component.html',
	styleUrls: ['./vendor-form.component.css']
})
export class VendorFormComponent {

	vendorDTO: ClientVendorDTO = new ClientVendorDTO();
	imageSrc!: string;
	serverUrl: string;
	viewMode: boolean;
	categoryList: Array<CategoryDTO> = new Array<CategoryDTO>();
	isClient: boolean = false;
	constructor(
		private clientVendorService: ClientVendorService,
		private categoryService: CategoryService,
		private route: ActivatedRoute,
		private toasterService: ToastrService,
		private _configService: ConfigService,
		private router: Router) {
	}

	ngOnInit() {
		this.imageSrc = "assets/images/icon/avatar-big-01.jpg";
		this.vendorDTO = new ClientVendorDTO();
		const id = this.route.snapshot.paramMap.get('id');
		if (id) {
			this.getVendorById(id);
			if (this.router.url.includes('/view/')) {
				this.viewMode = true;
			}
		}
	}

	getVendorById(vendorId: any) {
		this.clientVendorService.getById(vendorId).subscribe((res: any) => {
			this.vendorDTO = res;
			this.serverUrl = this._configService.getServerUrl();
			this.imageSrc = this.serverUrl + "wwwroot/Images/ClientVendors/" + this.vendorDTO.imageUrl;
			if (this.vendorDTO.typeId == ClientVendorTypeEnum.All)
				this.isClient = true;
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
	validattion(vendorDTO: ClientVendorDTO): boolean {
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
		if (this.isClient)
			this.vendorDTO.typeId = ClientVendorTypeEnum.All
		else
			this.vendorDTO.typeId = ClientVendorTypeEnum.Vendor

		if (this.validattion(this.vendorDTO)) {
			if (this.vendorDTO.id) {
				this.clientVendorService.update(this.vendorDTO).subscribe(res => {
					if (res) {
						this.toasterService.success("success");
						this.back();
					}
				})
			}
			else {
				this.clientVendorService.add(this.vendorDTO).subscribe(res => {
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
