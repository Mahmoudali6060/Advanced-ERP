import { Injectable } from '@angular/core';
import { BaseEntityService } from '../../../shared/services/base-entity.service';

@Injectable()
export class PurchasesBillService extends BaseEntityService {
  controllerName = 'PurchasesBillHeader';

  GetAllByVendorId(vendorId: number): any {
    return this.httpHelperService.get(this.controllerName + '/GetAllByVendorId/' + vendorId);
  }

}