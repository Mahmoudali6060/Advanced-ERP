import { CanDeactivate } from '@angular/router';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DialogService } from '../services/confirmation-dialog.service';
import { TranslateService } from '@ngx-translate/core';

export interface ComponentCanDeactivate {
    canDeactivate: () => boolean | Observable<boolean>;
}

@Injectable()
export class PendingChangesGuard implements CanDeactivate<ComponentCanDeactivate> {
    confirmed: boolean | null = null;

    constructor(private dialogService: DialogService,
        private translate: TranslateService
    ) {
        this.confirmed = null
    };
    canDeactivate(component: ComponentCanDeactivate): any {
        if (component.canDeactivate()) {
            // this.dialogService.confirm(this.translate.instant("ConfirmaionDialog.Title"), this.translate.instant("ConfirmaionDialog.Description"))
            //     .then((confirmed: boolean) => {
            //         return confirmed;
            //         //this.canDeactivate(component);
            //     })

            if (confirm("انتبه!\nهل تريد فعلا المغادرة؟")) {
                return true;
            } else {
                return false;
            }

            // // // if there are no pending changes, just allow deactivation; else confirm first
            // if (component.canDeactivate()) {
            //     //     true :
            //     // NOTE: this warning message will only be shown when navigating elsewhere within your angular app;
            //     // when navigating away from your angular app, the browser will show a generic warning message
            //     // see http://stackoverflow.com/a/42207299/7307355


            //     if (this.confirmed == null) {
            //         this.openConfirmDialog(component);
            //     }
            //     else if (this.confirmed == true) {
            //         this.confirmed = null;
            //         return true;
            //     }
            //     else if (this.confirmed == false) {
            //         this.confirmed = null;
            //         return false;
            //     }
            // }
            // else{
            //     return true;
            // }

        }
        return true;
    }
    private openConfirmDialog(component: ComponentCanDeactivate): any {
        this.dialogService.confirm(this.translate.instant("ConfirmaionDialog.Title"), this.translate.instant("ConfirmaionDialog.Description"))
            .then((confirmed: boolean) => {
                this.confirmed = confirmed;
                this.canDeactivate(component);
            })
    }
}