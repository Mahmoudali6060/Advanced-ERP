import { Component, HostListener, Input, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ConfigService } from 'src/app/shared/services/config.service';
import { NgForm } from '@angular/forms';
import { ProductDTO } from 'src/app/modules/setup/models/product.dto';
import { SalesBillService } from '../../../services/sales-bill.service';
import { ProductService } from 'src/app/modules/setup/services/product.service';
import { SalesBillHeaderDTO } from '../../../models/sales-bill-header.dto';
import { ClientVendorDTO, ClientVendorTypeEnum } from 'src/app/modules/setup/models/client-vendor.dto';
import { SalesBillDetailsDTO } from '../../../models/sales-bill-details.dto';
import { Pipe, PipeTransform } from '@angular/core';
import { DatePipe } from '@angular/common';
import { HelperService } from 'src/app/shared/services/helper.service';
import { TranslateService } from '@ngx-translate/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ProductFormPopupComponent } from 'src/app/shared/modules/setup-shared/components/product-form-popup/product-form-popup.component';
import { DialogService } from 'src/app/shared/services/confirmation-dialog.service';
import { ClientVendorService } from 'src/app/modules/setup/services/client-vendor.service';
import { AlertService } from 'src/app/shared/services/alert.service';
import { ClientVendorFormPopupComponent } from 'src/app/shared/modules/setup-shared/components/client-vendor-form-popup/client-vendor-form-popup.component';
import { AuthService } from 'src/app/modules/authentication/services/auth.service';
import { ReportService } from 'src/app/modules/report/services/report.service';
import { RepresentiveDTO } from 'src/app/modules/setup/models/representive.dto';
import { RepresentiveService } from 'src/app/modules/setup/services/representive.service';
import { RepresentiveTypeEnum } from 'src/app/shared/enums/representive-type.enum';
import { RepresentiveFormPopupComponent } from 'src/app/shared/modules/setup-shared/components/representive-form-popup/representive-form-popup.component';
import { Location } from '@angular/common';
import { ComponentCanDeactivate } from 'src/app/shared/guards/pending-changes-guard.service';
import { Observable } from 'rxjs';

@Component({
	selector: 'app-sales-bill-form',
	templateUrl: './sales-bill-form.component.html',
	styleUrls: ['./sales-bill-form.component.css']
})
export class SalesBillFormComponent implements ComponentCanDeactivate {
	// @HostListener allows us to also guard against browser refresh, close, etc.
	@HostListener('window:beforeunload')
	canDeactivate(): Observable<any> | any {
		// debugger;
		if (this.viewMode == false && this.salesBillHeaderDTO.salesBillDetailList.length > 0) {
			return true;
		}
		return false;
	}
	salesBillHeaderDTO: SalesBillHeaderDTO = new SalesBillHeaderDTO();
	originalSalesBillDetailsList: Array<SalesBillDetailsDTO> = new Array<SalesBillDetailsDTO>();
	serverUrl: string;
	viewMode: boolean = false;
	productList: Array<ProductDTO> = new Array<ProductDTO>();
	clientList: Array<ClientVendorDTO> = new Array<ClientVendorDTO>();
	salesHeaderId: any;
	currentBalance: number = 0;
	@Input() searchByNumber: boolean = false;
	selectedClient: ClientVendorDTO = new ClientVendorDTO();
	representiveList: Array<RepresentiveDTO> = new Array<RepresentiveDTO>();
	//#region 
	reportName: string;
	parameters: any;
	reportPopupTitle: any;
	isReportOpened: boolean;
	//#endregion
	vatPercentage: number = 0;
	numberOfProducts: number = 1;
	@Input() isTemp: boolean = false;
	@Input() isReturned: boolean = false;
	isTransfereToBill: boolean = false;
	constructor(
		private salesBillService: SalesBillService,
		private productService: ProductService,
		private clientVendorService: ClientVendorService,
		private route: ActivatedRoute,
		private toasterService: ToastrService,
		private _configService: ConfigService,
		private router: Router,
		private helperService: HelperService,
		private translate: TranslateService,
		private dialogService: DialogService,
		private alertService: AlertService,
		private authService: AuthService,
		private reportService: ReportService,
		private representiveService: RepresentiveService,
		private location: Location

	) {
	}

