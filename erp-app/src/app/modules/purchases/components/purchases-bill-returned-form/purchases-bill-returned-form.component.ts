import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
	selector: 'app-purchases-bill-returned-form',
	templateUrl: './purchases-bill-returned-form.component.html',
	styleUrls: ['./purchases-bill-returned-form.component.css']
})
export class PurchasesBillReturnedFormComponent {
	purchasesHeaderId: number;
	constructor(
		private route: ActivatedRoute,
	) {
	}

	ngOnInit() {
		let purchasesHeaderId = this.route.snapshot.paramMap.get('id');
		if (purchasesHeaderId) {
			this.purchasesHeaderId = parseInt(purchasesHeaderId);
		}
	}

}
