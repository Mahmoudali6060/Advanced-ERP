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
import { TranslateService } from '@ngx-translate/core';
import { ProductFormPopupComponent } from 'src/app/shared/modules/setup-shared/components/product-form-popup/product-form-popup.component';
import { DialogService } from 'src/app/shared/services/confirmation-dialog.service';
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
	purchaseHeaderId: any;
	searchProduct: any;
	itemList = ['carrot', 'banana', 'apple', 'potato', 'tomato', 'cabbage', 'turnip', 'okra', 'onion', 'cherries', 'plum', 'mango'];
	constructor(
		private purchasesBillService: PurchasesBillService,
		private productService: ProductService,
		private vendorService: VendorService,
		private route: ActivatedRoute,
		private toasterService: ToastrService,
		private router: Router,
		private helperService: HelperService,
		private translate: TranslateService,
		private dialogService: DialogService) {
	}

	ngOnInit() {
		this.imageSrc = "assets/images/icon/avatar-big-01.jpg";
		this.purchaseHeaderId = this.route.snapshot.paramMap.get('id');
		if (this.purchaseHeaderId) {
			this.getPurchasesBillById(this.purchaseHeaderId);
			if (this.router.url.includes('/view/')) {
				this.viewMode = true;
			}
		}
		else {
			this.purchasesBillHeaderDTO.vendorId = null;
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
			this.setPurchaseDetailsDefaultData();
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
			this.setPurchaseDetailsDefaultData();
		});
	}

	setPurchaseDetailsDefaultData() {
		if (this.purchasesBillHeaderDTO.purchasesBillDetailList) {
			let index = 0;
			for (let item of this.purchasesBillHeaderDTO.purchasesBillDetailList) {
				item.index = index;
				index++;
				this.onProductChange(item);
			}
		}
	}

	back() {
		this.router.navigateByUrl('purchases-bill/purchases-bill-list');
	}
	validation(purchasesBillDTO: PurchasesBillHeaderDTO): boolean {
		if (!purchasesBillDTO.purchasesBillDetailList || this.purchasesBillHeaderDTO.purchasesBillDetailList.length == 0) {
			this.toasterService.error(this.translate.instant("Errors.YouMustSelectProducts"));
			return false;
		}

		return true;

	}

	save(form: NgForm) {
		if (this.validation(this.purchasesBillHeaderDTO)) {
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

	public deleteRow(event: any, item: PurchasesBillDetailsDTO) {
		this.purchasesBillHeaderDTO.purchasesBillDetailList = this.purchasesBillHeaderDTO.purchasesBillDetailList.filter(x => x.index != item.index);
		this.updateTotal();
	}

	addNewRow() {
		let lastElement = this.purchasesBillHeaderDTO.purchasesBillDetailList[this.purchasesBillHeaderDTO.purchasesBillDetailList.length - 1];
		let purchasesBillDetails: PurchasesBillDetailsDTO = new PurchasesBillDetailsDTO();
		purchasesBillDetails.index = lastElement ? lastElement.index + 1 : 0;
		this.purchasesBillHeaderDTO.purchasesBillDetailList.push(purchasesBillDetails);
	}

	onProductChange(item: PurchasesBillDetailsDTO) {
		let product = this.productList.find(x => x.id == item.productId);
		if (product) {
			if (!item.price) item.price = product.price;
			if (!item.discount) item.discount = product.purchasingPricePercentage;
			item.actualQuantity = product.actualQuantity;
			this.updateTotal();
		}
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

	showProductFormPopUp(item: PurchasesBillDetailsDTO) {
		this.dialogService.show("sm", ProductFormPopupComponent)
			.then((product) => {
				if (product) {
					item.productId = product.id;
					this.getAllProducts();
				}
			})
			.catch(() => console.log('SalesBill dismissed the dialog (e.g., by using ESC, clicking the cross icon, or clicking outside the dialog)'));
	}

}
