import { NgModule } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { ReportService } from 'src/app/modules/report/services/report.service';
import { CommonModule } from '@angular/common';

@NgModule({
  imports: [
    CommonModule,
    SharedModule
  ],
  declarations: [
  ],
  providers: [
    ReportService
  ]
})

export class ReportModule {
}