	ngOnInit() {
		this.salesBillHeaderDTO.isTemp = this.isTemp;
		this.salesBillHeaderDTO.isReturned = this.isReturned;

		this.serverUrl = this._configService.getServerUrl();

		this.salesHeaderId = this.route.snapshot.paramMap.get('id');

		if (this.salesHeaderId) {
			this.getSalesBillById(this.salesHeaderId);
			if (this.router.url.includes('view')) {
				this.viewMode = true;
			}
		}
		else {
			this.salesBillHeaderDTO.clientVendorId = null;
			this.addNewRow();
			//set today by default>>Insert Mode
			this.salesBillHeaderDTO.date = this.helperService.conveertDateToString(new Date());
			this.getAllProducts();
			this.getAllClients();
			this.getAllRepresentives();
		}

	}

	getAllProducts() {
		this.productService.getAllLite().subscribe((res: any) => {
			this.productList = res.list;
			this.setPurchaseDetailsDefaultData();
		})
	}

	getAllClients() {
		this.clientVendorService.getAllLiteByTypeId(ClientVendorTypeEnum.Client).subscribe((res: any) => {
			this.clientList = res.list;
			this.onClientChange();

		})
	}

	getAllRepresentives() {
		this.representiveService.getAllLite().subscribe((res: any) => {
			this.representiveList = res.list;
			this.representiveList = this.representiveList.filter(x => x.representiveTypeId == RepresentiveTypeEnum.Sales);
		})
	}

	getSalesBillById(salesBillId: any) {
		this.salesBillService.getById(salesBillId).subscribe((res: any) => {
			this.salesBillHeaderDTO = res;
			//To prevent change after cloning
			this.originalSalesBillDetailsList = res.salesBillDetailList.map((el: any) => Object.assign({}, el));
			this.getAllProducts();
			this.getAllClients();
			this.getAllRepresentives();
			this.isTaxChange();
		});
	}

	setPurchaseDetailsDefaultData() {
		if (this.salesBillHeaderDTO.salesBillDetailList) {
			let index = 0;
			for (let item of this.salesBillHeaderDTO.salesBillDetailList) {
				item.index = index;
				index++;
				this.setProductToSales(item, false);
			}
		}
	}

	back() {
		this.location.back();
		// this.router.navigateByUrl('sales-bill/sales-bill-list');
	}
	validation(salesBillDTO: SalesBillHeaderDTO): boolean {
		if (!salesBillDTO.clientVendorId) {
			this.toasterService.error(this.translate.instant("Errors.YouMustSelectClient"));
			return false;
		}
		if (!salesBillDTO.salesBillDetailList || this.salesBillHeaderDTO.salesBillDetailList.length == 0) {
			this.toasterService.error(this.translate.instant("Errors.YouMustSelectProducts"));
			return false;
		}

		if (salesBillDTO.salesBillDetailList?.length > 0) {
			for (let item of this.salesBillHeaderDTO.salesBillDetailList) {
				if (!item.productId) {
					this.toasterService.error(this.translate.instant("Errors.YouMustSelectProducts"));
					return false;
				}
			}
		}
		return true;

	}

