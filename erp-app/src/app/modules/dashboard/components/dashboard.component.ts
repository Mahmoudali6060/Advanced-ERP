import { DatePipe } from '@angular/common';
import { Component, ViewChild, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { PaginationComponent } from 'src/app/shared/components/pagination/pagination.component';
import { LocalStorageItems } from 'src/app/shared/constants/local-storage-items';
import { LocalStorageService } from 'src/app/shared/services/local-storage.service';
import { UserProfileSearchCriteriaDTO } from '../../user-management/models/user-list-search-criteria-dto';
import { CompanyTotalDetails } from '../../setup/models/company-total-details';
import { UserProfileDTO } from '../../user-management/models/user-profile.dto';
import { UserTypeEnum } from '../../user-management/models/user-type-enum';
import { UserProfileService } from '../../user-management/services/user.service';
import { CompanyService } from '../../user-management/services/company.service';
import { ConfigService } from 'src/app/shared/services/config.service';
import { AuthService } from '../../authentication/services/auth.service';
declare var jQuery: any;

@Component({
	selector: 'app-dashboard',
	templateUrl: './dashboard.component.html',
	styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent {
	searchCriteriaDTO: UserProfileSearchCriteriaDTO = new UserProfileSearchCriteriaDTO();
	@ViewChild(PaginationComponent) paginationComponent: PaginationComponent;

	totalUsers: any;
	totalRequests: number;
	baseRequestList: any;
	totalInprogress: number;
	inprogress: boolean;
	noOfRequests: number = 0;
	totalTrucksProvider: number;
	companyTotalDetails: CompanyTotalDetails = new CompanyTotalDetails();
	totalOffers: any;
	dataWithStatus: any;
	dataWithTypes: any;
	chartOptions: any;
	waitingForCorrection: any;
	approved: any;
	closed: any;
	waitingForBL: any;
	waitingForPaymnet: any;
	waitingForOriginalBL: any;
	userProfile: UserProfileDTO = new UserProfileDTO();
	userTypeEnum = UserTypeEnum;
	serverUrl: string;

	constructor(private userProfileService: UserProfileService,
		private datepipe: DatePipe,
		private companyService: CompanyService,
		private translate: TranslateService,
		private localStorageService: LocalStorageService,
		public authService: AuthService,
		private _configService: ConfigService) {

	}
	ngOnInit() {
		this.serverUrl = this._configService.getServerUrl();
		this.userProfile = this.localStorageService.getItem(LocalStorageItems.userProfile);
		this.getAllUsers();
		this.getAllCompanyTotalDetails();
	}
	getAllUsers() {
		this.userProfileService.getAll(this.searchCriteriaDTO).subscribe((res: any) => {
			this.totalUsers = res.total;
		});
	}

	getAllCompanyTotalDetails() {
		this.companyService.GetAllCompanyTotalDetails().subscribe((res: CompanyTotalDetails) => {
			this.companyTotalDetails = res
		})
	}

}
