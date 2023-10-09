import { Component } from '@angular/core';
declare var $: any;
import { TranslateService } from '@ngx-translate/core';
import { AuthService } from 'src/app/modules/authentication/services/auth.service';
import { Router } from '@angular/router';
import { DatabaseBackupComponent } from 'src/app/modules/database/components/database-backup/database-backup.component';
@Component({
  selector: 'app-header-mobile',
  templateUrl: './header-mobile.component.html'
})
export class HeaderMobileComponent {

  constructor(
    private router: Router,
   ) {
  }

  public switchLanguage(language: string) {

  }

  public logOut() {
    this.router.navigate(["/home"]);
  }
}
