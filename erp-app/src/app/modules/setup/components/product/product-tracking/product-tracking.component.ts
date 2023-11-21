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
import { ProductTrackingDTO } from '../../../models/product-tracking.dto';
import { ProductTrackingSearchDTO } from '../../../models/product-tracking-search.dto';
import { LabelValuePair } from 'src/app/shared/enums/label-value-pair';
import { ProductProcessTypeEnum } from 'src/app/shared/enums/product-process-type.enum';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
	selector: 'app-product-tracking',
	templateUrl: './product-tracking.component.html',
	styleUrls: ['./product-tracking.component.css']


})
export class ProductTrackingComponent {
	@ViewChild(PaginationComponent) paginationComponent: PaginationComponent;
	dataSource: PagingDTO = new PagingDTO();
	productTrackingList: Array<ProductTrackingDTO>;
	showFilterControls: boolean = true;
	searchCriteriaDTO: ProductTrackingSearchDTO = new ProductTrackingSearchDTO()
	total: number;
	recordsPerPage: number = 5;
	productList: Array<ProductDTO> = new Array<ProductDTO>();
	productProcessTypelist: LabelValuePair[];
	productProcessTypeEnum = ProductProcessTypeEnum;
	productId: any;
	constructor(private productService: ProductService,
		private helperService: HelperService,
		private route: ActivatedRoute
	) {

	}

	ngOnInit() {
		this.productProcessTypelist = this.helperService.enumSelector(ProductProcessTypeEnum);
		this.productId = this.route.snapshot.paramMap.get('productId');
		this.getAllProducts();

	}

	toggleFilter() {
		this.searchCriteriaDTO = new ProductTrackingSearchDTO();
		this.showFilterControls = !this.showFilterControls;
	}


	getAllProducts() {
		this.productService.getAllLite().subscribe((res: any) => {
			this.productList = res.list;
			if (this.productId) {
				this.searchCriteriaDTO.productId = parseInt(this.productId);
				this.search();
			}
		})
	}
	getProductTrackingByProductId() {
		this.productService.getProductTrackingByProductId(this.searchCriteriaDTO).subscribe((res: any) => {
			this.productTrackingList = res.list;
			this.total = res.total;
			if (this.paginationComponent) {
				this.paginationComponent.totalRecordsCount = this.total;
				this.paginationComponent.setPagination(this.searchCriteriaDTO.page);
			}
		});
	}


	search() {
		this.getProductTrackingByProductId();
	}

	onPageChange(event: any) {
		this.searchCriteriaDTO.page = event;
		this.getProductTrackingByProductId();
	}
}
