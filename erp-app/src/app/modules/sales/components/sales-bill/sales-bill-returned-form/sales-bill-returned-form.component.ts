import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
	selector: 'app-sales-bill-returned-form',
	templateUrl: './sales-bill-returned-form.component.html',
	styleUrls: ['./sales-bill-returned-form.component.css']
})
export class SalesBillReturnedFormComponent {
	salesHeaderId: number;
	constructor(
		private route: ActivatedRoute,
	) {
	}

	ngOnInit() {
		let salesHeaderId = this.route.snapshot.paramMap.get('id');
		if (salesHeaderId) {
			this.salesHeaderId = parseInt(salesHeaderId);
		}
	}

}
