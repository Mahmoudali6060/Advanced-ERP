
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CategoryDTO } from 'src/app/modules/setup/models/category.dto';
import { CategoryService } from 'src/app/modules/setup/services/category.service';

@Component({
  selector: 'app-category-form-popup',
  templateUrl: './category-form-popup.component.html',
  styles: ['./category-form-popup.component.css']
})

export class CategoryFormPopupComponent {

  categoryDTO: CategoryDTO = new CategoryDTO();
  constructor(private activeModal: NgbActiveModal,
    private categoryService: CategoryService
  ) { }

  ngOnInit() {
  }

  public save() {
    this.categoryService.add(this.categoryDTO).subscribe((res: any) => {
      if (res) {
        this.categoryDTO.id = parseInt(res);
        this.activeModal.close(this.categoryDTO);
      }
    });
  }

  public cancel() {
    this.activeModal.dismiss();
  }


}
