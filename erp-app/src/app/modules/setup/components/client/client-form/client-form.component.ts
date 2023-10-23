import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ConfigService } from 'src/app/shared/services/config.service';
import { NgForm } from '@angular/forms';
import { ClientVendorDTO, ClientVendorTypeEnum } from '../../../models/client-vendor.dto';
import { CategoryDTO } from '../../../models/category.dto';
import { CategoryService } from '../../../services/category.service';
import { ClientVendorService } from '../../../services/client-vendor.service';

@Component({
	selector: 'app-client-form',
	templateUrl: './client-form.component.html',
	styleUrls: ['./client-form.component.css']
})
export class ClientFormComponent {

	clientDTO: ClientVendorDTO = new ClientVendorDTO();
	imageSrc!: string;
	serverUrl: string;
	viewMode: boolean;
	categoryList: Array<CategoryDTO> = new Array<CategoryDTO>();
	isVendor: boolean = false;
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
		this.clientDTO = new ClientVendorDTO();
		const id = this.route.snapshot.paramMap.get('id');
		if (id) {
			this.getClientById(id);
			if (this.router.url.includes('/view/')) {
				this.viewMode = true;
			}
		}
	}

	getClientById(clientId: any) {
		this.clientVendorService.getById(clientId).subscribe((res: any) => {
			this.clientDTO = res;
			this.serverUrl = this._configService.getServerUrl();
			this.imageSrc = this.serverUrl + "wwwroot/Images/ClientVendors/" + this.clientDTO.imageUrl;
			if (this.clientDTO.typeId == ClientVendorTypeEnum.All)
				this.isVendor = true;
			// if (!this.clientDTO.imageUrl) {
			// 	this.imageSrc = "assets/images/icon/avatar-big-01.jpg";
			// }
		})
	}
	handleChange(event: boolean) {
		// this.clientDTO.isActive = event.target
	}

	back() {
		this.router.navigateByUrl('setup/client-list');
	}
	validattion(clientDTO: ClientVendorDTO): boolean {
		// if (!clientDTO.firstName || isNullOrUndefined(clientDTO.firstName)) {
		// 	this.toasterService.error(this.translate.instant("Errors.FirstNameIsRequired"));
		// 	return false;
		//   }
		//   if (!clientDTO.lastName || isNullOrUndefined(clientDTO.lastName)) {
		// 	this.toasterService.error(this.translate.instant("Errors.LastNameIsRequired"));
		// 	return false;
		//   }
		//   if (!clientDTO.mobile || isNullOrUndefined(clientDTO.mobile)) {
		// 	this.toasterService.error(this.translate.instant("Errors.MobileIsRequired"));
		// 	return false;
		//   }
		//   if (!clientDTO.clientName || isNullOrUndefined(clientDTO.clientName)) {
		// 	this.toasterService.error(this.translate.instant("Errors.ClientNameIsRequired"));
		// 	return false;
		//   }
		//   if (!clientDTO.email || isNullOrUndefined(clientDTO.email)) {
		// 	this.toasterService.error(this.translate.instant("Errors.EmailIsRequired"));
		// 	return false;
		//   }
		return true;

	}

	save(form: NgForm) {
		if (this.isVendor)
			this.clientDTO.typeId = ClientVendorTypeEnum.All
		else
			this.clientDTO.typeId = ClientVendorTypeEnum.Client

		if (this.validattion(this.clientDTO)) {
			if (this.clientDTO.id) {
				this.clientVendorService.update(this.clientDTO).subscribe(res => {
					if (res) {
						this.toasterService.success("success");
						this.back();
					}
				})
			}
			else {
				this.clientVendorService.add(this.clientDTO).subscribe(res => {
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
				this.clientDTO.imageBase64 = this.imageSrc;

			};
		}
	}
}
