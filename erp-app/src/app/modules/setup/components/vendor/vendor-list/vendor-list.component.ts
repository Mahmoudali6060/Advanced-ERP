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
import { ClientVendorService } from '../../../services/client-vendor.service';
import { ClientVendorDTO, ClientVendorTypeEnum } from '../../../models/client-vendor.dto';
import { ClientVendorSearchCriteriaDTO } from '../../../models/client-vendor-search-criteria-dto';

@Component({
	selector: 'app-vendor-list',
	templateUrl: './vendor-list.component.html',
	styleUrls: ['./vendor-list.component.css']


})
export class VendorListComponent {
	@ViewChild(PaginationComponent) paginationComponent: PaginationComponent;
	dataSource: PagingDTO = new PagingDTO();
	vendorList: Array<ClientVendorDTO>;
	serverUrl: string;
	showFilterControls: boolean = false;
	searchCriteriaDTO: ClientVendorSearchCriteriaDTO = new ClientVendorSearchCriteriaDTO()
	total: number;
	recordsPerPage: number = 5;
	vendorTypeEnum: any;
	countryList: Array<CountryModel> = new Array<CountryModel>();
	stateList: Array<StateModel> = new Array<StateModel>();
	cityList: Array<CityModel> = new Array<CityModel>();
	statusList: Array<string> | Boolean
	statusDDL: any;

	constructor(private clientVendorService: ClientVendorService,
		private confirmationDialogService: DialogService,
		private toastrService: ToastrService,
		private translate: TranslateService,
		private _configService: ConfigService,
		private SpinnerService: NgxSpinnerService,
		public helperService: HelperService) {

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
		this.searchCriteriaDTO = new ClientVendorSearchCriteriaDTO();
		this.showFilterControls = !this.showFilterControls;
	}

	getAllVendors() {
		this.searchCriteriaDTO.typeId = ClientVendorTypeEnum.Vendor;
		this.clientVendorService.getAll(this.searchCriteriaDTO).subscribe((res: any) => {
			this.vendorList = res.list;
			this.total = res.total;
			if (this.paginationComponent) {
				this.paginationComponent.totalRecordsCount = this.total;
				this.paginationComponent.setPagination(this.searchCriteriaDTO.page);
			}
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

	public openConfirmationDialog(item: ClientVendorDTO) {
		this.confirmationDialogService.confirm(this.translate.instant("DeleteConfirmaionDialog.Title"), this.translate.instant("DeleteConfirmaionDialog.Description"))
			.then((confirmed) => {
				if (confirmed) {
					this.delete(item.id);
				}
			})
			.catch(() => console.log('Vendor dismissed the dialog (e.g., by using ESC, clicking the cross icon, or clicking outside the dialog)'));
	}

	onPageChange(event: any) {
		this.searchCriteriaDTO.page = event;
		this.getAllVendors();
	}
}
