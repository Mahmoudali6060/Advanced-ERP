"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
exports.__esModule = true;
exports.ReportViewerModule = void 0;
var core_1 = require("@angular/core");
var common_1 = require("@angular/common");
var reports_component_1 = require("./components/main-reports-component/reports.component");
var telerik_angular_report_viewer_1 = require("@progress/telerik-angular-report-viewer");
var shared_module_1 = require("../../shared/modules/shared.module");
var reports_popup_component_1 = require("./components/reports-popup-component/reports-popup.component");
var report_viewer_routing_module_1 = require("./report-viewer-routing.module");
var ReportViewerModule = /** @class */ (function () {
    function ReportViewerModule() {
    }
    ReportViewerModule = __decorate([
        core_1.NgModule({
            imports: [
                common_1.CommonModule,
                shared_module_1.SharedModule,
                telerik_angular_report_viewer_1.TelerikReportingModule,
                report_viewer_routing_module_1.ReportViewerRoutingModule
            ],
            declarations: [
                reports_component_1.ReportsComponent,
                reports_popup_component_1.ReportsPopupComponent
            ],
            exports: [
                reports_component_1.ReportsComponent,
                reports_popup_component_1.ReportsPopupComponent
            ], providers: [],
            schemas: [
                core_1.CUSTOM_ELEMENTS_SCHEMA
            ]
        })
    ], ReportViewerModule);
    return ReportViewerModule;
}());
exports.ReportViewerModule = ReportViewerModule;
