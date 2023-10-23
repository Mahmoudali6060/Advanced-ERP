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
		private dialogService: DialogService) {
	}

	ngOnInit() {
		this.purchaseHeaderId = this.route.snapshot.paramMap.get('id');
		if (this.purchaseHeaderId) {
			this.getSalesBillById(this.purchaseHeaderId);
			if (this.router.url.includes('/view/')) {
				this.viewMode = true;
			}
		}
		else {
			this.salesBillHeaderDTO.clientId = null;
			this.addNewRow();
			//set today by default>>Insert Mode
			this.salesBillHeaderDTO.date = this.helperService.conveertDateToString(new Date());
		}
		this.getAllProducts();
		this.getAllClients();
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
		})
	}

	getSalesBillById(salesBillId: any) {
		this.salesBillService.getById(salesBillId).subscribe((res: any) => {
			this.salesBillHeaderDTO = res;
			this.setPurchaseDetailsDefaultData();
		});
	}

	setPurchaseDetailsDefaultData() {
		if (this.salesBillHeaderDTO.salesBillDetailList) {
			let index = 0;
			for (let item of this.salesBillHeaderDTO.salesBillDetailList) {
				item.index = index;
				index++;
				this.onProductChange(item);
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

	onProductChange(item: SalesBillDetailsDTO) {
		let product = this.productList.find(x => x.id == item.productId);
		if (product) {
			if (!item.price) item.price = product.price;
			if (!item.discount) item.discount = product.purchasingPricePercentage;
			item.actualQuantity = product.actualQuantity;
			this.updateTotal();
		}
	}
	updateTotal() {
		this.salesBillHeaderDTO.total = 0;
		for (let item of this.salesBillHeaderDTO.salesBillDetailList) {
			item.priceAfterDiscount = item.price - (item.discount / 100) * item.price;
			item.subTotal = item.priceAfterDiscount * item.quantity;
			this.salesBillHeaderDTO.total += item.subTotal;
		}
		this.salesBillHeaderDTO.totalDiscount = this.salesBillHeaderDTO.transfer + this.salesBillHeaderDTO.discount;
		this.salesBillHeaderDTO.totalAfterDiscount = this.salesBillHeaderDTO.total - this.salesBillHeaderDTO.totalDiscount;
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
