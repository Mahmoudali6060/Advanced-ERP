import { Component, ViewChild, ViewEncapsulation } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { PagingDTO } from '../../../../../shared/models/paging-dto';
import { DialogService } from '../../../../../shared/services/confirmation-dialog.service';
import { ConfigService } from '../../../../../shared/services/config.service';
import { PaginationComponent } from 'src/app/shared/components/pagination/pagination.component';
import { LabelValuePair } from 'src/app/shared/enums/label-value-pair';
import { HelperService } from 'src/app/shared/services/helper.service';
import { UnitOfMeasurementDTO } from '../../../models/unit-of-measurement.dto';
import { UnitOfMeasurementSearchDTO } from '../../../models/unit-of-measurement-search.dto';
import { UnitOfMeasurementService } from '../../../services/unit-of-measurement.service';

@Component({
	selector: 'app-unit-of-measurement-list',
	templateUrl: './unit-of-measurement-list.component.html',
	styleUrls: ['./unit-of-measurement-list.component.css']
})

export class UnitOfMeasurementListComponent {
	@ViewChild(PaginationComponent) paginationComponent: PaginationComponent;
	dataSource: PagingDTO = new PagingDTO();
	unitOfMeasurementList: Array<UnitOfMeasurementDTO>;
	serverUrl: string;
	showFilterControls: boolean = false;
	searchCriteriaDTO: UnitOfMeasurementSearchDTO = new UnitOfMeasurementSearchDTO()
	total: number;
	statusDDL: any;

	constructor(private unitOfMeasurementService: UnitOfMeasurementService,
		private confirmationDialogService: DialogService,
		private toastrService: ToastrService,
		private translate: TranslateService,
		private _configService: ConfigService,
		public helperService: HelperService
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
		this.searchCriteriaDTO = new UnitOfMeasurementSearchDTO();
		this.showFilterControls = !this.showFilterControls;
	}

	getAllUnitOfMeasurement() {
		this.unitOfMeasurementService.getAll(this.searchCriteriaDTO).subscribe((res: any) => {
			this.unitOfMeasurementList = res.list;
			this.total = res.total;
			if (this.paginationComponent) {
				this.paginationComponent.totalRecordsCount = this.total;
				this.paginationComponent.setPagination(this.searchCriteriaDTO.page);
			}
			this.serverUrl = this._configService.getServerUrl();
		});
	}

	search() {
		this.getAllUnitOfMeasurement();
	}

	delete(id: any) {
		this.unitOfMeasurementService.delete(id).subscribe((res: any) => {
			if (res) {
				this.toastrService.success(this.translate.instant("Message.DeletedSuccessfully"));
				this.getAllUnitOfMeasurement();
			}
		});
	}

	public openConfirmationDialog(item: UnitOfMeasurementDTO) {
		this.confirmationDialogService.confirm(this.translate.instant("DeleteConfirmaionDialog.Title"), this.translate.instant("DeleteConfirmaionDialog.Description"))
			.then((confirmed) => {
				if (confirmed) {
					this.delete(item.id);
				}
			})
			.catch(() => console.log('UnitOfMeasurement dismissed the dialog (e.g., by using ESC, clicking the cross icon, or clicking outside the dialog)'));
	}

	onPageChange(event: any) {
		this.searchCriteriaDTO.page = event;
		this.getAllUnitOfMeasurement();
	}
}
