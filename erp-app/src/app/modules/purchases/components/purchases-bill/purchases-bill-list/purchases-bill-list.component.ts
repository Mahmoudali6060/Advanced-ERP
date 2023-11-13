import { Component, ViewChild, ViewEncapsulation } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { PaginationComponent } from 'src/app/shared/components/pagination/pagination.component';
import { ConfigService } from 'src/app/shared/services/config.service';
import { HelperService } from 'src/app/shared/services/helper.service';
import { PagingDTO } from '../../../../../shared/models/paging-dto';
import { DialogService } from '../../../../../shared/services/confirmation-dialog.service';
import { CountryModel } from 'src/app/modules/configurations/models/country.model';
import { StateModel } from 'src/app/modules/configurations/models/state.model';
import { CityModel } from 'src/app/modules/configurations/models/city.model';
import { PurchasesBillService } from '../../../services/purchases-bill.service';
import { PurchasesBillSearchCriteriaDTO } from '../../../models/purchases-bill-search-criteria-dto';
import { PurchasesBillHeaderDTO } from '../../../models/purchases-bill-header.dto';

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

	constructor(private productService: PurchasesBillService,
		private confirmationDialogService: DialogService,
		private toastrService: ToastrService,
		private translate: TranslateService) {

	}

	ngOnInit() {
		this.search();
		this.statusDDL = [
			{ label: "All", value: '' },
			{ label: "Active", value: true },
			{ label: "Inactive", value: false }
		];
		console.table("status", this.statusDDL);
	}

	toggleFilter() {
		this.searchCriteriaDTO = new PurchasesBillSearchCriteriaDTO();
		this.showFilterControls = !this.showFilterControls;
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
		this.confirmationDialogService.confirm(this.translate.instant("ConfirmaionDialog.Title"), this.translate.instant("ConfirmaionDialog.Description"))
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
