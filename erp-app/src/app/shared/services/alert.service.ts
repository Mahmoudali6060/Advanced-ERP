import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { HttpHelperService } from './http-helper.service';

@Injectable({
    providedIn: 'root'
})
export class AlertService {

    constructor(private toastr: ToastrService,public httpHelperService: HttpHelperService) { }

    showSuccess(message: any, title: any) {
        this.toastr.success(message, title)
    }

    showError(message: any, title: any) {
        this.toastr.error(message, title)
    }

    showInfo(message: any, title: any) {
        this.toastr.info(message, title)
    }

    showWarning(message: any, title: any) {
        this.toastr.warning(message, title)
    }

}