import { Injectable } from '@angular/core';
import { BaseEntityService } from '../../../shared/services/base-entity.service';
import { ClientVendorTypeEnum } from '../models/client-vendor.dto';

@Injectable()
export class ClientVendorService extends BaseEntityService {
  controllerName = 'ClientVendor';

  getAllLiteByTypeId(typeId: ClientVendorTypeEnum): any {
    return this.httpHelperService.get(this.controllerName + '/GetAllLiteByTypeId/' + typeId);
  }

}