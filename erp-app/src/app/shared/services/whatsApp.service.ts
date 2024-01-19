import { Injectable } from '@angular/core';
import { HttpHelperService } from './http-helper.service';

@Injectable()
export class WhatsAppService {

  whatsAppBaseUrl: string = 'https://api.whatsapp.com/send/';

  constructor(private httpHelper: HttpHelperService) {
  }

  sendMessage(phoneNo: string, body: string) {
    const sendUrl = `${this.whatsAppBaseUrl}?phone=${phoneNo}&text=${body}&app_absent=0`;
    window.open(sendUrl, '_blank');
  }

  sendFile(phoneNo: string) {
    debugger;
    let body: any = {
      "recipient_type": "individual",
      "to": "+201093162036",
      "type": "document",
      "document": {
        "link": "http://localhost:54095/wwwroot/Images/Companies/2024_01_19_10_17_07.jpg",
        "filename": "tt"
      }
    };
    const sendUrl = this.httpHelper.postWhatsUp(this.whatsAppBaseUrl + 'v1/messages', body).subscribe(res => {
      debugger;
    });
    // window.open(sendUrl, '_blank');
  }
}
