import { Injectable } from '@angular/core';
import { BaseEntityService } from 'src/app/shared/services/base-entity.service';

@Injectable({
  providedIn: 'root'
})
export class AdvertismentService  extends BaseEntityService {
  controllerName = 'Advertisments';
  urlAddRang = "Add";
  
  addRang(entity: any) {
    return this.httpHelperService.post(`${this.controllerName}/AddRange/`, entity);
}
}
