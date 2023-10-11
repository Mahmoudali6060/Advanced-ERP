import { Injectable } from '@angular/core';
import { BaseEntityService } from '../../../shared/services/base-entity.service';

@Injectable()
export class ProductService extends BaseEntityService {
  controllerName = 'Product';

  getAllLiteByCategoryId(categoryId: number): any {
    return this.httpHelperService.get(this.controllerName + '/GetAllLiteByCategoryId/' + categoryId);
  }

}