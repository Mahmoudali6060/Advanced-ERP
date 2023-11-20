
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { RepresentiveDTO } from 'src/app/modules/setup/models/representive.dto';
import { RepresentiveService } from 'src/app/modules/setup/services/representive.service';
import { RepresentiveTypeEnum } from 'src/app/shared/enums/representive-type.enum';

@Component({
  selector: 'app-representive-form-popup',
  templateUrl: './representive-form-popup.component.html',
  styles: ['./representive-form-popup.component.']
})

export class RepresentiveFormPopupComponent {

  representiveDTO: RepresentiveDTO = new RepresentiveDTO();
  obj: RepresentiveTypeEnum;
  representiveTypeEnum = RepresentiveTypeEnum;
  constructor(private activeModal: NgbActiveModal,
    private representiveService: RepresentiveService
  ) { }

  ngOnInit() {
  }

  public save() {
    this.representiveDTO.representiveTypeId = this.obj;
    this.representiveService.add(this.representiveDTO).subscribe((res: any) => {
      if (res) {
        this.representiveDTO.id = parseInt(res);
        this.activeModal.close(this.representiveDTO);
      }
    });
  }

  public cancel() {
    this.activeModal.dismiss();
  }

}
