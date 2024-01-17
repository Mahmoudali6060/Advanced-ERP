import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { Location } from '@angular/common';
import { ConfigService } from 'src/app/shared/services/config.service';
import { NgForm } from '@angular/forms';
import { HelperService } from 'src/app/shared/services/helper.service';
import { SubjectService } from 'src/app/shared/services/subject.service';
import { LocalStorageService } from 'src/app/shared/services/local-storage.service';
import { TreasuryDTO } from '../../../models/treasury.dto';
import { TreasuryService } from '../../../services/treasury.service';
import { ClientVendorService } from 'src/app/modules/setup/services/client-vendor.service';
import { LabelValuePair } from 'src/app/shared/enums/label-value-pair';
import { AccountTypeEnum } from 'src/app/shared/enums/account-type.enum';
import { PaymentMethodEnum } from 'src/app/shared/enums/payment-method.enum';
import { TransactionTypeEnum } from 'src/app/shared/enums/transaction-type.enum';
import { ClientVendorDTO, ClientVendorTypeEnum } from 'src/app/modules/setup/models/client-vendor.dto';
import { ReportService } from 'src/app/modules/report/services/report.service';

@Component({
	selector: 'app-treasury-form',
	templateUrl: './treasury-form.component.html',
	styleUrls: ['./treasury-form.component.css']
})
export class TreasuryFormComponent {

	treasuryDTO: TreasuryDTO = new TreasuryDTO();
	viewMode: boolean = false;
	accountTypeList: LabelValuePair[];
	paymentMethodList: LabelValuePair[];
	transactionTypeList: LabelValuePair[];
	clientVendorList: Array<ClientVendorDTO> = new Array<ClientVendorDTO>();
	accountTypeEnum = AccountTypeEnum;
	paymentMethodEnum = PaymentMethodEnum;
	currentBalance: number = 0;
	previousBalance: number = 0;
	disableButton: boolean = false;

	constructor(
		private treasuryService: TreasuryService,
		private clientVendorService: ClientVendorService,
		private route: ActivatedRoute,
		private toasterService: ToastrService,
		private _configService: ConfigService,
		private helperService: HelperService,
		private reportService: ReportService,
		private translate: TranslateService,
		private router: Router) {
	}

	ngOnInit() {
		this.accountTypeList = this.helperService.enumSelector(AccountTypeEnum);
		this.paymentMethodList = this.helperService.enumSelector(PaymentMethodEnum);
		this.transactionTypeList = this.helperService.enumSelector(TransactionTypeEnum);
		this.treasuryDTO = new TreasuryDTO();
		this.treasuryDTO.date = this.helperService.conveertDateToString(new Date());

		const id = this.route.snapshot.paramMap.get('id');
		if (id) {
			this.getTreasuryById(id);
			if (this.router.url.includes('view')) {
				this.viewMode = true;
			}
		}

	}

	getAllClientVendors() {
		this.clientVendorService.getAllLite().subscribe((res: any) => {
			this.clientVendorList = res.list;
			if (this.treasuryDTO.accountTypeId == AccountTypeEnum.Clients) {
				this.clientVendorList = this.clientVendorList.filter(x => x.typeId == ClientVendorTypeEnum.All || x.typeId == ClientVendorTypeEnum.Client)
			}
			else if (this.treasuryDTO.accountTypeId == AccountTypeEnum.Vendors) {
				this.clientVendorList = this.clientVendorList.filter(x => x.typeId == ClientVendorTypeEnum.All || x.typeId == ClientVendorTypeEnum.Vendor)
			}
			this.treasuryDTO.beneficiaryName = this.clientVendorList.find(x => x.id == this.treasuryDTO.clientVendorId)?.fullName;
			this.onClientVendorChange();
		})
	}

	getTreasuryById(treasuryId: any, isPrint?: boolean) {
		this.treasuryService.getById(treasuryId).subscribe((res: any) => {
			this.treasuryDTO = res;
			if (isPrint) {
				this.print();
				this.back();
			}
			if (this.treasuryDTO.accountTypeId != AccountTypeEnum.Other)
				this.getAllClientVendors();
		})
	}

