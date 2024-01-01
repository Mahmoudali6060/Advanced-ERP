import { Component, Input, ViewChild, ViewEncapsulation } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { PaginationComponent } from 'src/app/shared/components/pagination/pagination.component';
import { ConfigService } from 'src/app/shared/services/config.service';
import { DialogService } from '../../../../../shared/services/confirmation-dialog.service';
import { TreasuryService } from '../../../services/treasury.service';
import { TreasuryDTO } from '../../../models/treasury.dto';
import { TreasurySearchDTO } from '../../../models/treasury-search.dto';
import { AlertService } from 'src/app/shared/services/alert.service';
import { LabelValuePair } from 'src/app/shared/enums/label-value-pair';
import { ClientVendorDTO, ClientVendorTypeEnum } from 'src/app/modules/setup/models/client-vendor.dto';
import { AccountTypeEnum } from 'src/app/shared/enums/account-type.enum';
import { PaymentMethodEnum } from 'src/app/shared/enums/payment-method.enum';
import { TransactionTypeEnum } from 'src/app/shared/enums/transaction-type.enum';
import { HelperService } from 'src/app/shared/services/helper.service';
import { ClientVendorService } from 'src/app/modules/setup/services/client-vendor.service';
import { ReportService } from 'src/app/modules/report/services/report.service';

@Component({
	selector: 'app-treasury-list',
	templateUrl: './treasury-list.component.html',
	styleUrls: ['./treasury-list.component.css']


})
export class TreasuryListComponent {
	@ViewChild(PaginationComponent) paginationComponent: PaginationComponent;
	treasuryList: Array<TreasuryDTO>;
	showFilterControls: boolean = false;
	searchCriteriaDTO: TreasurySearchDTO = new TreasurySearchDTO()
	total: number;
	viewMode: boolean = false;
	accountTypeList: LabelValuePair[];
	paymentMethodList: LabelValuePair[];
	transactionTypeList: LabelValuePair[];
	clientVendorList: Array<ClientVendorDTO> = new Array<ClientVendorDTO>();
	accountTypeEnum = AccountTypeEnum;
	transactionTypeEnum = TransactionTypeEnum;
	paymentMethodEnum = PaymentMethodEnum;
	incomingTotal: number = 0;
	outcomingTotal: number = 0;

	constructor(private treasuryService: TreasuryService,
		private confirmationDialogService: DialogService,
		private toastrService: ToastrService,
		private translate: TranslateService,
		private helperService: HelperService,
		private clientVendorService: ClientVendorService,
		private reportService: ReportService) {

	}

	ngOnInit() {
		this.accountTypeList = this.helperService.enumSelector(AccountTypeEnum);
		this.paymentMethodList = this.helperService.enumSelector(PaymentMethodEnum);
		this.transactionTypeList = this.helperService.enumSelector(TransactionTypeEnum);
		this.search();
		this.getAllTreasuries();
	}

	toggleFilter() {
		this.searchCriteriaDTO = new TreasurySearchDTO();
		this.showFilterControls = !this.showFilterControls;
	}


	getAllClientVendors() {
		this.clientVendorService.getAllLite().subscribe((res: any) => {
			this.clientVendorList = res.list;
			if (this.searchCriteriaDTO.accountTypeId == AccountTypeEnum.Clients) {
				this.clientVendorList = this.clientVendorList.filter(x => x.typeId == ClientVendorTypeEnum.All || x.typeId == ClientVendorTypeEnum.Client)
			}
			else if (this.searchCriteriaDTO.accountTypeId == AccountTypeEnum.Vendors) {
				this.clientVendorList = this.clientVendorList.filter(x => x.typeId == ClientVendorTypeEnum.All || x.typeId == ClientVendorTypeEnum.Vendor)
			}

		})
	}


	getAllTreasuries() {
		this.treasuryService.getAll(this.searchCriteriaDTO).subscribe((res: any) => {
			this.treasuryList = res.list;
			this.total = res.total;
			if (this.paginationComponent) {
				this.paginationComponent.totalRecordsCount = this.total;
				this.paginationComponent.setPagination(this.searchCriteriaDTO.page);
			}
		});
	}


	search() {
		this.getAllTreasuries();
	}

	delete(id: any) {
		this.treasuryService.delete(id).subscribe((res: any) => {
			if (res) {
				this.toastrService.success(this.translate.instant("Message.DeletedSuccessfully"));
				this.getAllTreasuries();
			}
		});
	}

	public openConfirmationDialog(item: TreasuryDTO) {
		this.confirmationDialogService.confirm(this.translate.instant("ConfirmaionDialog.Title"), this.translate.instant("ConfirmaionDialog.Description"))
			.then((confirmed) => {
				if (confirmed) {
					this.delete(item.id);
				}
			})
			.catch(() => console.log('Treasury dismissed the dialog (e.g., by using ESC, clicking the cross icon, or clicking outside the dialog)'));
	}

	onPageChange(event: any) {
		this.searchCriteriaDTO.page = event;
		this.getAllTreasuries();
	}

	onAccountTypeChange() {
		if (this.searchCriteriaDTO.accountTypeId == AccountTypeEnum.Clients || this.searchCriteriaDTO.accountTypeId == AccountTypeEnum.Vendors) {
			this.getAllClientVendors();
		}

	}

	print() {
		this.incomingTotal = 0;
		this.outcomingTotal = 0;

		this.searchCriteriaDTO.pageSize = 1000000;
		this.treasuryService.getAll(this.searchCriteriaDTO).subscribe((res: any) => {
			this.treasuryList = res.list;
			for (let item of this.treasuryList) {
				this.incomingTotal += item.debit;
				this.outcomingTotal += item.credit;
			}

			setTimeout(() => {
				let div: any = document.getElementById('treasury-list');
				this.reportService.print(this.translate.instant("Reports.Treasury"), div);
				this.searchCriteriaDTO.pageSize = 20;
			}, 50);

		});

	}
}
