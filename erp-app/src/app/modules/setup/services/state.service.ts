import { Injectable } from '@angular/core';
import { BaseEntityService } from '../../../shared/services/base-entity.service';

@Injectable()
export class StateService extends BaseEntityService {
  controllerName='State'

  getAllLiteByCountryId(countryId: number): any {
    return this.httpHelperService.get(this.controllerName + '/' + "GetAllLiteByCountryId/" + countryId);
  }
  
}