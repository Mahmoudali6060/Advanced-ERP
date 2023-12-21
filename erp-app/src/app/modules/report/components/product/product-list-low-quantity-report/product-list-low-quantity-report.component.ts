import { Component, Input, ViewChild, ViewEncapsulation } from '@angular/core';
import { ProductSearchCriteriaDTO } from 'src/app/modules/setup/models/product-search-criteria.dto';
import { ProductListReportComponent } from '../product-list-report/product-list-report.component';

@Component({
	selector: 'app-product-list-low-quantity-report',
	templateUrl: './product-list-low-quantity-report.component.html',
	styleUrls: ['./product-list-low-quantity-report.component.css']


})
export class ProductListLowQuantityReportComponent {
	@ViewChild(ProductListReportComponent) productListReportComponent: ProductListReportComponent;
	searchCriteriaDTO: ProductSearchCriteriaDTO = new ProductSearchCriteriaDTO()

	constructor() {

	}

	ngOnInit() {
		this.searchCriteriaDTO.isLowQuantity = true;
	}

	print() {
		this.productListReportComponent.print();
	}

}
