import { Component, ViewChild, ViewEncapsulation } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { PaginationComponent } from 'src/app/shared/components/pagination/pagination.component';
import { ConfigService } from 'src/app/shared/services/config.service';
import { HelperService } from 'src/app/shared/services/helper.service';
import { DataSourceModel } from '../../../../../shared/models/data-source.model';
import { ConfirmationDialogService } from '../../../../../shared/services/confirmation-dialog.service';
import { ClientService } from '../../../services/client.service';
import { CountryModel } from 'src/app/modules/configurations/models/country.model';
import { StateModel } from 'src/app/modules/configurations/models/state.model';
import { CityModel } from 'src/app/modules/configurations/models/city.model';
import { ClientDTO } from '../../../models/client.dto';
import { ClientSearchCriteriaDTO } from '../../../models/client-search-criteria-dto';

@Component({
	selector: 'app-client-list',
	templateUrl: './client-list.component.html',
	styleUrls: ['./client-list.component.css']


})
export class ClientListComponent {
	@ViewChild(PaginationComponent) paginationComponent: PaginationComponent;
	dataSource: DataSourceModel = new DataSourceModel();
	clientList: Array<ClientDTO>;
	serverUrl: string;
	showFilterControls: boolean = false;
	searchCriteriaDTO: ClientSearchCriteriaDTO = new ClientSearchCriteriaDTO()
	total: number;
	recordsPerPage: number = 5;
	clientTypeEnum: any;
	countryList: Array<CountryModel> = new Array<CountryModel>();
	stateList: Array<StateModel> = new Array<StateModel>();
	cityList: Array<CityModel> = new Array<CityModel>();
	statusList: Array<string> | Boolean
	statusDDL: any;

	constructor(private clientService: ClientService,
		private confirmationDialogService: ConfirmationDialogService,
		private toastrService: ToastrService,
		private translate: TranslateService,
		private _configService: ConfigService,
		private SpinnerService: NgxSpinnerService,
		private helperService: HelperService) {

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
		this.searchCriteriaDTO = new ClientSearchCriteriaDTO();
		this.showFilterControls = !this.showFilterControls;
	}

	getAllClients() {
		this.clientService.getAll(this.searchCriteriaDTO).subscribe((res: any) => {
			this.clientList = res.list;
			this.total = res.total;
			if (this.paginationComponent) {
				this.paginationComponent.totalRecordsCount = this.total;
				this.paginationComponent.setPagination(this.searchCriteriaDTO.page);
			}
			this.serverUrl = this._configService.getServerUrl();
		});
	}

	search() {
		this.getAllClients();
	}

	delete(id: any) {
		this.clientService.delete(id).subscribe((res: any) => {
			if (res) {
				this.toastrService.success(this.translate.instant("Message.DeletedSuccessfully"));
				this.getAllClients();
			}
		});
	}

	public openConfirmationDialog(item: ClientDTO) {
		this.confirmationDialogService.confirm(this.translate.instant("ConfirmaionDialog.Title"), this.translate.instant("ConfirmaionDialog.Description"))
			.then((confirmed) => {
				if (confirmed) {
					this.delete(item.id);
				}
			})
			.catch(() => console.log('Client dismissed the dialog (e.g., by using ESC, clicking the cross icon, or clicking outside the dialog)'));
	}

	onPageChange(event: any) {
		this.searchCriteriaDTO.page = event;
		this.getAllClients();
	}
}
