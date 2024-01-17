import { Injectable } from '@angular/core';
import { BaseEntityService } from '../../../shared/services/base-entity.service';

@Injectable()
export class AccountStatementService extends BaseEntityService {
  controllerName = 'AccountStatement';

  getAllForGrid(dataSourceModel: any): any {
    return this.httpHelperService.post(this.controllerName + '/GetAllForGrid', dataSourceModel);
  }

}