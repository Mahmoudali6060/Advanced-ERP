import { Component, ViewChild, ViewEncapsulation } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { PagingDTO } from '../../../../../shared/models/paging-dto';
import { DialogService } from '../../../../../shared/services/confirmation-dialog.service';
import { RepresentiveService } from '../../../services/representive.service';
import { ConfigService } from '../../../../../shared/services/config.service';
import { PaginationComponent } from 'src/app/shared/components/pagination/pagination.component';
import { RepresentiveDTO } from '../../../models/representive.dto';
import { RepresentiveSearchDTO } from '../../../models/representive-search.dto';
import { RepresentiveTypeEnum } from 'src/app/shared/enums/representive-type.enum';
import { LabelValuePair } from 'src/app/shared/enums/label-value-pair';
import { HelperService } from 'src/app/shared/services/helper.service';

@Component({
	selector: 'app-representive-list',
	templateUrl: './representive-list.component.html',
	styleUrls: ['./representive-list.component.css']
})

export class RepresentiveListComponent {
	@ViewChild(PaginationComponent) paginationComponent: PaginationComponent;
	dataSource: PagingDTO = new PagingDTO();
	representiveList: Array<RepresentiveDTO>;
	serverUrl: string;
	showFilterControls: boolean = false;
	searchCriteriaDTO: RepresentiveSearchDTO = new RepresentiveSearchDTO()
	total: number;
	statusDDL: any;
	representiveTypeId = RepresentiveTypeEnum;
	representiveTypelist: LabelValuePair[];

	constructor(private representiveService: RepresentiveService,
		private confirmationDialogService: DialogService,
		private toastrService: ToastrService,
		private translate: TranslateService,
		private _configService: ConfigService,
		private helperService: HelperService
	) {

	}

	ngOnInit() {
		this.representiveTypelist = this.helperService.enumSelector(RepresentiveTypeEnum);

		this.search();
		this.statusDDL = [
			{ label: "All", value: '' },
			{ label: "Active", value: true },
			{ label: "Inactive", value: false }
		];
		console.table("status", this.statusDDL);
	}

	toggleFilter() {
		this.searchCriteriaDTO = new RepresentiveSearchDTO();
		this.showFilterControls = !this.showFilterControls;
	}

	getAllCompanies() {
		this.representiveService.getAll(this.searchCriteriaDTO).subscribe((res: any) => {
			this.representiveList = res.list;
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
		this.representiveService.delete(id).subscribe((res: any) => {
			if (res) {
				this.toastrService.success(this.translate.instant("Message.DeletedSuccessfully"));
				this.getAllCompanies();
			}
		});
	}

	public openConfirmationDialog(item: RepresentiveDTO) {
		this.confirmationDialogService.confirm(this.translate.instant("ConfirmaionDialog.Title"), this.translate.instant("ConfirmaionDialog.Description"))
			.then((confirmed) => {
				if (confirmed) {
					this.delete(item.id);
				}
			})
			.catch(() => console.log('Representive dismissed the dialog (e.g., by using ESC, clicking the cross icon, or clicking outside the dialog)'));
	}

	onPageChange(event: any) {
		this.searchCriteriaDTO.page = event;
		this.getAllCompanies();
	}
}
