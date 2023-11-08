import { NgModule, CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA } from "@angular/core";
import { ButtonsModule } from "@progress/kendo-angular-buttons";
import { InputsModule } from "@progress/kendo-angular-inputs";
import { GridModule } from "@progress/kendo-angular-grid";
import { DateInputsModule } from "@progress/kendo-angular-dateinputs";
import { DropDownsModule } from "@progress/kendo-angular-dropdowns";
import { NotificationModule } from "@progress/kendo-angular-notification";
import { TooltipModule } from "@progress/kendo-angular-tooltip";
import { LayoutModule } from "@progress/kendo-angular-layout";
import { PopupModule } from "@progress/kendo-angular-popup";
import { MenuModule } from "@progress/kendo-angular-menu";
import { ScrollViewModule } from "@progress/kendo-angular-scrollview";

@NgModule({
  declarations: [],
  imports: [
    ButtonsModule,
    InputsModule,
    GridModule,
    DateInputsModule,
    DropDownsModule,
    NotificationModule,
    TooltipModule,
    LayoutModule,
    PopupModule,
    MenuModule,
    ScrollViewModule
  ],
  providers: [],
  exports: [
    ButtonsModule,
    InputsModule,
    GridModule,
    DateInputsModule,
    DropDownsModule,
    NotificationModule,
    TooltipModule,
    LayoutModule,
    PopupModule,
    MenuModule,
    ScrollViewModule
  ], 
  schemas: [CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA]
})
export class KendoUIControlsModule {}
