import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmationDialogComponent } from '../components/confirmation-dialog/confirmation-dialog.component';


@Injectable()
export class DialogService {

    constructor(private modalService: NgbModal) { }

    public confirm(title: string, message: string, btnOkText: string = 'OK', btnCancelText: string = 'Cancel', dialogSize: 'sm' | 'lg' = 'sm', showCancel?: boolean, showOk?: boolean): Promise<boolean> {
        const modalRef = this.modalService.open(ConfirmationDialogComponent, { size: dialogSize });
        modalRef.componentInstance.title = title;
        modalRef.componentInstance.message = message;
        modalRef.componentInstance.btnOkText = btnOkText;
        modalRef.componentInstance.btnCancelText = btnCancelText;
        modalRef.componentInstance.showOk = showOk != null ? showOk : true;
        modalRef.componentInstance.showCancel = showCancel != null ? showCancel : true;


        return modalRef.result;
    }

    public show(dialogSize: 'sm' | 'lg' = 'sm', component: any, obj?: any): Promise<any> {
        const modalRef = this.modalService.open(component, { size: dialogSize });
        modalRef.componentInstance.obj = obj;
        return modalRef.result;
    }

}