import { Component, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ConfigService } from 'src/app/shared/services/config.service';
import { NgForm } from '@angular/forms';
import { ProductDTO } from 'src/app/modules/setup/models/product.dto';
import { ProductService } from 'src/app/modules/setup/services/product.service';
import { Pipe, PipeTransform } from '@angular/core';
import { DatePipe } from '@angular/common';
import { HelperService } from 'src/app/shared/services/helper.service';
import { TranslateService } from '@ngx-translate/core';
import { ProductFormPopupComponent } from 'src/app/shared/modules/setup-shared/components/product-form-popup/product-form-popup.component';
import { DialogService } from 'src/app/shared/services/confirmation-dialog.service';
import { ClientVendorService } from 'src/app/modules/setup/services/client-vendor.service';
import { ClientVendorDTO, ClientVendorTypeEnum } from 'src/app/modules/setup/models/client-vendor.dto';
import { AlertService } from 'src/app/shared/services/alert.service';
import { ClientVendorFormPopupComponent } from 'src/app/shared/modules/setup-shared/components/client-vendor-form-popup/client-vendor-form-popup.component';
import { AuthService } from 'src/app/modules/authentication/services/auth.service';
import { ReportService } from 'src/app/modules/report/services/report.service';
import { RepresentiveDTO } from 'src/app/modules/setup/models/representive.dto';
import { RepresentiveService } from 'src/app/modules/setup/services/representive.service';
import { RepresentiveFormPopupComponent } from 'src/app/shared/modules/setup-shared/components/representive-form-popup/representive-form-popup.component';
import { RepresentiveTypeEnum } from 'src/app/shared/enums/representive-type.enum';
import { PurchasesBillHeaderDTO } from '../../models/purchases-bill-header.dto';
import { PurchasesBillService } from '../../services/purchases-bill.service';
import { PurchasesBillDetailsDTO } from '../../models/purchases-bill-details.dto';
import { PaymentMethodEnum } from 'src/app/shared/enums/payment-method.enum';
import { LabelValuePair } from 'src/app/shared/enums/label-value-pair';
@Component({
	selector: 'app-purchases-bill-form',
	templateUrl: './purchases-bill-form.component.html',
	styleUrls: ['./purchases-bill-form.component.css']
})
export class PurchasesBillFormComponent {

	purchasesBillHeaderDTO: PurchasesBillHeaderDTO = new PurchasesBillHeaderDTO();
	imageSrc!: string;
	viewMode: boolean;
	productList: Array<ProductDTO> = new Array<ProductDTO>();
	vendorList: Array<ClientVendorDTO> = new Array<ClientVendorDTO>();
	representiveList: Array<RepresentiveDTO> = new Array<RepresentiveDTO>();
	purchaseHeaderId: any;
	searchProduct: any;
	previousBalance: number = 0;
	@Input() searchByNumber: boolean = false;
	selectedVendor: ClientVendorDTO = new ClientVendorDTO();
	vatPercentage: number = 0;
	numberOfProducts: number = 1;
	@Input() isTemp: boolean = false;
	isTransfereToBill: boolean = false;
	@Input() isReturned: boolean = false;
	isNewReturn: boolean = false;
	@Input() purchasesHeaderId: number = 0;
	hideBillnumber: boolean = false;
	tempPurchasesBillDetailList: PurchasesBillDetailsDTO[];
	paymentMethodList: LabelValuePair[];
	disableButton: boolean = false;

	constructor(
		private purchasesBillService: PurchasesBillService,
		private productService: ProductService,
		private clientVendorService: ClientVendorService,
		private route: ActivatedRoute,
		private toasterService: ToastrService,
		private router: Router,
		public helperService: HelperService,
		private translate: TranslateService,
		private dialogService: DialogService,
		private alertService: AlertService,
		public authService: AuthService,
		private reportService: ReportService,
		private representiveService: RepresentiveService) {
	}

	ngOnInit() {
		this.purchasesBillHeaderDTO.isTemp = this.isTemp;
		this.purchasesBillHeaderDTO.isReturned = this.isReturned;
		this.paymentMethodList = this.helperService.enumSelector(PaymentMethodEnum);


		let purchasesHeaderId = this.route.snapshot.paramMap.get('id');
		if (purchasesHeaderId || (this.purchasesBillHeaderDTO.isReturned && this.purchasesHeaderId)) {
			this.getPurchasesBillById(purchasesHeaderId);
		}
		else {
			this.purchasesBillHeaderDTO.clientVendorId = null;
			this.addNewRow();
			//set today by default>>Insert Mode
			this.purchasesBillHeaderDTO.date = this.helperService.conveertDateToString(new Date());
			this.getAllProducts();
			this.getAllVendors();
			this.getAllRepresentives();
		}

		if (this.router.url.includes('view')) {
			this.viewMode = true;
		}
		if (this.router.url.includes('purchases-bill-new-returned-form')) {
			this.isNewReturn = true;
		}
		if (purchasesHeaderId == null && (this.router.url.includes('purchases-bill-form') || this.router.url.includes('purchases-bill-temp-form'))) {
			this.hideBillnumber = true;
		}

	}

