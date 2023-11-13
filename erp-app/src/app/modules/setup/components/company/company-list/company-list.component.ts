import { Component, ViewChild, ViewEncapsulation } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { PagingDTO } from '../../../../../shared/models/paging-dto';
import { DialogService } from '../../../../../shared/services/confirmation-dialog.service';
import { CompanyService } from '../../../services/company.service';
import { ConfigService } from '../../../../../shared/services/config.service';
import { PaginationComponent } from 'src/app/shared/components/pagination/pagination.component';
import { CompanyDTO } from '../../../models/company-dto';
import { CompanySearchDTO } from '../../../models/company-search-dto';

@Component({
	selector: 'app-company-list',
	templateUrl: './company-list.component.html',
	styleUrls: ['./company-list.component.css']
})

export class CompanyListComponent {
	@ViewChild(PaginationComponent) paginationComponent: PaginationComponent;
	dataSource: PagingDTO = new PagingDTO();
	companyList: Array<CompanyDTO>;
	serverUrl: string;
	showFilterControls: boolean = false;
	searchCriteriaDTO: CompanySearchDTO = new CompanySearchDTO()
	total: number;
	statusDDL: any;

	constructor(private companyService: CompanyService,
		private confirmationDialogService: DialogService,
		private toastrService: ToastrService,
		private translate: TranslateService,
		private _configService: ConfigService
	) {

	}

	ngOnInit() {
		this.search();
		this.statusDDL = [
			{ label: "All", value: '' },
			{ label: "Active", value: true },
			{ label: "Inactive", value: false }
		];
		console.table("status", this.statusDDL);
	}

	toggleFilter() {
		this.searchCriteriaDTO = new CompanySearchDTO();
		this.showFilterControls = !this.showFilterControls;
	}

	getAllCompanies() {
		this.companyService.getAll(this.searchCriteriaDTO).subscribe((res: any) => {
			this.companyList = res.list;
			this.total = res.total;
			if (this.paginationComponent) {
				this.paginationComponent.totalRecordsCount = this.total;
				this.paginationComponent.setPagination(this.searchCriteriaDTO.page);
			}
			this.serverUrl = this._configService.getServerUrl();
		});
	}

	search() {
		this.getAllCompanies();
	}

	delete(id: any) {
		this.companyService.delete(id).subscribe((res: any) => {
			if (res) {
				this.toastrService.success(this.translate.instant("Message.DeletedSuccessfully"));
				this.getAllCompanies();
			}
		});
	}

	public openConfirmationDialog(item: CompanyDTO) {
		this.confirmationDialogService.confirm(this.translate.instant("ConfirmaionDialog.Title"), this.translate.instant("ConfirmaionDialog.Description"))
			.then((confirmed) => {
				if (confirmed) {
					this.delete(item.id);
				}
			})
			.catch(() => console.log('Company dismissed the dialog (e.g., by using ESC, clicking the cross icon, or clicking outside the dialog)'));
	}

	onPageChange(event: any) {
		this.searchCriteriaDTO.page = event;
		this.getAllCompanies();
	}
}
