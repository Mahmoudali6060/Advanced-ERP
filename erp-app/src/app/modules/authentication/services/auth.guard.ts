import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthGuardService } from 'src/app/shared/guards/auth-guard.service';
import { LocalStorageItems } from '../../../shared/constants/local-storage-items';
import { LocalStorageService } from '../../../shared/services/local-storage.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {


  constructor(private router: Router,
    private localStorageService: LocalStorageService, private authService: AuthGuardService) {
  }
  canActivate(): boolean {
    if (this.authService.canActivate()) {
      return true;
    }
    else {
      return false;
    }
  }
  // canActivate(
  //   next: ActivatedRouteSnapshot,
  //   state: RouterStateSnapshot): boolean {
  //   if (this.localStorageService.getToken() != null) {
  //     let roles = next.data['permittedRoles'] as Array<string>;
  //     if (roles) {
  //       if (this.roleMatch(roles)) return true;
  //       else {
  //         window.alert('Access Denied, Login is Required to Access This Page!')
  //         this.router.navigate(['login']);
  //          return false;
  //       }
  //     }
  //     return true;
  //   }
  //   else {
  //     this.router.navigate(['/user/login']);
  //     return false;
  //   }

  // }

  roleMatch(allowedRoles: any): boolean {
    var isMatch = false;
    // var payLoad = JSON.parse(window.atob(localStorage.getItem('token').split('.')[1]));
    // var userRole = payLoad.role;
    // allowedRoles.forEach(element => {
    //   if (userRole == element) {
    //     isMatch = true;
    //     return false;
    //   }
    // });
    return isMatch;
  }
}
