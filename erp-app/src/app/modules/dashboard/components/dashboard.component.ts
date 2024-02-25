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
import { DashboardDTO } from '../models/dashboard-dto';
import { DashboardService } from '../services/dashboard.service';
import { DashboardSearchDTO } from '../models/dashboard-search-dto';
declare var jQuery: any;

@Component({
	selector: 'app-dashboard',
	templateUrl: './dashboard.component.html',
	styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent {

	dashboardDTO: DashboardDTO = new DashboardDTO();
	dashboardSearchDTO: DashboardSearchDTO = new DashboardSearchDTO();


	constructor(
		public authService: AuthService,
		private dashboardService: DashboardService) {

	}
	ngOnInit() {
		this.getDashboard();
	}


	getDashboard() {
		this.dashboardService.getDashboard(this.dashboardSearchDTO).subscribe((res: any) => {
			this.dashboardDTO = res;
		})
	}


}
