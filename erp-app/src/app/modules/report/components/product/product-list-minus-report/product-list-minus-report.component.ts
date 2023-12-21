import { Component, Input, ViewChild, ViewEncapsulation } from '@angular/core';
import { ProductSearchCriteriaDTO } from 'src/app/modules/setup/models/product-search-criteria.dto';
import { ProductListReportComponent } from '../product-list-report/product-list-report.component';

@Component({
	selector: 'app-product-list-minus-report',
	templateUrl: './product-list-minus-report.component.html',
	styleUrls: ['./product-list-minus-report.component.css']


})
export class ProductListMinusReportComponent {
	@ViewChild(ProductListReportComponent) productListReportComponent: ProductListReportComponent;
	searchCriteriaDTO: ProductSearchCriteriaDTO = new ProductSearchCriteriaDTO()

	constructor() {

	}

	ngOnInit() {
		this.searchCriteriaDTO.isMinusQuantity = true;
	}

	print() {
		this.productListReportComponent.print();
	}

}
