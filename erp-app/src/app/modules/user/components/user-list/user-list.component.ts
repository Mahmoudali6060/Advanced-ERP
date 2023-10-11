import { Component, ViewChild, ViewEncapsulation } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { PaginationComponent } from 'src/app/shared/components/pagination/pagination.component';
import { LabelValuePair } from 'src/app/shared/enums/label-value-pair';
import { ConfigService } from 'src/app/shared/services/config.service';
import { HelperService } from 'src/app/shared/services/helper.service';
import { DataSourceModel } from '../../../../shared/models/data-source.model';
import { ConfirmationDialogService } from '../../../../shared/services/confirmation-dialog.service';
import {  UserProfileSearchCriteriaDTO } from '../../models/user-list-search-criteria-dto';
import { UserProfileDTO } from '../../models/user-profile.dto';
import { UserTypeEnum } from '../../models/user-type-enum';
import { UserProfileService } from '../../services/user.service';
import { CountryModel } from 'src/app/modules/configurations/models/country.model';
import { StateModel } from 'src/app/modules/configurations/models/state.model';
import { CityModel } from 'src/app/modules/configurations/models/city.model';

@Component({
	selector: 'app-user-list',
	templateUrl: './user-list.component.html',
	styleUrls: ['./user-list.component.css']


})
export class UserListComponent {
	@ViewChild(PaginationComponent) paginationComponent: PaginationComponent;
	dataSource: DataSourceModel = new DataSourceModel();
	userList: Array<UserProfileDTO> ;
	serverUrl: string;
	showFilterControls: boolean = false;
	searchCriteriaDTO:UserProfileSearchCriteriaDTO = new UserProfileSearchCriteriaDTO()
	total: number;
	recordsPerPage: number = 5;
	userTypeEnum:any;
	userTypes:LabelValuePair[];
	countryList: Array<CountryModel> = new Array<CountryModel>();
	stateList: Array<StateModel> = new Array<StateModel>();
	cityList: Array<CityModel> = new Array<CityModel>();
	statusList : Array<string> | Boolean

	statusDDL: any;

	constructor(private userProfileService: UserProfileService,
		private confirmationDialogService: ConfirmationDialogService,
		private toastrService: ToastrService,
		private translate: TranslateService,
		private _configService: ConfigService,
		private SpinnerService: NgxSpinnerService,
		private helperService:HelperService) {

	}

	ngOnInit() {
		this.userTypeEnum = UserTypeEnum;
		this.searchCriteriaDTO.isActive = true;
		this.userTypes = this.helperService.enumSelector(this.userTypeEnum);
		this.search();
		this.statusDDL= [
			{ label: "All", value:  '' },
			{ label: "Active", value: true },
			{ label: "Inactive", value: false }
		  ];
		console.table("status",this.statusDDL);
	}
	toggleFilter() {
	    this.searchCriteriaDTO = new UserProfileSearchCriteriaDTO();
		this.showFilterControls = !this.showFilterControls;
	}
	getAllUsers() {
		//this.SpinnerService.show();
		this.userProfileService.getAll(this.searchCriteriaDTO).subscribe((res: any) => {
			this.userList = res.list;
			this.total = res.total;
			if (this.paginationComponent) {
				this.paginationComponent.totalRecordsCount = this.total;
				this.paginationComponent.setPagination(this.searchCriteriaDTO.page);
			}
			this.serverUrl = this._configService.getServerUrl();
		});
	}
    search(){
		this.getAllUsers();
	}
	delete(id: any) {
		this.userProfileService.delete(id).subscribe((res: any) => {
			if (res) {
				this.toastrService.success(this.translate.instant("Message.DeletedSuccessfully"));
				this.getAllUsers();
			}
		});
	}
	public openConfirmationDialog(item: UserProfileDTO) {
		this.confirmationDialogService.confirm(this.translate.instant("ConfirmaionDialog.Title"), this.translate.instant("ConfirmaionDialog.Description"))
			.then((confirmed) => {
				if (confirmed) {
					this.delete(item.id);
				}
			})
			.catch(() => console.log('User dismissed the dialog (e.g., by using ESC, clicking the cross icon, or clicking outside the dialog)'));
	}
	onPageChange(event: any) {
		this.searchCriteriaDTO.page = event;
		this.getAllUsers();
	}
}
