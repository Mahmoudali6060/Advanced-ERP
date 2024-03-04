import { Component, Input, ViewChild, ViewEncapsulation } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { PaginationComponent } from 'src/app/shared/components/pagination/pagination.component';
import { ConfigService } from 'src/app/shared/services/config.service';
import { HelperService } from 'src/app/shared/services/helper.service';
import { CountryModel } from 'src/app/modules/configurations/models/country.model';
import { StateModel } from 'src/app/modules/configurations/models/state.model';
import { CityModel } from 'src/app/modules/configurations/models/city.model';
import { PagingDTO } from 'src/app/shared/models/paging-dto';
import { PurchasesBillHeaderDTO } from '../../models/purchases-bill-header.dto';
import { PurchasesBillSearchCriteriaDTO } from '../../models/purchases-bill-search-criteria-dto';
import { DialogService } from 'src/app/shared/services/confirmation-dialog.service';
import { PurchasesBillService } from '../../services/purchases-bill.service';
import { ClientVendorDTO, ClientVendorTypeEnum } from 'src/app/modules/setup/models/client-vendor.dto';
import { ClientVendorService } from 'src/app/modules/setup/services/client-vendor.service';
import { RepresentiveService } from 'src/app/modules/setup/services/representive.service';
import { RepresentiveDTO } from 'src/app/modules/setup/models/representive.dto';

@Component({
	selector: 'app-purchases-bill-list',
	templateUrl: './purchases-bill-list.component.html',
	styleUrls: ['./purchases-bill-list.component.css']


})
export class PurchasesBillListComponent {
	@ViewChild(PaginationComponent) paginationComponent: PaginationComponent;
	dataSource: PagingDTO = new PagingDTO();
	purchasesBillHeaderList: Array<PurchasesBillHeaderDTO>;
	showFilterControls: boolean = false;
	searchCriteriaDTO: PurchasesBillSearchCriteriaDTO = new PurchasesBillSearchCriteriaDTO()
	total: number;
	recordsPerPage: number = 5;
	statusDDL: any;
	@Input() isTemp: boolean = false;
	@Input() isReturned: boolean = false;
	vendorList: Array<ClientVendorDTO> = new Array<ClientVendorDTO>();
	representiveList: Array<RepresentiveDTO> = new Array<RepresentiveDTO>();

	constructor(private productService: PurchasesBillService,
		private confirmationDialogService: DialogService,
		private toastrService: ToastrService,
		private translate: TranslateService,
		public helperService: HelperService,
		private clientVendorService: ClientVendorService,
		private representiveService: RepresentiveService) {

	}

	ngOnInit() {
		this.searchCriteriaDTO.isTemp = this.isTemp;
		this.searchCriteriaDTO.isReturned = this.isReturned;
		this.getAllVendors();
		this.getAllRepresentives();
		this.search();
		this.statusDDL = [
			{ label: "All", value: '' },
			{ label: "Active", value: true },
			{ label: "Inactive", value: false }
		];
		console.table("status", this.statusDDL);
	}

	toggleFilter() {
		// this.searchCriteriaDTO = new PurchasesBillSearchCriteriaDTO();
		this.showFilterControls = !this.showFilterControls;
	}


	getAllVendors() {
		this.clientVendorService.getAllLiteByTypeId(ClientVendorTypeEnum.Vendor).subscribe((res: any) => {
			this.vendorList = res.list;
		})
	}

	getAllRepresentives() {
		this.representiveService.getAllLite().subscribe((res: any) => {
			this.representiveList = res.list;
		})
	}


	getAllPurchasesBills() {
		this.productService.getAll(this.searchCriteriaDTO).subscribe((res: any) => {
			this.purchasesBillHeaderList = res.list;
			this.total = res.total;
			if (this.paginationComponent) {
				this.paginationComponent.totalRecordsCount = this.total;
				this.paginationComponent.setPagination(this.searchCriteriaDTO.page);
			}
		});
	}

	search() {
		this.getAllPurchasesBills();
	}

	delete(id: any) {
		this.productService.delete(id).subscribe((res: any) => {
			if (res) {
				this.toastrService.success(this.translate.instant("Message.DeletedSuccessfully"));
				this.getAllPurchasesBills();
			}
		});
	}

	public openConfirmationDialog(item: PurchasesBillHeaderDTO) {
		this.confirmationDialogService.confirm(this.translate.instant("DeleteConfirmaionDialog.Title"), this.translate.instant("DeleteConfirmaionDialog.Description"))
			.then((confirmed) => {
				if (confirmed) {
					this.delete(item.id);
				}
			})
			.catch(() => console.log('PurchasesBill dismissed the dialog (e.g., by using ESC, clicking the cross icon, or clicking outside the dialog)'));
	}

	onPageChange(event: any) {
		this.searchCriteriaDTO.page = event;
		this.getAllPurchasesBills();
	}
}
