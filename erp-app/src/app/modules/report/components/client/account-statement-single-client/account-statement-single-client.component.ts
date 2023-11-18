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
import { ClientVendorBalanceDTO } from 'src/app/modules/setup/models/client-vendor-balance.dto';
import { SalesBillService } from 'src/app/modules/sales/services/sales-bill.service';

@Component({
	selector: 'app-account-statement-single-client',
	templateUrl: './account-statement-single-client.component.html',
	styleUrls: ['./account-statement-single-client.component.css']


})
export class AccountStatementSingleClientComponent {
	clientList: Array<ClientVendorDTO>;
	selectedClient: ClientVendorDTO = new ClientVendorDTO();
	selectedClientId: number;
	clientVendorBalanceList: Array<ClientVendorBalanceDTO>;
	currentBalance: number = 0;
	constructor(private clientVendorService: ClientVendorService,
		private confirmationDialogService: DialogService,
		private toastrService: ToastrService,
		private translate: TranslateService,
		private _configService: ConfigService,
		private reportService: ReportService,
		private salesBillService: SalesBillService) {

	}

	ngOnInit() {
		this.getAllClients();
	}



	onClientChange() {
		if (this.selectedClientId) {
			let selectedClient: any = this.clientList.find(c => c.id == this.selectedClientId);
			if (selectedClient) {
				this.selectedClient = selectedClient;
				this.currentBalance = this.selectedClient.debit - this.selectedClient.credit;
			}
		}

	}


	getAllClients() {
		this.clientVendorService.getAllLiteByTypeId(ClientVendorTypeEnum.Client).subscribe((res: any) => {
			this.clientList = res.list;
		})
	}

	getAllByClientId(isPrint?: boolean) {
		if (this.selectedClientId) {
			this.salesBillService.getAllByClientId(this.selectedClient.id).subscribe((res: any) => {
				this.clientVendorBalanceList = res;
				if (isPrint)
					setTimeout(() => {                          
						this.print();
					}, 300);
			});
		}
		else {
			this.selectedClient = new ClientVendorDTO();
			this.currentBalance = 0;
			this.clientVendorBalanceList = [];
		}
	}

	searchAndPrint() {
		this.getAllByClientId(true);
	}

	print() {

		let div: any = document.getElementById('accountStatementSingleClient');
		this.reportService.print(this.translate.instant("Reports.AccountStatementForSingleClient"), div);
	}


}
