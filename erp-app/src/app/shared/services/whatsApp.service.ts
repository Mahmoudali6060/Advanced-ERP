import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class WhatsAppService {

  whatsAppBaseUrl: string = 'https://api.whatsapp.com/send/';

  constructor() {
  }

  sendMessage(phoneNo: string, body: string){
    const sendUrl = `${this.whatsAppBaseUrl}?phone=${phoneNo}&text=${body}&app_absent=0`;
    window.open(sendUrl, '_blank');
  }
}
