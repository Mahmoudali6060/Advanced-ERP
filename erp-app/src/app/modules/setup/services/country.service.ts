import { Injectable } from '@angular/core';
import { BaseEntityService } from '../../../shared/services/base-entity.service';

@Injectable()
export class CountryService extends BaseEntityService {
  controllerName = 'Country'
}