import { Component, ViewChild, ViewEncapsulation } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { PaginationComponent } from 'src/app/shared/components/pagination/pagination.component';
import { LabelValuePair } from 'src/app/shared/enums/label-value-pair';
import { ConfigService } from 'src/app/shared/services/config.service';
import { HelperService } from 'src/app/shared/services/helper.service';
import { PagingDTO } from '../../../../../shared/models/paging-dto';
import { DialogService } from '../../../../../shared/services/confirmation-dialog.service';
import { CountryModel } from 'src/app/modules/configurations/models/country.model';
import { StateModel } from 'src/app/modules/configurations/models/state.model';
import { CityModel } from 'src/app/modules/configurations/models/city.model';
import { RoleDTO } from '../../../models/role.dto';
import { RoleService } from '../../../services/role.service';
import { RoleSearchDTO } from '../../../models/role-search.dto';

@Component({
	selector: 'app-role-list',
	templateUrl: './role-list.component.html',
	styleUrls: ['./role-list.component.css']


})
export class RoleListComponent {
	@ViewChild(PaginationComponent) paginationComponent: PaginationComponent;
	dataSource: PagingDTO = new PagingDTO();
	roleList: Array<RoleDTO>;
	showFilterControls: boolean = false;
	searchCriteriaDTO: RoleSearchDTO = new RoleSearchDTO()
	total: number;
	recordsPerPage: number = 5;
	statusDDL: any;

	constructor(private roleService: RoleService,
		private confirmationDialogService: DialogService,
		private toastrService: ToastrService,
		private translate: TranslateService,
		private _configService: ConfigService,
		private SpinnerService: NgxSpinnerService,
		public helperService: HelperService) {

	}

	ngOnInit() {
		this.searchCriteriaDTO.isActive = true;
		this.search();
		this.statusDDL = [
			{ label: "All", value: '' },
			{ label: "Active", value: true },
			{ label: "Inactive", value: false }
		];
		console.table("status", this.statusDDL);
	}
	toggleFilter() {
		this.searchCriteriaDTO = new RoleSearchDTO();
		this.showFilterControls = !this.showFilterControls;
	}
	getAllRoles() {
		//this.SpinnerService.show();
		this.roleService.getAll(this.searchCriteriaDTO).subscribe((res: any) => {
			this.roleList = res.list;
			this.total = res.total;
			if (this.paginationComponent) {
				this.paginationComponent.totalRecordsCount = this.total;
				this.paginationComponent.setPagination(this.searchCriteriaDTO.page);
			}
		});
	}
	search() {
		this.getAllRoles();
	}
	delete(id: any) {
		this.roleService.delete(id).subscribe((res: any) => {
			if (res) {
				this.toastrService.success(this.translate.instant("Message.DeletedSuccessfully"));
				this.getAllRoles();
			}
		});
	}
	public openConfirmationDialog(item: RoleDTO) {
		this.confirmationDialogService.confirm(this.translate.instant("DeleteConfirmaionDialog.Title"), this.translate.instant("DeleteConfirmaionDialog.Description"))
			.then((confirmed) => {
				if (confirmed) {
					this.delete(item.id);
				}
			})
			.catch(() => console.log('Role dismissed the dialog (e.g., by using ESC, clicking the cross icon, or clicking outside the dialog)'));
	}
	onPageChange(event: any) {
		this.searchCriteriaDTO.page = event;
		this.getAllRoles();
	}
}
