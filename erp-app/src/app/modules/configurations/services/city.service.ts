import { Injectable } from '@angular/core';
import { BaseEntityService } from '../../../shared/services/base-entity.service';

@Injectable()
export class CityService extends BaseEntityService {
  controllerName = 'City'

  getAllLiteByStateId(stateId: number): any {
    return this.httpHelperService.get(this.controllerName + '/' + "GetAllLiteByStateId/" + stateId);
  }
  
}