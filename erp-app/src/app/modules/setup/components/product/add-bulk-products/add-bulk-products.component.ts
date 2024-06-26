import { Component, Input, ViewChild, ViewEncapsulation } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { PaginationComponent } from 'src/app/shared/components/pagination/pagination.component';
import { ConfigService } from 'src/app/shared/services/config.service';
import { HelperService } from 'src/app/shared/services/helper.service';
import { PagingDTO } from '../../../../../shared/models/paging-dto';
import { DialogService } from '../../../../../shared/services/confirmation-dialog.service';
import { ProductService } from '../../../services/product.service';
import { CountryModel } from 'src/app/modules/configurations/models/country.model';
import { StateModel } from 'src/app/modules/configurations/models/state.model';
import { CityModel } from 'src/app/modules/configurations/models/city.model';
import { ProductDTO } from '../../../models/product.dto';
import { ProductSearchCriteriaDTO } from '../../../models/product-search-criteria.dto';
import { CategoryDTO } from '../../../models/category.dto';
import { CategoryService } from '../../../services/category.service';
import { AlertService } from 'src/app/shared/services/alert.service';

@Component({
	selector: 'app-add-bulk-products',
	templateUrl: './add-bulk-products.component.html',
	styleUrls: ['./add-bulk-products.component.css']
})

export class AddBulkProductsComponent {
	@ViewChild(PaginationComponent) paginationComponent: PaginationComponent;
	dataSource: PagingDTO = new PagingDTO();
	productList: Array<ProductDTO>;
	serverUrl: string;
	showFilterControls: boolean = false;
	searchCriteriaDTO: ProductSearchCriteriaDTO = new ProductSearchCriteriaDTO()
	total: number;
	recordsPerPage: number = 5;
	productTypeEnum: any;
	countryList: Array<CountryModel> = new Array<CountryModel>();
	stateList: Array<StateModel> = new Array<StateModel>();
	cityList: Array<CityModel> = new Array<CityModel>();
	statusList: Array<string> | Boolean
	statusDDL: any;
	categoryList: Array<CategoryDTO> = new Array<CategoryDTO>();
	@Input() viewMode: boolean = false;
	@Input() changePrice: boolean = false;
	@Input() changeQuantity: boolean = false;
	changeListPrice: boolean = false;
	constructor(private productService: ProductService,
		private confirmationDialogService: DialogService,
		private toastrService: ToastrService,
		private translate: TranslateService,
		private _configService: ConfigService,
		private categoryService: CategoryService,
		public helperService: HelperService,
		private alertService: AlertService) {

	}

	ngOnInit() {
		this.search();
		this.getAllCategories();
		this.statusDDL = [
			{ label: "All", value: '' },
			{ label: "Active", value: true },
			{ label: "Inactive", value: false }
		];
		console.table("status", this.statusDDL);
	}


	toggleFilter() {
		this.searchCriteriaDTO = new ProductSearchCriteriaDTO();
		this.showFilterControls = !this.showFilterControls;
	}

	getAllCategories() {
		this.categoryService.getAllLite().subscribe((res: any) => {
			this.categoryList = res.list;
		})
	}


	getAllProducts() {
		this.productService.getAll(this.searchCriteriaDTO).subscribe((res: any) => {
			this.productList = res.list;
			this.total = res.total;
			if (this.paginationComponent) {
				this.paginationComponent.totalRecordsCount = this.total;
				this.paginationComponent.setPagination(this.searchCriteriaDTO.page);
			}
			this.serverUrl = this._configService.getServerUrl();
		});
	}

	saveAll() {
		let changedProducts = this.productList.filter(x => x.isChanged == true);
		this.setDefaultPrices(changedProducts);
		this.productService.updateAll(changedProducts).subscribe(res => {
			if (res)
				this.alertService.showSuccess("Success", "Success");
		});
	}
	setDefaultPrices(changedProducts: ProductDTO[]) {
		if (this.changeListPrice) {
			for (let item of changedProducts) {
				item.sellingPrice = item.purchasingPrice;
			}
		}
	}
	search() {
		this.getAllProducts();
	}

	delete(id: any) {
		this.productService.delete(id).subscribe((res: any) => {
			if (res) {
				this.toastrService.success(this.translate.instant("Message.DeletedSuccessfully"));
				this.getAllProducts();
			}
		});
	}

	public openConfirmationDialog(item: ProductDTO) {
		this.confirmationDialogService.confirm(this.translate.instant("DeleteConfirmaionDialog.Title"), this.translate.instant("DeleteConfirmaionDialog.Description"))
			.then((confirmed) => {
				if (confirmed) {
					this.delete(item.id);
				}
			})
			.catch(() => console.log('Product dismissed the dialog (e.g., by using ESC, clicking the cross icon, or clicking outside the dialog)'));
	}

	onPageChange(event: any) {
		this.searchCriteriaDTO.page = event;
		this.getAllProducts();
	}

	reset() {
		this.search();
	}
}
