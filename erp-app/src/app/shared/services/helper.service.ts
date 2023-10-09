import { Injectable, Inject } from '@angular/core';
import { DatePipe } from '@angular/common';
import { TranslateService } from '@ngx-translate/core';
import { Enum } from '../enums/enum';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { UserProfileDTO } from 'src/app/modules/user/models/user-profile.dto';
import { LocalStorage } from '@ngx-pwa/local-storage';
import { LocalStorageService } from './local-storage.service';
import { LocalStorageItems } from '../constants/local-storage-items';


@Injectable({ providedIn: 'root' })

export class HelperService {

    constructor(private _datePipe: DatePipe, public translate: TranslateService,private localStorageService:LocalStorageService) {

    }
    
    //public subject = new Subject<any>();
    // getMessage(): Observable<any> {
    //     return this.subject.asObservable();
    // }
    // postUserProfile(message: string) {
    //     this.subject.next({ text: message });
    // }
    transformDate(date: any) {
        return this._datePipe.transform(date, "yyyy-MM-dd"); //whatever format you need. 
    }


   


    useLanguage(language: string): void {
        const htmlTag = document.getElementsByTagName("html")[0] as HTMLHtmlElement;
        htmlTag.dir = language === "ar" ? "rtl" : "ltr";
        htmlTag.lang = language;
        this.translate.use(language);

        if (language === "ar") {
            this.RemoveCssToHTMlPage("assets/css/styles-ltr.css");
            this.loadCssToHTMlPage("assets/css/styles-rtl.css");
        }
        else {
            this.RemoveCssToHTMlPage("assets/css/styles-rtl.css");
            this.loadCssToHTMlPage("assets/css/styles-ltr.css");
        }

    }


    loadCssToHTMlPage(url: string): void {
        // Create link
        let link = document.createElement('link');
        link.href = url;
        link.rel = 'stylesheet';
        link.type = 'text/css';

        let head = document.getElementsByTagName('head')[0];
        let links = head.getElementsByTagName('link');
        let style = head.getElementsByTagName('style')[0];

        // Check if the same style sheet has been loaded already.
        let isLoaded = false;
        for (var i = 0; i < links.length; i++) {
            var node = links[i];
            if (node.href.indexOf(link.href) > -1) {
                isLoaded = true;
            }
        }
        if (isLoaded) return;
        head.insertBefore(link, style);
    }

    RemoveCssToHTMlPage(url: string): void {
        // Create link
        let head = document.getElementsByTagName('head')[0];
        let links = head.getElementsByTagName('link');
        let style = head.getElementsByTagName('style')[0];
        // Check if the same style sheet has been loaded already.
        let isLoaded = false;
        for (var i = 0; i < links.length; i++) {
            var node = links[i];
            if (node.href.indexOf(url) > -1) {
                head.removeChild(node);
            }
        }
    }

    //// function for convert enum to object
    enumSelector(definition: any) {
        return Object.keys(definition)
            .filter((v) => isNaN(Number(v)))
            .map((label) => {
                return {
                    value: definition[label as keyof typeof definition],
                    label,
                };
            });
    }
    getRole() {
        return localStorage.getItem('role');
    }
}
