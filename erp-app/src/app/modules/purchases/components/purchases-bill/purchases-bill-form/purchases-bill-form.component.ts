import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ConfigService } from 'src/app/shared/services/config.service';
import { NgForm } from '@angular/forms';
import { ProductDTO } from 'src/app/modules/setup/models/product.dto';
import { PurchasesBillService } from '../../../services/purchases-bill.service';
import { ProductService } from 'src/app/modules/setup/services/product.service';
import { PurchasesBillHeaderDTO } from '../../../models/purchases-bill-header.dto';
import { VendorDTO } from 'src/app/modules/setup/models/vendor.dto';
import { VendorService } from 'src/app/modules/setup/services/vendor.service';
import { PurchasesBillDetailsDTO } from '../../../models/purchases-bill-details.dto';
import { Pipe, PipeTransform } from '@angular/core';
import { DatePipe } from '@angular/common';
import { HelperService } from 'src/app/shared/services/helper.service';
@Component({
	selector: 'app-purchases-bill-form',
	templateUrl: './purchases-bill-form.component.html',
	styleUrls: ['./purchases-bill-form.component.css']
})
export class PurchasesBillFormComponent {

	purchasesBillHeaderDTO: PurchasesBillHeaderDTO = new PurchasesBillHeaderDTO();
	imageSrc!: string;
	serverUrl: string;
	viewMode: boolean;
	productList: Array<ProductDTO> = new Array<ProductDTO>();
	vendorList: Array<VendorDTO> = new Array<VendorDTO>();

	constructor(
		private purchasesBillService: PurchasesBillService,
		private productService: ProductService,
		private vendorService: VendorService,
		private route: ActivatedRoute,
		private toasterService: ToastrService,
		private _configService: ConfigService,
		private router: Router,
		private helperService: HelperService) {
	}

	ngOnInit() {
		this.imageSrc = "assets/images/icon/avatar-big-01.jpg";
		const id = this.route.snapshot.paramMap.get('id');
		if (id) {
			this.getPurchasesBillById(id);
			if (this.router.url.includes('/view/')) {
				this.viewMode = true;
			}
		}
		else {
			this.addNewRow();
			//set today by default>>Insert Mode
			this.purchasesBillHeaderDTO.date = this.helperService.conveertDateToString(new Date());
		}
		this.getAllProducts();
		this.getAllVendors();
	}

	getAllProducts() {
		this.productService.getAllLite().subscribe((res: any) => {
			this.productList = res.list;
		})
	}

	getAllVendors() {
		this.vendorService.getAllLite().subscribe((res: any) => {
			this.vendorList = res.list;
		})
	}

	getPurchasesBillById(purchasesBillId: any) {
		this.purchasesBillService.getById(purchasesBillId).subscribe((res: any) => {
			this.purchasesBillHeaderDTO = res;
			if (this.purchasesBillHeaderDTO.purchasesBillDetailList)
				this.setInitialIndex();
		})
	}

	setInitialIndex() {
		let index = 0;
		for (let item of this.purchasesBillHeaderDTO.purchasesBillDetailList) {
			item.index = index;
			index++;
		}
	}

	back() {
		this.router.navigateByUrl('purchases-bill/purchases-bill-list');
	}
	validattion(purchasesBillDTO: PurchasesBillHeaderDTO): boolean {
		// if (!purchasesBillDTO.firstName || isNullOrUndefined(purchasesBillDTO.firstName)) {
		// 	this.toasterService.error(this.translate.instant("Errors.FirstNameIsRequired"));
		// 	return false;
		//   }
		//   if (!purchasesBillDTO.lastName || isNullOrUndefined(purchasesBillDTO.lastName)) {
		// 	this.toasterService.error(this.translate.instant("Errors.LastNameIsRequired"));
		// 	return false;
		//   }
		//   if (!purchasesBillDTO.mobile || isNullOrUndefined(purchasesBillDTO.mobile)) {
		// 	this.toasterService.error(this.translate.instant("Errors.MobileIsRequired"));
		// 	return false;
		//   }
		//   if (!purchasesBillDTO.purchasesBillName || isNullOrUndefined(purchasesBillDTO.purchasesBillName)) {
		// 	this.toasterService.error(this.translate.instant("Errors.PurchasesBillNameIsRequired"));
		// 	return false;
		//   }
		//   if (!purchasesBillDTO.email || isNullOrUndefined(purchasesBillDTO.email)) {
		// 	this.toasterService.error(this.translate.instant("Errors.EmailIsRequired"));
		// 	return false;
		//   }
		return true;

	}

	save(form: NgForm) {
		if (this.validattion(this.purchasesBillHeaderDTO)) {
			if (this.purchasesBillHeaderDTO.id) {
				this.purchasesBillService.update(this.purchasesBillHeaderDTO).subscribe(res => {
					this.toasterService.success("success");
					this.back();
				})
			}
			else {
				this.purchasesBillService.add(this.purchasesBillHeaderDTO).subscribe(res => {
					this.toasterService.success("success");
					this.back();
				})
			}
		}
	}

	public deleteRow(item: PurchasesBillDetailsDTO) {
		this.purchasesBillHeaderDTO.purchasesBillDetailList = this.purchasesBillHeaderDTO.purchasesBillDetailList.filter(x => x.index != item.index);
		this.updateTotal();
	}

	addNewRow() {
		let lastElement = this.purchasesBillHeaderDTO.purchasesBillDetailList[this.purchasesBillHeaderDTO.purchasesBillDetailList.length - 1];
		let purchasesBillDetails: PurchasesBillDetailsDTO = new PurchasesBillDetailsDTO();
		purchasesBillDetails.index = lastElement ? lastElement.index + 1 : 0;
		this.purchasesBillHeaderDTO.purchasesBillDetailList.push(purchasesBillDetails);
	}

	updateTotal() {
		this.purchasesBillHeaderDTO.total = 0;
		for (let item of this.purchasesBillHeaderDTO.purchasesBillDetailList) {
			item.priceAfterDiscount = item.price - (item.discount / 100) * item.price;
			item.subTotal = item.priceAfterDiscount * item.quantity;
			this.purchasesBillHeaderDTO.total += item.subTotal;
		}
		this.purchasesBillHeaderDTO.totalDiscount = this.purchasesBillHeaderDTO.transfer + this.purchasesBillHeaderDTO.discount;
		this.purchasesBillHeaderDTO.totalAfterDiscount = this.purchasesBillHeaderDTO.total - this.purchasesBillHeaderDTO.totalDiscount;
	}

}