	getAllProducts() {
		this.productService.getAllLite().subscribe((res: any) => {
			this.productList = res.list;
			if (this.isNewReturn && this.purchasesBillHeaderDTO.number) {
				let tempProductList: Array<ProductDTO> = new Array<ProductDTO>();
				for (let item of this.purchasesBillHeaderDTO.purchasesBillDetailList) {
					let product: any = this.productList.find(x => x.id == item.productId);
					tempProductList.push(product);
				}
				this.productList = tempProductList;
				this.tempPurchasesBillDetailList = this.purchasesBillHeaderDTO.purchasesBillDetailList;
				this.purchasesBillHeaderDTO.purchasesBillDetailList = [];
				this.addNewRow();

			}
			this.setPurchaseDetailsDefaultData();
		})
	}

	getAllVendors() {
		this.clientVendorService.getAllLiteByTypeId(ClientVendorTypeEnum.Vendor).subscribe((res: any) => {
			this.vendorList = res.list;
			this.onVendorChange()
		})
	}

	getAllRepresentives() {
		this.representiveService.getAllLite().subscribe((res: any) => {
			this.representiveList = res.list;
		})
	}

	getPurchasesBillById(purchasesBillId: any, isPrint?: boolean) {
		this.purchasesBillService.getById(purchasesBillId).subscribe((res: any) => {
			this.purchasesBillHeaderDTO = res;
			if (isPrint) {
				this.print();
				this.back();
			}
			else {
				this.getAllProducts();
				this.getAllVendors();
				this.getAllRepresentives();
				this.isTaxChange();
				this.purchasesBillHeaderDTO.isReturned = this.isReturned;
			}


		});
	}

	getPurchaseByNumber() {
		this.purchasesBillService.getByNumber(this.purchasesBillHeaderDTO.number).subscribe((res: any) => {
			if (!res) {
				this.purchasesBillHeaderDTO = new PurchasesBillHeaderDTO();
				this.previousBalance = 0;
				this.alertService.showError(this.translate.instant("Errors.NotFound"), this.translate.instant("Errors.Error"));
				return;
			}
			this.purchasesBillHeaderDTO = res;
			if (this.isReturned) {
				this.purchasesBillHeaderDTO.id = null;
				this.purchasesBillHeaderDTO.isReturned = true;
			}
			this.getAllProducts();
			this.getAllVendors();
		});
	}

	setPurchaseDetailsDefaultData() {
		if (this.purchasesBillHeaderDTO.purchasesBillDetailList) {
			let index = 0;
			for (let item of this.purchasesBillHeaderDTO.purchasesBillDetailList) {
				item.index = index;
				index++;
				this.setProductToPurchase(item, false);
			}
		}
	}

	back() {
		this.purchasesBillHeaderDTO = new PurchasesBillHeaderDTO();

		if (this.router.url.includes('purchases-bill-temp-form'))
			this.router.navigateByUrl('purchases-bill/purchases-bill-temp-list');
		else if (this.router.url.includes('purchases-bill-new-returned-form') || this.router.url.includes('purchases-bill-returned-form'))
			this.router.navigateByUrl('purchases-bill/purchases-bill-returned-list');
		else if (this.router.url.includes('purchases-bill-form'))
			this.router.navigateByUrl('purchases-bill/purchases-bill-list');

	}

	validation(purchasesBillDTO: PurchasesBillHeaderDTO): boolean {
		if (!purchasesBillDTO.clientVendorId) {
			this.toasterService.error(this.translate.instant("Errors.YouMustSelectVendor"));
			return false;
		}
		if (!purchasesBillDTO.purchasesBillDetailList || this.purchasesBillHeaderDTO.purchasesBillDetailList.length == 0) {
			this.toasterService.error(this.translate.instant("Errors.YouMustSelectProducts"));
			return false;
		}

		if (purchasesBillDTO.purchasesBillDetailList?.length > 0) {
			for (let item of this.purchasesBillHeaderDTO.purchasesBillDetailList) {
				if (!item.productId) {
					this.toasterService.error(this.translate.instant("Errors.YouMustSelectProducts"));
					return false;
				}
			}
		}

		if (purchasesBillDTO.paid == null) {
			this.toasterService.error(this.translate.instant("Errors.YouMustSetPaidAmount"));
			return false;
		}

		return true;

	}

