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
import { ClientVendorDTO, ClientVendorTypeEnum } from 'src/app/modules/setup/models/client-vendor.dto';
import { ClientVendorSearchCriteriaDTO } from 'src/app/modules/setup/models/client-vendor-search-criteria-dto';
import { ClientVendorService } from 'src/app/modules/setup/services/client-vendor.service';
import { ReportService } from '../../../services/report.service';
import { LabelValuePair } from 'src/app/shared/enums/label-value-pair';
import { AccountStatusEnum } from 'src/app/shared/enums/account-status.enum';

@Component({
	selector: 'app-account-statement-all-vendors',
	templateUrl: './account-statement-all-vendors.component.html',
	styleUrls: ['./account-statement-all-vendors.component.css']


})
export class AccountStatementAllVendorsComponent {
	dataSource: PagingDTO = new PagingDTO();
	vendorList: Array<ClientVendorDTO>;
	serverUrl: string;
	showFilterControls: boolean = false;
	searchCriteriaDTO: ClientVendorSearchCriteriaDTO = new ClientVendorSearchCriteriaDTO()
	total: number;
	statusList: Array<string> | Boolean
	statusDDL: any;
	accountStatusList: LabelValuePair[];

	constructor(private clientVendorService: ClientVendorService,
		private confirmationDialogService: DialogService,
		private toastrService: ToastrService,
		private translate: TranslateService,
		private _configService: ConfigService,
		private SpinnerService: NgxSpinnerService,
		public helperService: HelperService,
		private reportService: ReportService) {

	}

	ngOnInit() {
		this.searchCriteriaDTO.pageSize = 1000000;//To get All data 
		this.searchCriteriaDTO.accountStatusId = AccountStatusEnum.All;
		this.accountStatusList = this.helperService.enumSelector(AccountStatusEnum);
		this.search();
		this.statusDDL = [
			{ label: "All", value: '' },
			{ label: "Active", value: true },
			{ label: "Inactive", value: false }
		];
		console.table("status", this.statusDDL);
	}

	toggleFilter() {
		this.searchCriteriaDTO = new ClientVendorSearchCriteriaDTO();
		this.showFilterControls = !this.showFilterControls;
		this.searchCriteriaDTO.accountStatusId = AccountStatusEnum.All;
		this.searchCriteriaDTO.pageSize = 1000000;//To get All data 
	}

	getAllVendors() {
		this.searchCriteriaDTO.typeId = ClientVendorTypeEnum.Vendor;
		this.clientVendorService.getAll(this.searchCriteriaDTO).subscribe((res: any) => {
			this.vendorList = res.list;
			this.total = res.total;
			this.serverUrl = this._configService.getServerUrl();
		});
	}

	search() {
		this.getAllVendors();
	}

	delete(id: any) {
		this.clientVendorService.delete(id).subscribe((res: any) => {
			if (res) {
				this.toastrService.success(this.translate.instant("Message.DeletedSuccessfully"));
				this.getAllVendors();
			}
		});
	}

	print() {
		let div: any = document.getElementById('accountStatementAllVendors');
		this.reportService.print(this.translate.instant("Reports.AccountStatementForAllVendors"), div);
	}

	public openConfirmationDialog(item: ClientVendorDTO) {
		this.confirmationDialogService.confirm(this.translate.instant("DeleteConfirmaionDialog.Title"), this.translate.instant("DeleteConfirmaionDialog.Description"))
			.then((confirmed) => {
				if (confirmed) {
					this.delete(item.id);
				}
			})
			.catch(() => console.log('Client dismissed the dialog (e.g., by using ESC, clicking the cross icon, or clicking outside the dialog)'));
	}

	onPageChange(event: any) {
		this.searchCriteriaDTO.page = event;
		this.getAllVendors();
	}
}
