"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
exports.__esModule = true;
exports.ReportsComponent = void 0;
var core_1 = require("@angular/core");
var config_service_1 = require("../../../../shared/services/config.service");
var ReportsComponent = /** @class */ (function () {
    function ReportsComponent(_userInfoService) {
        this._userInfoService = _userInfoService;
    }
    ReportsComponent.prototype.ngOnInit = function () {
        var _a;
        this.serviceUrl = config_service_1.ConfigService.settings.apiServer.apiURL + 'api/reports';
        this.authenticationToken = this._userInfoService.getToken();
        this.viewerContainerStyle = (_a = {
                position: 'relative',
                width: '90vw',
                height: '90vh',
                overflow: 'scroll'
            },
            _a['font-family'] = 'ms sans serif',
            _a);
    };
    ReportsComponent.prototype.ngAfterViewInit = function () {
        if (this.viewer) {
            this.viewer.setReportSource({
                report: this.reportName,
                parameters: this.parameters
            });
        }
    };
    ReportsComponent.prototype.reload = function (reportName, parameters) {
        parameters["date"] = new Date().toString();
        this.viewer.setPageMode(''); //To force report for reloading new data when you change langugue
        this.viewer.setReportSource({
            report: reportName,
            parameters: parameters
        });
        this.viewer.setPageMode('SINGLE_PAGE'); //To handle pagination in report viewer 
    };
    __decorate([
        core_1.ViewChild('viewer1')
    ], ReportsComponent.prototype, "viewer");
    __decorate([
        core_1.Input()
    ], ReportsComponent.prototype, "reportName");
    __decorate([
        core_1.Input()
    ], ReportsComponent.prototype, "parameters");
    ReportsComponent = __decorate([
        core_1.Component({
            selector: 'app-reports',
            templateUrl: './reports.component.html',
            encapsulation: core_1.ViewEncapsulation.None
        })
    ], ReportsComponent);
    return ReportsComponent;
}());
exports.ReportsComponent = ReportsComponent;