	save(isPrint: boolean, form?: NgForm) {
		if (this.isTransfereToBill == true)
			this.purchasesBillHeaderDTO.isTemp = false;

		if (this.isNewReturn == true) {
			this.purchasesHeaderId = 0;
			this.purchasesBillHeaderDTO.isNewReturned = true;
		}

		if (this.purchasesBillHeaderDTO.isReturned) {
			this.purchasesBillHeaderDTO.id = this.purchasesHeaderId;
			this.purchasesBillHeaderDTO.purchasesBillDetailList = this.purchasesBillHeaderDTO.purchasesBillDetailList.filter(x => x.isReturned == true);
			for (let item of this.purchasesBillHeaderDTO.purchasesBillDetailList) {
				item.purchasesBillHeaderId = this.purchasesHeaderId;
				if (this.purchasesHeaderId == 0 || this.purchasesHeaderId == null)
					item.id = 0;
			}
		}
		if (this.validation(this.purchasesBillHeaderDTO)) {
			for (let item of this.purchasesBillHeaderDTO.purchasesBillDetailList) {
				if (!item.discount) item.discount = 0;
			}
			if (!this.purchasesBillHeaderDTO.discount) this.purchasesBillHeaderDTO.discount = 0;

			this.disableButton = true;
			if (this.purchasesBillHeaderDTO.id) {
				this.purchasesBillService.update(this.purchasesBillHeaderDTO).subscribe(res => {
					this.toasterService.success("success");
					this.disableButton = false;
					if (isPrint) {
						this.print();
					}
					this.back();
				})
			}
			else {
				this.purchasesBillHeaderDTO.companyId = this.authService.loggedUserProfile?.companyId;
				this.purchasesBillService.add(this.purchasesBillHeaderDTO).subscribe(res => {
					this.toasterService.success("success");
					this.disableButton = false;
					if (isPrint) {
						this.getPurchasesBillById(res, isPrint);
					}
					else {
						this.back();
					}

				})
			}
		}
	}

	public deleteRow(event: any, item: PurchasesBillDetailsDTO) {
		this.purchasesBillHeaderDTO.purchasesBillDetailList = this.purchasesBillHeaderDTO.purchasesBillDetailList.filter(x => x.index != item.index);
		this.updateTotal();
	}

	addNewRow() {
		for (let i = 0; i < this.numberOfProducts; i++) {
			let lastElement = this.purchasesBillHeaderDTO.purchasesBillDetailList[this.purchasesBillHeaderDTO.purchasesBillDetailList.length - 1];
			let purchasesBillDetails: PurchasesBillDetailsDTO = new PurchasesBillDetailsDTO();
			purchasesBillDetails.index = lastElement ? lastElement.index + 1 : 0;
			this.purchasesBillHeaderDTO.purchasesBillDetailList.push(purchasesBillDetails);
		}
	}


	onProductChange(item: PurchasesBillDetailsDTO, event: any) {
		let exsitedProduct = this.purchasesBillHeaderDTO.purchasesBillDetailList.filter(x => x.productId == item.productId);
		if (exsitedProduct != null && exsitedProduct.length > 1) {
			item.productId = null;
			event = null;
			this.alertService.showError(this.translate.instant("Errors.DuplicatedSelectedProduct"), this.translate.instant("Errors.Error"));
			return;
		}
		this.setProductToPurchase(item, true);
	}


	setProductToPurchase(item: PurchasesBillDetailsDTO, overrideOldData: boolean) {
		if (this.isNewReturn) {
			this.purchasesBillHeaderDTO.total = 0;
			this.purchasesBillHeaderDTO.discount = 0;
			this.purchasesBillHeaderDTO.totalAfterDiscount = 0;
			this.purchasesBillHeaderDTO.otherExpenses = 0;
			this.purchasesBillHeaderDTO.totalAmount = 0;
			this.purchasesBillHeaderDTO.paid = 0;
			this.purchasesBillHeaderDTO.remaining = 0;

		}
		let product = this.productList.find(x => x.id == item.productId);
		let purchasesBillDetail = this.tempPurchasesBillDetailList?.find(x => x.productId == item.productId);

		if (product) {
			item.price = overrideOldData ? product.purchasingPrice : item.price;
			item.lastPurchasingPrice = product.lastPurchasingPrice;
			item.discount = overrideOldData ? product.purchasingPricePercentage : item.discount;

			item.actualQuantity = product.actualQuantity;
			item.productName = product.name;
			item.productCode = product.code;
			item.isReturned = this.purchasesBillHeaderDTO.isReturned;
			if (purchasesBillDetail) {
				item.purchasedQuantity = purchasesBillDetail.quantity;
			}
			this.updateTotal();
		}
	}



