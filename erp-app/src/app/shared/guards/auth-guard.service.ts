import { Injectable } from '@angular/core';
import { CanActivate } from '@angular/router';
import { Router } from '@angular/router';
import { JwtHelperService } from "@auth0/angular-jwt";
import { LocalStorageItems } from '../constants/local-storage-items';
import { LocalStorageService } from '../services/local-storage.service';

@Injectable({ providedIn: 'root' })
export class AuthGuardService implements CanActivate {
    constructor(private jwtHelper: JwtHelperService,
        private router: Router,
        private localStorageService: LocalStorageService) {
    }
    canActivate() {
        //To-Do
        return true;

        // var token = this.getToken();
        // if (token && !this.jwtHelper.isTokenExpired(token)) {
        //     return true;
        // }
        // // window.alert('Access Denied, Login is Required to Access This Page!')
        // this.router.navigate(["/"]);
        // return false;
    }
    getToken() {
        return this.localStorageService.getToken();
    }
}