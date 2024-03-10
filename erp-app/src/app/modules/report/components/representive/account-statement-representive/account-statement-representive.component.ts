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
import { AccountStatementService } from 'src/app/modules/accounting/services/account-statement.service';
import { TreasurySearchDTO } from 'src/app/modules/accounting/models/treasury-search.dto';
import { AccountTypeEnum } from 'src/app/shared/enums/account-type.enum';
import { PaymentMethodEnum } from 'src/app/shared/enums/payment-method.enum';
import { AccountStatementDTO } from 'src/app/modules/accounting/models/account-statement-dto';
import { RepresentiveDTO } from 'src/app/modules/setup/models/representive.dto';
import { RepresentiveService } from 'src/app/modules/setup/services/representive.service';
import { AccountStatementSearchDTO } from 'src/app/modules/accounting/models/account-statement-search.dto';
import { BillTypeEnum } from 'src/app/shared/enums/bill-type.enum';

@Component({
	selector: 'app-account-statement-representive',
	templateUrl: './account-statement-representive.component.html',
	styleUrls: ['./account-statement-representive.component.css']


})
export class AccountStatementRepresentiveComponent {
	clientList: Array<ClientVendorDTO>;
	selectedClient: ClientVendorDTO = new ClientVendorDTO();
	selectedRepresentive: RepresentiveDTO = new RepresentiveDTO();
	selectedClientId: number;
	selectedRepresentiveId: number;
	clientVendorBalanceList: Array<AccountStatementDTO>;
	currentBalance: number = 0;
	paymentMethodEnum = PaymentMethodEnum
	billType = BillTypeEnum;
	representiveList: Array<RepresentiveDTO> = new Array<RepresentiveDTO>();

	constructor(private clientVendorService: ClientVendorService,
		private confirmationDialogService: DialogService,
		private toastrService: ToastrService,
		private translate: TranslateService,
		private _configService: ConfigService,
		private reportService: ReportService,
		private representiveService: RepresentiveService,
		private accountStatementService: AccountStatementService) {

	}

	ngOnInit() {
		this.getAllClients();
		this.getAllRepresentives();
	}

	onRepresentiveChange() {
		if (this.selectedRepresentiveId) {
			let selectedRepresentive: any = this.representiveList.find(c => c.id == this.selectedRepresentiveId);
			if (selectedRepresentive) {
				this.selectedRepresentive = selectedRepresentive;
			}
		}
	}


	getAllClients() {
		this.clientVendorService.getAllLiteByTypeId(ClientVendorTypeEnum.Client).subscribe((res: any) => {
			this.clientList = res.list;
		})
	}

	getAllRepresentives() {
		this.representiveService.getAllLite().subscribe((res: any) => {
			this.representiveList = res.list;
		})
	}


	getAllByClientId(isPrint?: boolean) {
		// if (this.selectedClientId) {
		let searchCriteria: AccountStatementSearchDTO = new AccountStatementSearchDTO();
		searchCriteria.pageSize = 1000000;
		searchCriteria.clientVendorId = this.selectedClientId;
		searchCriteria.representiveId = this.selectedRepresentiveId;
		this.accountStatementService.getAll(searchCriteria).subscribe((res: any) => {
			this.clientVendorBalanceList = res.list;
			if (isPrint)
				setTimeout(() => {
					this.print();
				}, 300);
		});
		// }
		// else {
		// 	this.selectedClient = new ClientVendorDTO();
		// 	this.currentBalance = 0;
		// 	this.clientVendorBalanceList = [];
		// }
	}

	searchAndPrint() {
		this.getAllByClientId(true);
	}

	print() {

		let div: any = document.getElementById('accountStatementSingleClient');
		this.reportService.print(this.translate.instant("Reports.AccountStatementForSingleClient"), div);
	}


}
