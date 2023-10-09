import { Injectable } from '@angular/core';
import { BaseEntityService } from 'src/app/shared/services/base-entity.service';

@Injectable({
  providedIn: 'root'
})
export class AboutUsService extends BaseEntityService {
  controllerName = 'AboutUs';
}
