import { NgModule } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { ReportService } from 'src/app/modules/report/services/report.service';
import { CommonModule } from '@angular/common';
import { DatabaseService } from 'src/app/modules/database/services/database.service';
import { DatabaseBackupComponent } from 'src/app/modules/database/components/database-backup/database-backup.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
  ],
  declarations: [
    DatabaseBackupComponent
  ],
  providers: [
    DatabaseService
  ],
  entryComponents: [
    DatabaseBackupComponent
  ]
})

export class DatabaseModule {
}
