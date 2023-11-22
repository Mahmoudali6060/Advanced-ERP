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
import { ClientVendorDTO } from 'src/app/modules/setup/models/client-vendor.dto';

@Component({
	selector: 'app-treasury-form',
	templateUrl: './treasury-form.component.html',
	styleUrls: ['./treasury-form.component.css']
})
export class TreasuryFormComponent {

	treasuryDTO: TreasuryDTO = new TreasuryDTO();
	viewMode: boolean;
	accountTypeList: LabelValuePair[];
	paymentMethodList: LabelValuePair[];
	transactionTypeList: LabelValuePair[];
	clientVendorList: Array<ClientVendorDTO> = new Array<ClientVendorDTO>();
	accountTypeEnum = AccountTypeEnum;
	constructor(
		private treasuryService: TreasuryService,
		private clientVendorService: ClientVendorService,
		private route: ActivatedRoute,
		private toasterService: ToastrService,
		private _configService: ConfigService,
		private helperService: HelperService,
		private router: Router) {
	}

	ngOnInit() {
		this.accountTypeList = this.helperService.enumSelector(AccountTypeEnum);
		this.paymentMethodList = this.helperService.enumSelector(PaymentMethodEnum);
		this.transactionTypeList = this.helperService.enumSelector(TransactionTypeEnum);
		this.treasuryDTO = new TreasuryDTO();

		const id = this.route.snapshot.paramMap.get('id');
		if (id) {
			this.getTreasuryById(id);
			if (this.router.url.includes('/view/')) {
				this.viewMode = true;
			}
		}
		//this.getAllClientVendors();
	}

	getAllClientVendors() {
		this.clientVendorService.getAllLite().subscribe((res: any) => {
			this.clientVendorList = res.list;
		})
	}

	getTreasuryById(treasuryId: any) {
		this.treasuryService.getById(treasuryId).subscribe((res: any) => {
			this.treasuryDTO = res;
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

	save(form: NgForm) {
		if (this.validattion(this.treasuryDTO)) {
			if (this.treasuryDTO.id) {
				this.treasuryService.update(this.treasuryDTO).subscribe(res => {
					this.toasterService.success("success");
					this.back();
				})
			}
			else {
				this.treasuryService.add(this.treasuryDTO).subscribe(res => {
					this.toasterService.success("success");
					this.back();
				})
			}
		}
	}



}
