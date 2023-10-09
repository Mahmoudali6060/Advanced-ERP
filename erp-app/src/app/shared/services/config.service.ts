


import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Config } from '../models/config.model';
// import configData from '../../../assets/config/config.json';

@Injectable({ providedIn: 'root' })
export class ConfigService {
    config = 'assets/config/config.json';
    constructor(private _http: HttpClient) {
    }

    getServerUrl(): string {
        // now returns an Observable of Config
        //return this._http.get<Config>(this.config);
        return environment.serverUrl;
    }
}