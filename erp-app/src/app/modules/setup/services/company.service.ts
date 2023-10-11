import { Injectable } from '@angular/core';
import { BaseEntityService } from '../../../shared/services/base-entity.service';

@Injectable({
  providedIn: "root"
})
export class CompanyService extends BaseEntityService {
  controllerName = 'ShippingLine';
  GetAllCompanyTotalDetails(): any{
    return this.httpHelperService.get(this.controllerName + '/GetAllCompanyTotalDetails');
  }
}