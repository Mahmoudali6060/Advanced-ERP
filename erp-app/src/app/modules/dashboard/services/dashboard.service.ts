import { Injectable } from '@angular/core';
import { BaseEntityService } from 'src/app/shared/services/base-entity.service';



@Injectable()
export class DashboardService extends BaseEntityService {
  controllerName = 'Dashboard';

  getDashboard(dataSourceModel: any): any {
    return this.httpHelperService.post(this.controllerName + '/GetDashboard', dataSourceModel);
}

  
}