
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CategoryDTO } from 'src/app/modules/setup/models/category.dto';
import { ClientVendorDTO, ClientVendorTypeEnum } from 'src/app/modules/setup/models/client-vendor.dto';
import { CategoryService } from 'src/app/modules/setup/services/category.service';
import { ClientVendorService } from 'src/app/modules/setup/services/client-vendor.service';

@Component({
  selector: 'app-client-vendor-form-popup',
  templateUrl: './client-vendor-form-popup.component.html',
  styles: ['./client-vendor-form-popup.component.']
})

export class ClientVendorFormPopupComponent {

  clientVendorDTO: ClientVendorDTO = new ClientVendorDTO();
  obj: ClientVendorTypeEnum;
  clientVendorTypeEnum=ClientVendorTypeEnum;
  constructor(private activeModal: NgbActiveModal,
    private clientVendorService: ClientVendorService
  ) { }

  ngOnInit() {
  }

  public save() {
    this.clientVendorDTO.typeId = this.obj;
    this.clientVendorService.add(this.clientVendorDTO).subscribe((res: any) => {
      if (res) {
        this.clientVendorDTO.id = parseInt(res);
        this.activeModal.close(this.clientVendorDTO);
      }
    });
  }

  public cancel() {
    this.activeModal.dismiss();
  }


}
