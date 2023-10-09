import { Injectable } from '@angular/core';
import { HttpHelperService } from '../../../shared/services/http-helper.service';

@Injectable()
export class DatabaseService extends HttpHelperService {


  backupdDatabase(model: any) {
    return this.post(`Database/BackupDatabase`, model);
  }
}