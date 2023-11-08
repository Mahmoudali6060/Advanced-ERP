"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
exports.__esModule = true;
exports.ReportsPopupComponent = void 0;
var core_1 = require("@angular/core");
var util_1 = require("../../../../shared/classes/util");
var base_component_1 = require("../../../../shared/components/base-component/base.component");
var language_enum_1 = require("../../../../shared/enums/language.enum");
var printing_paper_type_enum_1 = require("../../../../shared/enums/printing-paper-type.enum");
var reports_component_1 = require("../main-reports-component/reports.component");
var ReportsPopupComponent = /** @class */ (function (_super) {
    __extends(ReportsPopupComponent, _super);
    function ReportsPopupComponent(alert, translate, userInfoService, sharedService, vouchersNumberingService) {
        var _this = _super.call(this, sharedService, translate, alert) || this;
        _this.alert = alert;
        _this.userInfoService = userInfoService;
        _this.sharedService = sharedService;
        _this.vouchersNumberingService = vouchersNumberingService;
        _this.onClose = new core_1.EventEmitter();
        return _this;
    }
    ReportsPopupComponent.prototype.ngOnInit = function () {
        this.languageEnum = language_enum_1.LanguageEnum;
        this.printLanguage = language_enum_1.LanguageEnum.en;
    };
    ReportsPopupComponent.prototype.ngAfterViewInit = function () {
        var _this = this;
        this.propertySubscription = this.sharedService.propertyObservable.subscribe(function (prop) {
            _this.getPrintingPaperType(prop.id);
        });
    };
    ReportsPopupComponent.prototype.setReportName = function () {
        if (this.reportName) {
            this._reportName = this.reportName;
            this.setReportArabicEnglishBasedOnLanguage();
        }
    };
    ReportsPopupComponent.prototype.close = function () {
        this.onClose.emit(true);
        this.isOpened = false;
    };
    ReportsPopupComponent.prototype.onPaperTypesChange = function (dataItem) {
        // get parameters, convert them to json object, get property named 'ShowHeaderFooter' and set it to false  
        this.setPrintingPaperType();
        this.reportsComponent.reload(this._reportName, this._reportParameters);
    };
    ReportsPopupComponent.prototype.setPrintingPaperType = function () {
        this._reportParameters = this.parameters;
        if (this.printPaperType == printing_paper_type_enum_1.PrintingPaperTypeEnum.LetterHeadPaper) {
            this._reportParameters["ShowHeaderFooter"] = true;
        }
        else { //White Paper
            this._reportParameters["ShowHeaderFooter"] = false;
        }
    };
    ReportsPopupComponent.prototype.onPrintingLanguageChange = function () {
        this.setReportArabicEnglishBasedOnLanguage();
        this.reportsComponent.reload(this._reportName, this._reportParameters);
    };
    ReportsPopupComponent.prototype.setReportArabicEnglishBasedOnLanguage = function () {
        if (this.printLanguage) {
            switch (this.printLanguage) {
                case language_enum_1.LanguageEnum.ar:
                    this._reportName = this.reportName.concat('_Ar.trdp');
                    break;
                default:
                    this._reportName = this.reportName.concat('_En.trdp');
                    break;
            }
        }
        else {
            this._reportName = this.reportName.concat('_En.trdp');
        }
        this._reportParameters = this.parameters;
    };
    ReportsPopupComponent.prototype.getPrintingPaperType = function (propertyId) {
        var _this = this;
        this.vouchersNumberingService.getAll(propertyId).subscribe(function (res) {
            _this.printPaperType = res.printingPaperTypeId;
            if (util_1.isNullOrUndefined(res.printingPaperTypeId) || res.printingPaperTypeId == 0) {
                _this.printPaperType = printing_paper_type_enum_1.PrintingPaperTypeEnum.LetterHeadPaper;
            }
            _this.setPrintingPaperType();
            _this.setReportName();
        });
    };
    ReportsPopupComponent.prototype.toggleButtonMenu = function () {
        this.buttonMenuOpen = !this.buttonMenuOpen;
    };
    __decorate([
        core_1.Input()
    ], ReportsPopupComponent.prototype, "reportPopupTitle");
    __decorate([
        core_1.Input()
    ], ReportsPopupComponent.prototype, "reportName");
    __decorate([
        core_1.Input()
    ], ReportsPopupComponent.prototype, "parameters");
    __decorate([
        core_1.Input()
    ], ReportsPopupComponent.prototype, "isOpened");
    __decorate([
        core_1.Output()
    ], ReportsPopupComponent.prototype, "onClose");
    __decorate([
        core_1.ViewChild(reports_component_1.ReportsComponent)
    ], ReportsPopupComponent.prototype, "reportsComponent");
    ReportsPopupComponent = __decorate([
        core_1.Component({
            selector: 'reports-popup',
            templateUrl: './reports-popup.component.html'
        })
    ], ReportsPopupComponent);
    return ReportsPopupComponent;
}(base_component_1.BaseComponent));
exports.ReportsPopupComponent = ReportsPopupComponent;
