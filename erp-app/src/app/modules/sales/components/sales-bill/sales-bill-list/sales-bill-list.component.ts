import { Component, Input, ViewChild, ViewEncapsulation } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { PaginationComponent } from 'src/app/shared/components/pagination/pagination.component';
import { PagingDTO } from '../../../../../shared/models/paging-dto';
import { DialogService } from '../../../../../shared/services/confirmation-dialog.service';
import { SalesBillService } from '../../../services/sales-bill.service';
import { SalesBillSearchCriteriaDTO } from '../../../models/sales-bill-search-criteria-dto';
import { SalesBillHeaderDTO } from '../../../models/sales-bill-header.dto';
import { ClientVendorDTO, ClientVendorTypeEnum } from 'src/app/modules/setup/models/client-vendor.dto';
import { ClientVendorService } from 'src/app/modules/setup/services/client-vendor.service';

@Component({
	selector: 'app-sales-bill-list',
	templateUrl: './sales-bill-list.component.html',
	styleUrls: ['./sales-bill-list.component.css']


})
export class SalesBillListComponent {
	@ViewChild(PaginationComponent) paginationComponent: PaginationComponent;
	dataSource: PagingDTO = new PagingDTO();
	salesBillHeaderList: Array<SalesBillHeaderDTO>;
	showFilterControls: boolean = false;
	searchCriteriaDTO: SalesBillSearchCriteriaDTO = new SalesBillSearchCriteriaDTO()
	total: number;
	recordsPerPage: number = 5;
	statusDDL: any;
	@Input() isTemp: boolean = false;
	@Input() isReturned: boolean = false;
	clientList: Array<ClientVendorDTO> = new Array<ClientVendorDTO>();

	constructor(private productService: SalesBillService,
		private confirmationDialogService: DialogService,
		private toastrService: ToastrService,
		private translate: TranslateService,
		private clientVendorService: ClientVendorService) {

	}

	ngOnInit() {
		this.searchCriteriaDTO.isTemp = this.isTemp;
		this.searchCriteriaDTO.isReturned = this.isReturned;
		this.getAllClients();
		this.search();
		this.statusDDL = [
			{ label: "All", value: '' },
			{ label: "Active", value: true },
			{ label: "Inactive", value: false }
		];
		console.table("status", this.statusDDL);
	}

	getAllClients() {
		this.clientVendorService.getAllLiteByTypeId(ClientVendorTypeEnum.Client).subscribe((res: any) => {
			this.clientList = res.list;
		})
	}
	toggleFilter() {
		this.searchCriteriaDTO = new SalesBillSearchCriteriaDTO();
		this.showFilterControls = !this.showFilterControls;
	}

	getAllSalesBills() {
		this.productService.getAll(this.searchCriteriaDTO).subscribe((res: any) => {
			this.salesBillHeaderList = res.list;
			this.total = res.total;
			if (this.paginationComponent) {
				this.paginationComponent.totalRecordsCount = this.total;
				this.paginationComponent.setPagination(this.searchCriteriaDTO.page);
			}
		});
	}

	search() {
		this.getAllSalesBills();
	}

	delete(id: any) {
		this.productService.delete(id).subscribe((res: any) => {
			if (res) {
				this.toastrService.success(this.translate.instant("Message.DeletedSuccessfully"));
				this.getAllSalesBills();
			}
		});
	}

	public openConfirmationDialog(item: SalesBillHeaderDTO) {
		this.confirmationDialogService.confirm(this.translate.instant("ConfirmaionDialog.Title"), this.translate.instant("ConfirmaionDialog.Description"))
			.then((confirmed) => {
				if (confirmed) {
					this.delete(item.id);
				}
			})
			.catch(() => console.log('SalesBill dismissed the dialog (e.g., by using ESC, clicking the cross icon, or clicking outside the dialog)'));
	}

	onPageChange(event: any) {
		this.searchCriteriaDTO.page = event;
		this.getAllSalesBills();
	}
}
