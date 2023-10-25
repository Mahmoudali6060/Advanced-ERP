import { Component, ViewChild } from '@angular/core';
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
import { PurchasesBillDetailsDTO } from 'src/app/modules/purchases/models/purchases-bill-details.dto';
import { ClientVendorService } from 'src/app/modules/setup/services/client-vendor.service';
import { AlertService } from 'src/app/shared/services/alert.service';

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
	previousBalance: number = 0;
	searhByCodeMode: any = false;
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
		private alertService: AlertService
	) {
	}

	ngOnInit() {
		this.purchaseHeaderId = this.route.snapshot.paramMap.get('id');
		this.searhByCodeMode = this.route.snapshot.paramMap.get('searhByCodeMode');


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

	getSalesBillById(salesBillId: any) {
		this.salesBillService.getById(salesBillId).subscribe((res: any) => {
			debugger;
			this.salesBillHeaderDTO = res;
			this.getAllProducts();
			this.getAllClients();
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
		if (!salesBillDTO.salesBillDetailList || this.salesBillHeaderDTO.salesBillDetailList.length == 0) {
			this.toasterService.error(this.translate.instant("Errors.YouMustSelectProducts"));
			return false;
		}

		return true;

	}

	save(form: NgForm) {
		if (this.validation(this.salesBillHeaderDTO)) {
			if (this.salesBillHeaderDTO.id) {
				this.salesBillService.update(this.salesBillHeaderDTO).subscribe(res => {
					this.toasterService.success("success");
					this.back();
				})
			}
			else {
				this.salesBillService.add(this.salesBillHeaderDTO).subscribe(res => {
					this.toasterService.success("success");
					this.back();
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
			if (!item.discount) item.discount = product.purchasingPricePercentage;
			item.actualQuantity = product.actualQuantity;
			item.sellingPrice = (product.price - (product.purchasingPricePercentage / 100) * product.price);
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
		this.salesBillHeaderDTO.remaining = this.salesBillHeaderDTO.totalAfterDiscount - this.salesBillHeaderDTO.paid;
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

	onClientChange() {
		if (this.salesBillHeaderDTO.clientVendorId) {
			let selectedClient: any = this.clientList.find(c => c.id == this.salesBillHeaderDTO.clientVendorId);
			this.previousBalance = selectedClient?.debit - selectedClient?.credit;
		}
	}
}
