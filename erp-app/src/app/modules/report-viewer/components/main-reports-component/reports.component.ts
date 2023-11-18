import { AfterViewInit, Component, Input, OnChanges, OnInit, SimpleChanges, ViewChild, ViewEncapsulation } from '@angular/core';
import { TelerikReportViewerComponent } from '@progress/telerik-angular-report-viewer';
import { ReportViewerService } from '../../services/report-viewer.service';
import { UserProfileService } from 'src/app/modules/user-management/services/user.service';
import { ConfigService } from 'src/app/shared/services/config.service';
import { AuthService } from 'src/app/modules/authentication/services/auth.service';
import { AuthGuardService } from 'src/app/shared/guards/auth-guard.service';

@Component({
    selector: 'app-reports',
    templateUrl: './reports.component.html',
    encapsulation: ViewEncapsulation.None,
})
export class ReportsComponent implements OnInit, AfterViewInit {

    constructor(private _userInfoService: UserProfileService,
        private reportService: ReportViewerService,
        private configService: ConfigService,
        private authGuardService: AuthGuardService,
        private authService: AuthService
    ) {
    }


    @ViewChild('viewer1') viewer: TelerikReportViewerComponent;
    @Input() reportName: string;
    @Input() parameters: any;
    @Input() baseURL: string;
    serviceUrl: string;
    authenticationToken: string;  // must e added in order to authenticate current user in Authorize attribute in ReportController in backend
    viewerContainerStyle: any;
    encapsulation: ViewEncapsulation.None;
    boundReportEvents: Function;

    ngOnInit(): void {
        this.serviceUrl = this.configService.getServerUrl() + 'api/reports';
        let token = this.authGuardService.getToken();
        if (token)
            this.authenticationToken = token;
        this.viewerContainerStyle = {
            position: 'relative',
            width: '90vw',
            height: '90vh',
            overflow: 'scroll',
            ['font-family']: 'ms sans serif'
        };
        this.boundReportEvents = this.reportMethodEventsFired.bind(this);
    }

    ngAfterViewInit(): void {
        if (this.viewer) {
            let token = this.authGuardService.getToken();
            if (token) {
                this.viewer.authenticationToken = token;
                this.viewer.setReportSource(
                    {
                        report: this.reportName,
                        parameters: this.parameters
                    }
                );
            }
        }
    }

    reportMethodEventsFired() {
        const isTokenStillValid = this.authService.isUserAuthenticated();
        if (!isTokenStillValid) {
            this.authService.logOut();
        }
    }

    reload(reportName: string, parameters: any) {
        // Overrite old token when reponse back to fronend ,It has new token
        let token = this.authGuardService.getToken();
        if (token)
            this.authenticationToken = token;
        if (this.authenticationToken) {
            this.viewer.authenticationToken = this.authenticationToken;
            this.isReportViewerAuthorized(reportName, parameters); // Check Report Viewer Authority
        }
    }

    private refreshReportData(reportName: string, parameters: any) {
        this.viewer.setPageMode(''); // To force report for reloading new data when you change langugue
        this.viewer.setReportSource(
            {
                report: reportName,
                parameters: parameters
            }
        );
        this.viewer.setPageMode('SINGLE_PAGE'); // To handle pagination in report viewer
        //
        setTimeout(() => {
            this.viewer.refreshReport();
        }, 500);
    }

    /*[Author]:[Mahmoud Ali Salman]
    [Date]:[18/3/2021]
    [Reason]:[This method is used to send a very simple request to back end
    After that check the resposnse code if (401),then user is UnAuthorized
    to handle Session timeout in report viewer .
    According to Bug:10431
    ]*/
    isReportViewerAuthorized(reportName: string, parameters: any) {
        this.reportService.isReportViewerAuthorized().subscribe(res => {
            if (res) {
                this.refreshReportData(reportName, parameters);
            }
        });
    }
}
