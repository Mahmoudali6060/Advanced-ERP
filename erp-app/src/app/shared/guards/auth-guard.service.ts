import { Injectable } from '@angular/core';
import { CanActivate } from '@angular/router';
import { Router } from '@angular/router';
import { JwtHelperService } from "@auth0/angular-jwt";
import { LocalStorageItems } from '../constants/local-storage-items';
import { LocalStorageService } from '../services/local-storage.service';
import { UserProfileDTO } from 'src/app/modules/user-management/models/user-profile.dto';
import { AuthService } from 'src/app/modules/authentication/services/auth.service';

@Injectable({ providedIn: 'root' })
export class AuthGuardService implements CanActivate {
    constructor(private jwtHelper: JwtHelperService,
        private router: Router,
        private localStorageService: LocalStorageService,
        private authService: AuthService) {

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



    isAuthorize(privilegeId: number): boolean {

        if (this.authService.loggedUserProfile) {
            if (this.authService.loggedUserProfile?.roleGroupDTO.rolePrivileges && this.authService.loggedUserProfile?.roleGroupDTO.rolePrivileges.length > 0) {
                let privilege = this.authService.loggedUserProfile?.roleGroupDTO.rolePrivileges?.find(x => x.privilegeId == privilegeId);
                if (privilege) {
                    return true;
                }
            }
        }
        return false;
    }

    getToken() {
        return this.localStorageService.getToken();
    }
}