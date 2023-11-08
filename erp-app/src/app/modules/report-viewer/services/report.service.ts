import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpHelperService } from 'src/app/shared/services/http-helper.service';

@Injectable({
    providedIn: 'root'
})
export class ReportService {

    url: string = "ReportViewer";
    constructor(private httpService: HttpHelperService) { }

    isReportViewerAuthorized(): Observable<boolean> {
        return this.httpService.get(this.url + '/IsReportViewerAuthorized') as Observable<boolean>;
    }
}
