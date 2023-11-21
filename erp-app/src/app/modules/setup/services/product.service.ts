import { Injectable } from '@angular/core';
import { BaseEntityService } from '../../../shared/services/base-entity.service';
import { ProductTrackingSearchDTO } from '../models/product-tracking-search.dto';

@Injectable()
export class ProductService extends BaseEntityService {
  controllerName = 'Product';

  getAllLiteByCategoryId(categoryId: number): any {
    return this.httpHelperService.get(this.controllerName + '/GetAllLiteByCategoryId/' + categoryId);
  }

  getProductTrackingByProductId(productTrackingSearchDTO: ProductTrackingSearchDTO): any {
    return this.httpHelperService.post(this.controllerName + '/GetProductTrackingByProductId/', productTrackingSearchDTO);
  }

}