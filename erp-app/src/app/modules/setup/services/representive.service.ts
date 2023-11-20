import { Injectable } from '@angular/core';
import { BaseEntityService } from '../../../shared/services/base-entity.service';

@Injectable({
  providedIn: "root"
})
export class RepresentiveService extends BaseEntityService {
  controllerName = 'Representive';

}