	handleChange(event: boolean) {
		// this.treasuryDTO.isActive = event.target
	}

	back() {
		this.router.navigateByUrl('accounting/treasury-list');
	}
	validattion(treasuryDTO: TreasuryDTO): boolean {
		// if (!treasuryDTO.firstName || isNullOrUndefined(treasuryDTO.firstName)) {
		// 	this.toasterService.error(this.translate.instant("Errors.FirstNameIsRequired"));
		// 	return false;
		//   }
		//   if (!treasuryDTO.lastName || isNullOrUndefined(treasuryDTO.lastName)) {
		// 	this.toasterService.error(this.translate.instant("Errors.LastNameIsRequired"));
		// 	return false;
		//   }
		//   if (!treasuryDTO.mobile || isNullOrUndefined(treasuryDTO.mobile)) {
		// 	this.toasterService.error(this.translate.instant("Errors.MobileIsRequired"));
		// 	return false;
		//   }
		//   if (!treasuryDTO.treasuryName || isNullOrUndefined(treasuryDTO.treasuryName)) {
		// 	this.toasterService.error(this.translate.instant("Errors.TreasuryNameIsRequired"));
		// 	return false;
		//   }
		//   if (!treasuryDTO.email || isNullOrUndefined(treasuryDTO.email)) {
		// 	this.toasterService.error(this.translate.instant("Errors.EmailIsRequired"));
		// 	return false;
		//   }
		return true;

	}

	save(isPrint?: boolean) {
		if (this.validattion(this.treasuryDTO)) {
			this.disableButton = true;
			if (this.treasuryDTO.id) {
				this.treasuryService.update(this.treasuryDTO).subscribe(res => {
					this.toasterService.success("success");
					this.disableButton = false;
					if (isPrint) {
						this.print();
					}
					this.back();
				})
			}
			else {
				this.treasuryService.add(this.treasuryDTO).subscribe(res => {
					this.toasterService.success("success");
					this.disableButton = false;
					if (isPrint) {
						this.getTreasuryById(res, isPrint);
					}
					else {
						this.back();
					}
				})
			}
		}
	}

	onAccountTypeChange() {
		this.treasuryDTO.clientVendorId = null;
		this.getAllClientVendors();
	}

	onClientVendorChange() {
		if (this.treasuryDTO.clientVendorId) {
			let clientVendor: any | undefined = this.clientVendorList.find(x => x.id == this.treasuryDTO.clientVendorId);
			this.treasuryDTO.beneficiaryName = clientVendor?.fullName;
			this.updateBalance();
		}
	}

	updateBalance() {
		let clientVendor: any | undefined = this.clientVendorList.find(x => x.id == this.treasuryDTO.clientVendorId);
		//Edit
		if (this.treasuryDTO.id) {
			// this.currentBalance = parseFloat((clientVendor?.debit - clientVendor?.credit).toFixed(2));
			// this.previousBalance = parseFloat((this.currentBalance - (clientVendor?.debit - clientVendor?.credit)).toFixed(2));

			this.currentBalance = parseFloat((clientVendor?.debit - clientVendor?.credit).toFixed(2));
			this.previousBalance = parseFloat((this.currentBalance + this.treasuryDTO?.debit + this.treasuryDTO?.credit).toFixed(2));

		}
		//Add
		else {
			this.previousBalance = parseFloat((clientVendor?.debit - clientVendor?.credit).toFixed(2));
			this.currentBalance = parseFloat((this.previousBalance - this.treasuryDTO?.debit - this.treasuryDTO?.credit).toFixed(2));

		}
	}

	onAmountsChange() {
		this.currentBalance = parseFloat((this.previousBalance + this.treasuryDTO?.debit - this.treasuryDTO?.credit).toFixed(2));
	}
	saveAndPrint() {
		this.save(true);

	}

	print() {
		let number: any = document.getElementById('treasuryNumber');
		number.innerHTML = this.treasuryDTO.number;
		let div: any = document.getElementById('treasury-form');
		this.reportService.print(this.translate.instant("Reports.TreasuryForm"), div);
	}

}
