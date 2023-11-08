
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CategoryDTO } from 'src/app/modules/setup/models/category.dto';
import { ProductDTO } from 'src/app/modules/setup/models/product.dto';
import { CategoryService } from 'src/app/modules/setup/services/category.service';
import { ProductService } from 'src/app/modules/setup/services/product.service';

@Component({
  selector: 'app-product-form-popup',
  templateUrl: './product-form-popup.component.html',
  styles:['./product-form-popup.component.css']
})

export class ProductFormPopupComponent {

  productDTO: ProductDTO = new ProductDTO();
  categoryList: Array<CategoryDTO> = new Array<CategoryDTO>();
  constructor(private activeModal: NgbActiveModal,
    private productService: ProductService, private categoryService: CategoryService
  ) { }

  ngOnInit() {
    this.getAllCategories();
  }

  getAllCategories() {
    this.categoryService.getAllLite().subscribe((res: any) => {
      this.categoryList = res.list;
    })
  }

  public save() {
    this.productService.add(this.productDTO).subscribe((res: any) => {
      if (res) {
        this.productDTO.id = parseInt(res);
        this.activeModal.close(this.productDTO);
      }
    });
  }

  public cancel() {
    this.activeModal.dismiss();
  }


}
