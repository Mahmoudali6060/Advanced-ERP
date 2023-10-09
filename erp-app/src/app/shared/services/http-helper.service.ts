import { CookieService } from 'ngx-cookie-service';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ConfigService } from './config.service';

@Injectable({ providedIn: 'root' })
export class HttpHelperService {

    baseUrl: string;
    constructor(
        private _http: HttpClient,
        private _configService: ConfigService
    ) {
        
        var config = _configService.getServerUrl();
        this.baseUrl = config + "api/";
    }

    post(url: any, body: any) {
        return this._http.post(this.baseUrl + url, body, {
            headers: new HttpHeaders({
                "Content-Type": "application/json"
            })
        });
    }

    get(url: string) {
        return this._http.get(this.baseUrl + url, {
            headers: new HttpHeaders({
                "Content-Type": "application/json"
            })
        });
    }

    delete(url: string) {
        return this._http.delete(this.baseUrl + url, {
            headers: new HttpHeaders({
                "Content-Type": "application/json"
            })
        });
    }
}