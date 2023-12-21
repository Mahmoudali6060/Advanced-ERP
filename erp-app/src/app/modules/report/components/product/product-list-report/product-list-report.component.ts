import { Component, Input, ViewChild, ViewEncapsulation } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { PaginationComponent } from 'src/app/shared/components/pagination/pagination.component';
import { ConfigService } from 'src/app/shared/services/config.service';
import { PagingDTO } from '../../../../../shared/models/paging-dto';
import { DialogService } from '../../../../../shared/services/confirmation-dialog.service';
import { CountryModel } from 'src/app/modules/configurations/models/country.model';
import { StateModel } from 'src/app/modules/configurations/models/state.model';
import { CityModel } from 'src/app/modules/configurations/models/city.model';
import { AlertService } from 'src/app/shared/services/alert.service';
import { ProductDTO } from 'src/app/modules/setup/models/product.dto';
import { ProductSearchCriteriaDTO } from 'src/app/modules/setup/models/product-search-criteria.dto';
import { CategoryDTO } from 'src/app/modules/setup/models/category.dto';
import { ProductService } from 'src/app/modules/setup/services/product.service';
import { CategoryService } from 'src/app/modules/setup/services/category.service';
import { ReportService } from '../../../services/report.service';

@Component({
	selector: 'app-product-list-report',
	templateUrl: './product-list-report.component.html',
	styleUrls: ['./product-list-report.component.css']


})
export class ProductListReportComponent {
	dataSource: PagingDTO = new PagingDTO();
	productList: Array<ProductDTO>;
	serverUrl: string;
	@Input() searchCriteriaDTO: ProductSearchCriteriaDTO = new ProductSearchCriteriaDTO()
	total: number;
	recordsPerPage: number = 5;

	constructor(private productService: ProductService,
		private _configService: ConfigService,
		private translate: TranslateService,
		private reportService: ReportService) {
	}

	ngOnInit() {
		this.searchCriteriaDTO.pageSize = 1000000;
		this.search();
	}

	getAllProducts() {
		this.productService.getAll(this.searchCriteriaDTO).subscribe((res: any) => {
			this.productList = res.list;
			this.total = res.total;
			
			this.serverUrl = this._configService.getServerUrl();
		});
	}

	search() {
		this.getAllProducts();
	}

	

	print() {
		let div: any = document.getElementById('product-list');
		this.reportService.print(this.translate.instant("Reports.MinusProducts"), div);
	}
}