	save(isPrint: boolean, form?: NgForm) {
		if (this.isTransfereToBill == true)
			this.salesBillHeaderDTO.isTemp = false;
		if (this.salesBillHeaderDTO.isReturned) {
			this.salesBillHeaderDTO.id = 0;
			this.salesBillHeaderDTO.salesBillDetailList = this.salesBillHeaderDTO.salesBillDetailList.filter(x => x.isReturned == true);
			for (let item of this.salesBillHeaderDTO.salesBillDetailList) {
				item.salesBillHeaderId = 0;
				item.id = 0;
			}
		}
		if (this.validation(this.salesBillHeaderDTO)) {
			if (this.salesBillHeaderDTO.id) {
				this.salesBillService.update(this.salesBillHeaderDTO).subscribe(res => {
					this.toasterService.success("success");
					if (isPrint) {
						this.print();
						window.location.reload();
					}
					else {
						this.back();
					}
				})
			}
			else {
				this.salesBillHeaderDTO.companyId = this.authService.loggedUserProfile?.companyId;
				this.salesBillService.add(this.salesBillHeaderDTO).subscribe(res => {
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

	public deleteRow(event: any, item: SalesBillDetailsDTO) {
		this.salesBillHeaderDTO.salesBillDetailList = this.salesBillHeaderDTO.salesBillDetailList.filter(x => x.index != item.index);
		this.updateTotal();
	}

	addNewRow() {
		for (let i = 0; i < this.numberOfProducts; i++) {
			let lastElement = this.salesBillHeaderDTO.salesBillDetailList[this.salesBillHeaderDTO.salesBillDetailList.length - 1];
			let salesBillDetails: SalesBillDetailsDTO = new SalesBillDetailsDTO();
			salesBillDetails.index = lastElement ? lastElement.index + 1 : 0;
			this.salesBillHeaderDTO.salesBillDetailList.push(salesBillDetails);
		}
	}

	onProductChange(item: SalesBillDetailsDTO, event: any) {

		let exsitedProduct = this.salesBillHeaderDTO.salesBillDetailList.filter(x => x.productId == item.productId);
		if (exsitedProduct != null && exsitedProduct.length > 1) {
			item.productId = null;
			event = null;
			this.alertService.showError(this.translate.instant("Errors.DuplicatedSelectedProduct"), this.translate.instant("Errors.Error"));
			return;
		}
		this.setProductToSales(item, true);
	}

	onQuantityChange(item: SalesBillDetailsDTO) {
		let quantity = 0;
		if (this.originalSalesBillDetailsList && this.originalSalesBillDetailsList.length > 0) {
			let exsitedSalesBillDetails = this.originalSalesBillDetailsList.find(x => x.id == item.id && x.productId == item.productId);
			if (exsitedSalesBillDetails) {
				quantity = exsitedSalesBillDetails.quantity;
			}
		}
		if (item.quantity > item.actualQuantity + quantity) {
			this.alertService.showWarning(this.translate.instant("Errors.QuantityIsGreaterThanActualQuantity"), this.translate.instant("Errors.Warning"));
			//item.quantity = item.actualQuantity;
			//return;
		}
		this.updateTotal();
	}
	setProductToSales(item: SalesBillDetailsDTO, overrideOldData: boolean) {
		let product = this.productList.find(x => x.id == item.productId);
		if (product) {
			item.price = overrideOldData ? product.price : item.price;
			item.lastPurchasingPrice = product.lastPurchasingPrice;
			item.discount = overrideOldData ? product.sellingPricePercentage : item.discount;
			item.actualQuantity = product.actualQuantity;
			item.sellingPrice = (product.lastPurchasingPrice - (product.sellingPricePercentage / 100) * product.lastPurchasingPrice);
			item.productName = product.name;
			item.productCode = product.code;
			this.updateTotal();
		}
	}

	isTaxChange() {
		if (!this.salesBillHeaderDTO.isTax) {
			this.salesBillHeaderDTO.taxPercentage = 0;
			this.vatPercentage = 0;
		}
		else {
			this.vatPercentage = this.helperService.VATPercentage;
		}
		this.updateTotal();
	}


	updateTotal() {
		this.salesBillHeaderDTO.total = 0;
		this.salesBillHeaderDTO.profit = 0;
		for (let item of this.salesBillHeaderDTO.salesBillDetailList) {
			if (!this.salesBillHeaderDTO.isReturned) {
				item.priceAfterDiscount = parseFloat((item.price - (item.discount / 100) * item.price).toFixed(2));
				item.subTotal = item.priceAfterDiscount * item.quantity;
				this.salesBillHeaderDTO.total += item.subTotal;
				this.salesBillHeaderDTO.profit += (item.subTotal - (item.lastPurchasingPrice * item.quantity));
			}
			else if (this.salesBillHeaderDTO.isReturned && item.isReturned) {
				item.priceAfterDiscount = parseFloat((item.price - (item.discount / 100) * item.price).toFixed(2));
				item.subTotal = item.priceAfterDiscount * item.quantity;
				this.salesBillHeaderDTO.total += item.subTotal;
				this.salesBillHeaderDTO.profit += (item.subTotal - (item.lastPurchasingPrice * item.quantity));
			}
		}
		this.salesBillHeaderDTO.totalAfterDiscount = parseFloat((this.salesBillHeaderDTO.total - this.salesBillHeaderDTO.discount).toFixed(2));
		this.salesBillHeaderDTO.vatAmount = parseFloat((this.vatPercentage * this.salesBillHeaderDTO.totalAfterDiscount).toFixed(2))
		this.salesBillHeaderDTO.taxAmount = parseFloat(((this.salesBillHeaderDTO.taxPercentage / 100) * this.salesBillHeaderDTO.totalAfterDiscount).toFixed(2))
		this.salesBillHeaderDTO.totalAfterVAT = this.salesBillHeaderDTO.totalAfterDiscount + this.salesBillHeaderDTO.vatAmount + this.salesBillHeaderDTO.taxAmount;
		this.salesBillHeaderDTO.totalAmount = this.salesBillHeaderDTO.totalAfterVAT + this.salesBillHeaderDTO.otherExpenses;
		this.salesBillHeaderDTO.remaining = parseFloat((this.salesBillHeaderDTO.paid - this.salesBillHeaderDTO.totalAmount).toFixed(2));
	}



	onClientChange() {
		if (this.salesBillHeaderDTO.clientVendorId) {
			let selectedClient: any = this.clientList.find(c => c.id == this.salesBillHeaderDTO.clientVendorId);
			if (selectedClient) {
				this.selectedClient = selectedClient;
				this.currentBalance = selectedClient?.debit - selectedClient?.credit;
			}
		}
	}

	viewReport() {
		this.reportName = 'Sales/SalesBill';
		this.parameters = {
			'SalesHeaderId': this.salesBillHeaderDTO.id,
			'PrintedBy': 'TTTTT',
			'PrintingDate': null //new Date().toString()//this.datePipe.transform(this.sharedService.convertDateTimeToString(new Date()), this.sharedService.getPropertyDateTimeFormat()),
		};
		this.reportPopupTitle = this.translate.instant('ttt');
		this.isReportOpened = true;
	}

	onReportPopupClose() {
		this.isReportOpened = false;
	}


	getSalesByNumber() {
		this.salesBillService.getByNumber(this.salesBillHeaderDTO.number).subscribe((res: any) => {
			if (!res) {
				this.salesBillHeaderDTO = new SalesBillHeaderDTO();
				this.currentBalance = 0;
				this.alertService.showError(this.translate.instant("Errors.NotFound"), this.translate.instant("Errors.Error"));
				return;
			}
			this.salesBillHeaderDTO = res;
			if (this.isReturned) {
				this.salesBillHeaderDTO.id = null;
				this.salesBillHeaderDTO.isReturned = true;
			}
			this.getAllProducts();
			this.getAllClients();
		});
	}

	showCleintVendorFormPopUp() {
		this.dialogService.show("sm", ClientVendorFormPopupComponent, ClientVendorTypeEnum.Client)
			.then((clientVendor) => {
				if (clientVendor) {
					this.salesBillHeaderDTO.clientVendorId = clientVendor.id
					this.getAllClients();
				}
			})
			.catch(() => console.log('SalesBill dismissed the dialog (e.g., by using ESC, clicking the cross icon, or clicking outside the dialog)'));
	}

	showRepresentiveFormPopUp() {
		this.dialogService.show("sm", RepresentiveFormPopupComponent, RepresentiveTypeEnum.Sales)
			.then((representive) => {
				if (representive) {
					this.salesBillHeaderDTO.representiveId = representive.id
					this.getAllRepresentives();
				}
			})
			.catch(() => console.log('SalesBill dismissed the dialog (e.g., by using ESC, clicking the cross icon, or clicking outside the dialog)'));
	}


	setReturnedItem(item: SalesBillDetailsDTO) {
		item.isReturned = !item.isReturned;
		this.updateTotal();
	}
	saveAndPrint() {
		this.save(true, undefined)
	}

	print() {
		let div: any = document.getElementById('salesBill');
		this.reportService.print(this.translate.instant("Reports.SalesBill"), div);
	}

}
