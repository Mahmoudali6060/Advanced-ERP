import { Injectable } from '@angular/core';
import { DatePipe } from '@angular/common';
import { environment } from 'src/environments/environment';
import { AuthService } from '../../authentication/services/auth.service';
import { ConfigService } from 'src/app/shared/services/config.service';

@Injectable()

export class ReportService {
  baseUrl: string;
  serverUrl: string;

  constructor(
    private configService: ConfigService,
    private authService: AuthService) {
    this.serverUrl = this.configService.getServerUrl();

  }

  print(title: string, printedDiv: any) {

    let popupWin: any;
    popupWin = window.open('', '_blank', 'top=0,left=0,height=100%');
    popupWin.document.open();
    var html = `
		  <html dir="rtl">
			<head>
			  <title>${title}</title>
			  <meta charset="utf-8">
			  <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
			  <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0">
			  <link href="/assets/plugins/bootstrap/bootstrap.min.css" rel="stylesheet" media="all">
		      <link href="/assets/css/report.css" rel="stylesheet" media="all">
			    <body onload="window.print()">
			       <div class="wrapper" style="font-family: 'Helvetica Neue', lato, arial, sans-serif;font-size: 12px;">
				       <div >
                   <div class="invoice">
                     <div class="row">
                       <div class="col-lg-12 text-center">
                          <img width="100" src="${this.serverUrl}wwwroot/Images/Companies/${this.authService.loggedUserProfile.companyDTO.imageUrl}" class="logo">
                        </div>
                        <div class="col-lg-12">
                          <h1 class="display-4 text-center">${this.authService.loggedUserProfile?.companyDTO.name}</h1>
                          <p class="second-header text-center"><strong>${this.authService.loggedUserProfile?.companyDTO.addressDetails}</strong></p>
                          <p class="second-header text-center"><strong>Tel:${this.authService.loggedUserProfile?.companyDTO.contactTelephone}</strong></p>
                        </div>
                    </div>`;
    html += printedDiv?.innerHTML;
    html += ` 
           </div>
          </div>
			  </div>
			</body>
		  </html>`
    popupWin.document.write(html);
    popupWin.document.close();
  }
}