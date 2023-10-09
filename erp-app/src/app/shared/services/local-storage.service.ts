


import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { LocalStorageItems } from '../constants/local-storage-items';

@Injectable({ providedIn: 'root' })
export class LocalStorageService {
    constructor(private _http: HttpClient) {
    }

    getItem(key: string) {
        let item = localStorage.getItem(key)
        if (item != null) {
            return JSON.parse(item);
        }
        return null;
    }

    getToken() {
        return localStorage.getItem(LocalStorageItems.token);
    }


    setItem(key: string, value: string) {
        localStorage.setItem(key, JSON.stringify(value));
    }

    updateItem(key: string, newValue: any) {
        this.removeItem(key);
        this.setItem(key, newValue);
    }


    removeItem(key: string) {
        localStorage.removeItem(key);
    }

    clear() {
        localStorage.clear();
    }
}