	isTaxChange() {
		if (!this.purchasesBillHeaderDTO.isTax) {
			this.purchasesBillHeaderDTO.taxPercentage = 0;
			this.vatPercentage = 0;
		}
		else {
			this.vatPercentage = this.helperService.VATPercentage;
		}
		this.updateTotal();
	}


	updateTotal() {
		this.purchasesBillHeaderDTO.total = 0;
		for (let item of this.purchasesBillHeaderDTO.purchasesBillDetailList) {
			//if (!this.purchasesBillHeaderDTO.isReturned || (this.purchasesBillHeaderDTO.isReturned && item.isReturned)) {
			item.priceAfterDiscount = parseFloat((item.price - (item.discount / 100) * item.price).toFixed(2));
			item.subTotal = parseFloat((item.priceAfterDiscount * item.quantity).toFixed(2));
			this.purchasesBillHeaderDTO.total += item.subTotal;
			//}

		}
		this.purchasesBillHeaderDTO.totalAfterDiscount = parseFloat((this.purchasesBillHeaderDTO.total - this.purchasesBillHeaderDTO.discount).toFixed(2));
		this.purchasesBillHeaderDTO.vatAmount = parseFloat((this.vatPercentage * this.purchasesBillHeaderDTO.totalAfterDiscount).toFixed(2))
		this.purchasesBillHeaderDTO.taxAmount = parseFloat(((this.purchasesBillHeaderDTO.taxPercentage / 100) * this.purchasesBillHeaderDTO.totalAfterDiscount).toFixed(2))
		this.purchasesBillHeaderDTO.totalAfterVAT = this.purchasesBillHeaderDTO.totalAfterDiscount + this.purchasesBillHeaderDTO.vatAmount + this.purchasesBillHeaderDTO.taxAmount;
		this.purchasesBillHeaderDTO.totalAmount = this.purchasesBillHeaderDTO.totalAfterVAT + this.purchasesBillHeaderDTO.otherExpenses;
		this.purchasesBillHeaderDTO.remaining = parseFloat(((this.purchasesBillHeaderDTO.paid ?? 0) - this.purchasesBillHeaderDTO.totalAmount).toFixed(2));

	}



	showProductFormPopUp(item: PurchasesBillDetailsDTO) {
		this.dialogService.show("sm", ProductFormPopupComponent)
			.then((product) => {
				if (product) {
					item.productId = product.id;
					this.getAllProducts();
				}
			})
			.catch(() => console.log('PurchasesBill dismissed the dialog (e.g., by using ESC, clicking the cross icon, or clicking outside the dialog)'));
	}

	onVendorChange() {
		if (this.purchasesBillHeaderDTO.clientVendorId) {
			let selectedVendor = this.vendorList.find(c => c.id == this.purchasesBillHeaderDTO.clientVendorId);
			if (selectedVendor) {
				this.selectedVendor = selectedVendor;
				this.previousBalance = parseFloat((selectedVendor?.debit - selectedVendor?.credit +this.purchasesBillHeaderDTO.remaining).toFixed(2));
			}
		}
	}

	setReturnedItem(item: PurchasesBillDetailsDTO) {
		item.isReturned = !item.isReturned;
		this.updateTotal();
	}

	showCleintVendorFormPopUp() {
		this.dialogService.show("sm", ClientVendorFormPopupComponent, ClientVendorTypeEnum.Vendor)
			.then((clientVendor) => {
				if (clientVendor) {
					this.purchasesBillHeaderDTO.clientVendorId = clientVendor.id
					this.getAllVendors();
				}
			})
			.catch(() => console.log('PurchasesBill dismissed the dialog (e.g., by using ESC, clicking the cross icon, or clicking outside the dialog)'));
	}

	showRepresentiveFormPopUp() {
		this.dialogService.show("sm", RepresentiveFormPopupComponent, RepresentiveTypeEnum.Purchases)
			.then((representive) => {
				if (representive) {
					this.purchasesBillHeaderDTO.representiveId = representive.id
					this.getAllRepresentives();
				}
			})
			.catch(() => console.log('PurchasesBill dismissed the dialog (e.g., by using ESC, clicking the cross icon, or clicking outside the dialog)'));
	}


	saveAndPrint() {
		this.save(true, undefined)
	}

	print() {
		let billNumber: any = document.getElementById('billNumber');
		billNumber.innerHTML = this.purchasesBillHeaderDTO.number;
		let div: any = document.getElementById('purchasesBill');
		this.reportService.print(this.translate.instant("Reports.PurchasesBill"), div);
	}

}
