import { Component, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ConfigService } from 'src/app/shared/services/config.service';
import { NgForm } from '@angular/forms';
import { ProductDTO } from 'src/app/modules/setup/models/product.dto';
import { PurchasesBillService } from '../../../services/purchases-bill.service';
import { ProductService } from 'src/app/modules/setup/services/product.service';
import { PurchasesBillHeaderDTO } from '../../../models/purchases-bill-header.dto';
import { PurchasesBillDetailsDTO } from '../../../models/purchases-bill-details.dto';
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
	vendorList: Array<ClientVendorDTO> = new Array<ClientVendorDTO>();
	representiveList: Array<RepresentiveDTO> = new Array<RepresentiveDTO>();
	purchaseHeaderId: any;
	searchProduct: any;
	currentBalance: number = 0;
	@Input() searchByNumber: boolean = false;
	selectedVendor: ClientVendorDTO = new ClientVendorDTO();

	constructor(
		private purchasesBillService: PurchasesBillService,
		private productService: ProductService,
		private clientVendorService: ClientVendorService,
		private route: ActivatedRoute,
		private toasterService: ToastrService,
		private router: Router,
		private helperService: HelperService,
		private translate: TranslateService,
		private dialogService: DialogService,
		private alertService: AlertService,
		private authService: AuthService,
		private reportService: ReportService,
		private representiveService: RepresentiveService) {
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
			this.purchasesBillHeaderDTO.clientVendorId = null;
			this.addNewRow();
			//set today by default>>Insert Mode
			this.purchasesBillHeaderDTO.date = this.helperService.conveertDateToString(new Date());
			this.getAllProducts();
			this.getAllVendors();
			this.getAllRepresentives();
		}

	}

	getAllProducts() {
		this.productService.getAllLite().subscribe((res: any) => {
			this.productList = res.list;
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
			this.representiveList = this.representiveList.filter(x => x.representiveTypeId == RepresentiveTypeEnum.Purchases);
		})
	}

	getPurchasesBillById(purchasesBillId: any) {
		this.purchasesBillService.getById(purchasesBillId).subscribe((res: any) => {
			this.purchasesBillHeaderDTO = res;
			this.getAllProducts();
			this.getAllVendors();
			this.getAllRepresentives();
		});
	}

	getPurchaseByNumber() {
		this.purchasesBillService.getByNumber(this.purchasesBillHeaderDTO.number).subscribe((res: any) => {
			if (!res) {
				this.purchasesBillHeaderDTO = new PurchasesBillHeaderDTO();
				this.currentBalance = 0;
				this.alertService.showError(this.translate.instant("Errors.NotFound"), this.translate.instant("Errors.Error"));
				return;
			}
			this.purchasesBillHeaderDTO = res;
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
				this.setProductToPurchase(item);
			}
		}
	}

	back() {
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
		return true;

	}

	save(isPrint: boolean, form?: NgForm) {
		if (this.validation(this.purchasesBillHeaderDTO)) {
			if (this.purchasesBillHeaderDTO.id) {
				this.purchasesBillService.update(this.purchasesBillHeaderDTO).subscribe(res => {
					this.toasterService.success("success");
					if (isPrint) {
						this.print();
					}
					else {
						this.back();
					}
				})
			}
			else {
				this.purchasesBillHeaderDTO.companyId = this.authService.loggedUserProfile?.companyId;
				this.purchasesBillService.add(this.purchasesBillHeaderDTO).subscribe(res => {
					this.toasterService.success("success");
					if (isPrint) {
						this.print();
						this.back();
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
		let lastElement = this.purchasesBillHeaderDTO.purchasesBillDetailList[this.purchasesBillHeaderDTO.purchasesBillDetailList.length - 1];
		let purchasesBillDetails: PurchasesBillDetailsDTO = new PurchasesBillDetailsDTO();
		purchasesBillDetails.index = lastElement ? lastElement.index + 1 : 0;
		this.purchasesBillHeaderDTO.purchasesBillDetailList.push(purchasesBillDetails);
	}


	onProductChange(item: PurchasesBillDetailsDTO, event: any) {
		let exsitedProduct = this.purchasesBillHeaderDTO.purchasesBillDetailList.filter(x => x.productId == item.productId);
		if (exsitedProduct != null && exsitedProduct.length > 1) {
			item.productId = null;
			event = null;
			this.alertService.showError(this.translate.instant("Errors.DuplicatedSelectedProduct"), this.translate.instant("Errors.Error"));
			return;
		}
		this.setProductToPurchase(item);
	}


	setProductToPurchase(item: PurchasesBillDetailsDTO) {
		let product = this.productList.find(x => x.id == item.productId);
		if (product) {
			if (!item.price) item.price = product.price;
			if (!item.discount) item.discount = product.purchasingPricePercentage;
			item.actualQuantity = product.actualQuantity;
			item.productName = product.name;
			item.productCode = product.code;
			this.updateTotal();
		}
	}



	updateTotal() {
		this.purchasesBillHeaderDTO.total = 0;
		for (let item of this.purchasesBillHeaderDTO.purchasesBillDetailList) {
			item.priceAfterDiscount = parseFloat((item.price - (item.discount / 100) * item.price).toFixed(2));
			item.subTotal = item.priceAfterDiscount * item.quantity;
			this.purchasesBillHeaderDTO.total += item.subTotal;
		}
		this.purchasesBillHeaderDTO.totalAfterDiscount = parseFloat((this.purchasesBillHeaderDTO.transfer + this.purchasesBillHeaderDTO.total - this.purchasesBillHeaderDTO.totalDiscount).toFixed(2));
		this.purchasesBillHeaderDTO.remaining = this.purchasesBillHeaderDTO.paid - this.purchasesBillHeaderDTO.totalAfterDiscount;
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

	onVendorChange() {
		if (this.purchasesBillHeaderDTO.clientVendorId) {
			let selectedVendor = this.vendorList.find(c => c.id == this.purchasesBillHeaderDTO.clientVendorId);
			if (selectedVendor) {
				this.selectedVendor = selectedVendor;
				this.currentBalance = this.selectedVendor?.debit - this.selectedVendor?.credit;
			}
		}
	}

	showCleintVendorFormPopUp() {
		this.dialogService.show("sm", ClientVendorFormPopupComponent, ClientVendorTypeEnum.Vendor)
			.then((clientVendor) => {
				if (clientVendor) {
					this.purchasesBillHeaderDTO.clientVendorId = clientVendor.id
					this.getAllVendors();
				}
			})
			.catch(() => console.log('SalesBill dismissed the dialog (e.g., by using ESC, clicking the cross icon, or clicking outside the dialog)'));
	}

	showRepresentiveFormPopUp() {
		this.dialogService.show("sm", RepresentiveFormPopupComponent, RepresentiveTypeEnum.Purchases)
			.then((representive) => {
				if (representive) {
					this.purchasesBillHeaderDTO.representiveId = representive.id
					this.getAllRepresentives();
				}
			})
			.catch(() => console.log('SalesBill dismissed the dialog (e.g., by using ESC, clicking the cross icon, or clicking outside the dialog)'));
	}


	saveAndPrint() {
		this.save(true, undefined)
	}

	print() {
		let div: any = document.getElementById('purchasesBill');
		this.reportService.print(this.translate.instant("Reports.PurchasesBill"), div);
	}

}
