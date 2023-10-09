import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';
import { ForgotPassword } from '../../models/forgot-password';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {
  public forgotPasswordForm: FormGroup
  public successMessage: string;
  public errorMessage: string;
  public showSuccess: boolean;
  public showError: boolean;
  public _token: string;
  public _email: string;
  constructor(private _authService: AuthService,private _route: Router,private toasterService:ToastrService,private translate: TranslateService,) { }  ngOnInit(): void {
    this.forgotPasswordForm = new FormGroup({
      email: new FormControl("", [Validators.required])
    })
  }
  public validateControl = (controlName: string) => {
    return this.forgotPasswordForm.controls[controlName].invalid && this.forgotPasswordForm.controls[controlName].touched
  }
  public hasError = (controlName: string, errorName: string) => {
    return this.forgotPasswordForm.controls[controlName].hasError(errorName)
  }
  public forgotPassword = (forgotPasswordFormValue: any) => {
    this.showError = this.showSuccess = false;
    const forgotPass = { ...forgotPasswordFormValue };
    var strUrl = `${environment.urlAddress4200}` + "resetpassword?"
    const forgotPassDto: ForgotPassword = {
      email: forgotPass.email,
     // clientURI: 'http://localhost:4200/#/Resetpassword'
      clientURI:strUrl
      
    }
    this._authService.forgotPassword('Account/ForgotPassword', forgotPassDto)
    .subscribe(_ => {
      this.showSuccess = true;
			this.toasterService.error(this.translate.instant("Authentication.forgetPasswordMessage"));
      // this.successMessage = 'The link has been sent, please check your email to reset your password.'
    },
    err => {
      this.showError = true;
      this.errorMessage = err;
    })
  }
  back(){
    this._route.navigate(['/home']);
  }
}