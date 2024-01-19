import { Component, ViewChild, ViewEncapsulation } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { PaginationComponent } from 'src/app/shared/components/pagination/pagination.component';
import { ConfigService } from 'src/app/shared/services/config.service';
import { HelperService } from 'src/app/shared/services/helper.service';
import { PagingDTO } from '../../../../../shared/models/paging-dto';
import { DialogService } from '../../../../../shared/services/confirmation-dialog.service';
import { CategoryService } from '../../../services/category.service';
import { CountryModel } from 'src/app/modules/configurations/models/country.model';
import { StateModel } from 'src/app/modules/configurations/models/state.model';
import { CityModel } from 'src/app/modules/configurations/models/city.model';
import { CategoryDTO } from '../../../models/category.dto';
import { CategorySearchCriteriaDTO } from '../../../models/category-search-criteria-dto';

@Component({
	selector: 'app-category-list',
	templateUrl: './category-list.component.html',
	styleUrls: ['./category-list.component.css']


})
export class CategoryListComponent {
	
	@ViewChild(PaginationComponent) paginationComponent: PaginationComponent;
	dataSource: PagingDTO = new PagingDTO();
	categoryList: Array<CategoryDTO>;
	serverUrl: string;
	showFilterControls: boolean = false;
	searchCriteriaDTO: CategorySearchCriteriaDTO = new CategorySearchCriteriaDTO()
	total: number;
	recordsPerPage: number = 5;
	categoryTypeEnum: any;
	countryList: Array<CountryModel> = new Array<CountryModel>();
	stateList: Array<StateModel> = new Array<StateModel>();
	cityList: Array<CityModel> = new Array<CityModel>();
	statusList: Array<string> | Boolean
	statusDDL: any;

	constructor(private categoryService: CategoryService,
		private confirmationDialogService: DialogService,
		private toastrService: ToastrService,
		public translate: TranslateService,
		public helperService:HelperService,
		private _configService: ConfigService,
	) {

	}

	ngOnInit() {
		this.search();
		this.statusDDL = [
			{ label: "All", value: '' },
			{ label: "Active", value: true },
			{ label: "Inactive", value: false }
		];
	}

	toggleFilter() {
		this.searchCriteriaDTO = new CategorySearchCriteriaDTO();
		this.showFilterControls = !this.showFilterControls;
	}

	getAllCategories() {
		this.categoryService.getAll(this.searchCriteriaDTO).subscribe((res: any) => {
			this.categoryList = res.list;
			this.total = res.total;
			if (this.paginationComponent) {
				this.paginationComponent.totalRecordsCount = this.total;
				this.paginationComponent.setPagination(this.searchCriteriaDTO.page);
			}
			this.serverUrl = this._configService.getServerUrl();
		});
	}

	search() {
		this.getAllCategories();
	}

	delete(id: any) {
		this.categoryService.delete(id).subscribe((res: any) => {
			if (res) {
				this.toastrService.success(this.translate.instant("Message.DeletedSuccessfully"));
				this.getAllCategories();
			}
		});
	}

	public openConfirmationDialog(item: CategoryDTO) {
		this.confirmationDialogService.confirm(this.translate.instant("ConfirmaionDialog.Title"), this.translate.instant("ConfirmaionDialog.Description"))
			.then((confirmed) => {
				if (confirmed) {
					this.delete(item.id);
				}
			})
			.catch(() => console.log('Category dismissed the dialog (e.g., by using ESC, clicking the cross icon, or clicking outside the dialog)'));
	}

	onPageChange(event: any) {
		this.searchCriteriaDTO.page = event;
		this.getAllCategories();
	}
}
