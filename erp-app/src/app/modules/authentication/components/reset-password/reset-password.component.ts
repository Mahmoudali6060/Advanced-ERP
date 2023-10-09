import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { LocalStorageItems } from 'src/app/shared/constants/local-storage-items';
import { LocalStorageService } from 'src/app/shared/services/local-storage.service';
import { isNullOrUndefined } from 'util';
import { ResetPasswordDTO } from '../../models/reset-password-dto';
import { AuthService } from '../../services/auth.service';
import { PasswordConfirmationValidatorService } from '../Customvalidators/password-confirmation-validator.service';


@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent implements OnInit {
  resetPasswordForm: FormGroup;
  showSuccess: boolean;
  showError: boolean;
  errorMessage: string;
  token: string;
  email: any;
  oldfieldTextType: boolean;
  fieldTextType: boolean;
  repeatFieldTextType: boolean;
  @Input() isProfileComponent: boolean;
  @Output() closeChangePasswordCard: EventEmitter<boolean> = new EventEmitter()
  constructor(fb: FormBuilder, private _authService: AuthService, private _passConfValidator: PasswordConfirmationValidatorService,
    private toasterService: ToastrService, private localStorageService: LocalStorageService,private router:Router,
    private _route: ActivatedRoute,) { }

  ngOnInit(): void {
    this.resetPasswordForm = new FormGroup({
      //oldPassword: new FormControl('', [Validators.required]),
      password: new FormControl('', [Validators.required]),
      confirm: new FormControl('', [Validators.required])
    });

    // this.resetPasswordForm.get('confirm')?.setValidators([Validators.required,
    //   this._passConfValidator.validateConfirmPassword(this.resetPasswordForm.value.get('password'))]);
    if (this.isProfileComponent) {
      this.token = this.localStorageService.getItem(LocalStorageItems.token)
      this.email = this.localStorageService.getItem(LocalStorageItems.email)
    } else {
      this.token = this._route.snapshot.queryParams['token'];
      this.email = this._route.snapshot.paramMap.get('email');
      if(!this.email || isNullOrUndefined(this.email)){
      this.email = this._route.snapshot.queryParams['email'];
      }
    }

  }
  toggleOldFieldTextType() {
    this.oldfieldTextType = !this.oldfieldTextType;
  }
  toggleFieldTextType() {
    this.fieldTextType = !this.fieldTextType;
  }
  toggleRepeatFieldTextType() {
    this.repeatFieldTextType = !this.repeatFieldTextType;
  }
  CloseChangePassword() {
    this.closeChangePasswordCard.emit(true)
  }
  cancel(){
    this.router.navigate(['/home']);
  }
  public validateControl = (controlName: string) => {
    return this.resetPasswordForm.controls[controlName].invalid && this.resetPasswordForm.controls[controlName].touched
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.resetPasswordForm.controls[controlName].hasError(errorName)
  }

  public resetPassword = (resetPasswordFormValue: any) => {
    this.showError = this.showSuccess = false;

    const resetPass = { ...resetPasswordFormValue };
    const resetPassDto: ResetPasswordDTO = {
      // oldPassword:resetPass.oldPassword,
      password: resetPass.password,
      confirmPassword: resetPass.confirm,
      token: this.token,
      email: this.email
    }
    this._authService.resetPassword('Account/ResetPassword', resetPassDto)
      .subscribe(_ => {
        this.showSuccess = true;
        this.toasterService.success("success");
        this.closeChangePasswordCard.emit(true)
        this.router.navigate(["login"]);
      },
        error => {
          this.showError = true;
          this.errorMessage = error;
        })
  }

}