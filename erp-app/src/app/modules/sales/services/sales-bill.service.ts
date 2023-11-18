import { Injectable } from '@angular/core';
import { BaseEntityService } from '../../../shared/services/base-entity.service';
import { ClientVendorBalanceDTO } from '../../setup/models/client-vendor-balance.dto';

@Injectable()
export class SalesBillService extends BaseEntityService {
  controllerName = 'SalesBillHeader';


  getAllByClientId(clientId: number): any {
    return this.httpHelperService.get(this.controllerName + '/GetAllByClientId/' + clientId);
  }

}