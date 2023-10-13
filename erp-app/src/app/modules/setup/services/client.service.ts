import { Injectable } from '@angular/core';
import { BaseEntityService } from '../../../shared/services/base-entity.service';

@Injectable()
export class ClientService extends BaseEntityService {
  controllerName = 'Client';
}