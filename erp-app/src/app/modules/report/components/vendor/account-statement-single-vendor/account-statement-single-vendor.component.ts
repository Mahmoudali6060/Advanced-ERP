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
import { PurchasesBillService } from 'src/app/modules/purchases/services/purchases-bill.service';
import { TreasuryDTO } from 'src/app/modules/accounting/models/treasury.dto';
import { PaymentMethodEnum } from 'src/app/shared/enums/payment-method.enum';
import { AccountTypeEnum } from 'src/app/shared/enums/account-type.enum';
import { TreasurySearchDTO } from 'src/app/modules/accounting/models/treasury-search.dto';
import { TreasuryService } from 'src/app/modules/accounting/services/treasury.service';

@Component({
	selector: 'app-account-statement-single-vendor',
	templateUrl: './account-statement-single-vendor.component.html',
	styleUrls: ['./account-statement-single-vendor.component.css']


})
export class AccountStatementSingleVendorComponent {
	vendorList: Array<ClientVendorDTO>;
	selectedVendor: ClientVendorDTO = new ClientVendorDTO();
	selectedVendorId: number;
	clientVendorBalanceList: Array<TreasuryDTO>;
	currentBalance: number = 0;
	paymentMethodEnum = PaymentMethodEnum;

	constructor(private clientVendorService: ClientVendorService,
		private confirmationDialogService: DialogService,
		private toastrService: ToastrService,
		private translate: TranslateService,
		private _configService: ConfigService,
		private reportService: ReportService,
		private treasuryService: TreasuryService) {

	}

	ngOnInit() {
		this.getAllVendors();
	}



	onClientChange() {
		if (this.selectedVendorId) {
			let selectedVendor: any = this.vendorList.find(c => c.id == this.selectedVendorId);
			if (selectedVendor) {
				this.selectedVendor = selectedVendor;
				this.currentBalance = this.selectedVendor.debit - this.selectedVendor.credit;
			}
		}

	}


	getAllVendors() {
		this.clientVendorService.getAllLiteByTypeId(ClientVendorTypeEnum.Vendor).subscribe((res: any) => {
			this.vendorList = res.list;
		})
	}

	getAllByVendorId(isPrint?: boolean) {
		if (this.selectedVendorId) {
			let searchCriteria: TreasurySearchDTO = new TreasurySearchDTO();
			searchCriteria.pageSize = 1000000;
			searchCriteria.accountTypeId = AccountTypeEnum.Vendors;
			searchCriteria.clientVendorId = this.selectedVendorId;
			this.treasuryService.getAll(searchCriteria).subscribe((res: any) => {
				this.clientVendorBalanceList = res.list;
				if (isPrint)
					setTimeout(() => {
						this.print();
					}, 300);
			});
		}
		else {
			this.selectedVendor = new ClientVendorDTO();
			this.currentBalance = 0;
			this.clientVendorBalanceList = [];
		}
	}

	searchAndPrint() {
		this.getAllByVendorId(true);
	}

	print() {

		let div: any = document.getElementById('accountStatementSingleVendor');
		this.reportService.print(this.translate.instant("Reports.AccountStatementForSingleVendor"), div);
	}


}
