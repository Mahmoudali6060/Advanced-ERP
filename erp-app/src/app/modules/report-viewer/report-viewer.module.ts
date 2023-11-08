import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { ReportsComponent } from "./components/main-reports-component/reports.component";
import { TelerikReportingModule } from '@progress/telerik-angular-report-viewer';
import { ReportsPopupComponent } from "./components/reports-popup-component/reports-popup.component";
import { ReportViewerRoutingModule } from "./report-viewer-routing.module";
import { SharedModule } from "src/app/shared/shared.module";

@NgModule({
    imports: [
        CommonModule,
        SharedModule,
        TelerikReportingModule,
        ReportViewerRoutingModule
    ],
    declarations: [
        ReportsComponent,
        ReportsPopupComponent
    ],
    exports: [
        ReportsComponent,
        ReportsPopupComponent
    ], providers: [

    ],
    schemas: [
        CUSTOM_ELEMENTS_SCHEMA
    ]
})
export class ReportViewerModule {

}