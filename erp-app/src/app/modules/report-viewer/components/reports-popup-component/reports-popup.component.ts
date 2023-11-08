import { AfterViewInit, ChangeDetectorRef, Component, EventEmitter, Input, OnInit, Output, SimpleChanges, ViewChild } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Subscription } from 'rxjs';
import { ReportsComponent } from '../main-reports-component/reports.component';
import { AlertService } from 'src/app/shared/services/alert.service';
import { UserProfileService } from 'src/app/modules/user-management/services/user.service';
import { HelperService } from 'src/app/shared/services/helper.service';

@Component({
    selector: 'reports-popup',
    templateUrl: './reports-popup.component.html',
})
export class ReportsPopupComponent implements OnInit, AfterViewInit {

    buttonMenuOpen: boolean;

    constructor(
        protected alertService: AlertService,
        translate: TranslateService,
        private userInfoService: UserProfileService,
        public sharedService: HelperService,
        private cdr: ChangeDetectorRef) {

    }



    @Input() reportPopupTitle: string;
    @Input() reportName: string;
    @Input() parameters: any;
    @Input() isOpened: boolean;
    @Input() baseURL: string;
    @Output() onClose: EventEmitter<boolean> = new EventEmitter<boolean>();
    @ViewChild(ReportsComponent) reportsComponent: ReportsComponent;

    _reportParameters: any;
    _reportName: string;

    propertySubscription: Subscription;

    ngOnInit(): void {

    }

    ngAfterViewInit(): void {
        this.setReportName();
    }

    setReportName() {
        if (this.reportName) {
            this._reportName = this.reportName.concat('.trdp');
        }
        this._reportParameters=this.parameters;
    }

    ngAfterViewChecked(){
        //your code to update the model
        this.cdr.detectChanges();
     }




    close() {
        this.onClose.emit(true);
        this.isOpened = false;
    }

    toggleButtonMenu() {
        this.buttonMenuOpen = !this.buttonMenuOpen;
    }
}
