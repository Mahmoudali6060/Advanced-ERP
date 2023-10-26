import { Injectable } from '@angular/core';
import { BaseEntityService } from '../../../shared/services/base-entity.service';
import { AvailabilitySearchCriteriaDTO } from '../models/availability-search-criteria-dto';

@Injectable()
export class RoleService extends BaseEntityService {
  controllerName = 'Role';


}