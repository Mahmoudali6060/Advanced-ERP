import { Component, Input, ViewChild } from '@angular/core';
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

@Component({
	selector: 'app-sales-bill-form',
	templateUrl: './sales-bill-form.component.html',
	styleUrls: ['./sales-bill-form.component.css']
})
export class SalesBillFormComponent {

	salesBillHeaderDTO: SalesBillHeaderDTO = new SalesBillHeaderDTO();
	serverUrl: string;
	viewMode: boolean;
	productList: Array<ProductDTO> = new Array<ProductDTO>();
	clientList: Array<ClientVendorDTO> = new Array<ClientVendorDTO>();
	purchaseHeaderId: any;
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
		private representiveService: RepresentiveService

	) {
	}

	ngOnInit() {
		this.serverUrl = this._configService.getServerUrl();

		this.purchaseHeaderId = this.route.snapshot.paramMap.get('id');

		if (this.purchaseHeaderId) {
			this.getSalesBillById(this.purchaseHeaderId);
			if (this.router.url.includes('/view/')) {
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
			this.getAllProducts();
			this.getAllClients();
			this.getAllRepresentives();
		});
	}

	setPurchaseDetailsDefaultData() {
		if (this.salesBillHeaderDTO.salesBillDetailList) {
			let index = 0;
			for (let item of this.salesBillHeaderDTO.salesBillDetailList) {
				item.index = index;
				index++;
				this.setProductToSales(item);
			}
		}
	}

	back() {
		this.router.navigateByUrl('sales-bill/sales-bill-list');
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
		if (this.validation(this.salesBillHeaderDTO)) {
			if (this.salesBillHeaderDTO.id) {
				this.salesBillService.update(this.salesBillHeaderDTO).subscribe(res => {
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
		let lastElement = this.salesBillHeaderDTO.salesBillDetailList[this.salesBillHeaderDTO.salesBillDetailList.length - 1];
		let salesBillDetails: SalesBillDetailsDTO = new SalesBillDetailsDTO();
		salesBillDetails.index = lastElement ? lastElement.index + 1 : 0;
		this.salesBillHeaderDTO.salesBillDetailList.push(salesBillDetails);
	}

	onProductChange(item: SalesBillDetailsDTO, event: any) {
		let exsitedProduct = this.salesBillHeaderDTO.salesBillDetailList.filter(x => x.productId == item.productId);
		if (exsitedProduct != null && exsitedProduct.length > 1) {
			item.productId = null;
			event = null;
			this.alertService.showError(this.translate.instant("Errors.DuplicatedSelectedProduct"), this.translate.instant("Errors.Error"));
			return;
		}
		this.setProductToSales(item);
	}

	setProductToSales(item: SalesBillDetailsDTO) {
		let product = this.productList.find(x => x.id == item.productId);
		if (product) {
			if (!item.price) item.price = product.price;
			if (!item.discount) item.discount = product.sellingPricePercentage;
			item.actualQuantity = product.actualQuantity;
			item.sellingPrice = (product.price - (product.sellingPricePercentage / 100) * product.price);
			item.productName = product.name;
			item.productCode = product.code;
			this.updateTotal();
		}
	}


	updateTotal() {
		this.salesBillHeaderDTO.total = 0;
		for (let item of this.salesBillHeaderDTO.salesBillDetailList) {
			item.priceAfterDiscount = parseFloat((item.price - (item.discount / 100) * item.price).toFixed(2));
			item.subTotal = item.priceAfterDiscount * item.quantity;
			this.salesBillHeaderDTO.total += item.subTotal;
		}
		this.salesBillHeaderDTO.totalAfterDiscount = parseFloat((this.salesBillHeaderDTO.transfer + this.salesBillHeaderDTO.total - this.salesBillHeaderDTO.totalDiscount).toFixed(2));
		this.salesBillHeaderDTO.remaining = this.salesBillHeaderDTO.paid - this.salesBillHeaderDTO.totalAfterDiscount;
		this.salesBillHeaderDTO.vatAmount = parseFloat((0.14 * this.salesBillHeaderDTO.total).toFixed(2))
		this.salesBillHeaderDTO.totalAfterVAT = this.salesBillHeaderDTO.total + this.salesBillHeaderDTO.vatAmount;
		this.salesBillHeaderDTO.totalAfterTax = this.salesBillHeaderDTO.totalAfterDiscount + parseFloat(((this.salesBillHeaderDTO.taxPercentage / 100) * this.salesBillHeaderDTO.totalAfterDiscount).toFixed(2))

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


	saveAndPrint() {
		this.save(true, undefined)
	}

	print() {
		let div: any = document.getElementById('salesBill');
		this.reportService.print(this.translate.instant("Reports.SalesBill"), div);
	}

}
