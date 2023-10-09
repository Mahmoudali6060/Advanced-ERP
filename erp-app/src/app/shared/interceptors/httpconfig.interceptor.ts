import { Injectable } from '@angular/core';
// import { ErrorDialogService } from '../error-dialog/errordialog.service';
import { HttpInterceptor, HttpRequest, HttpResponse, HttpHandler, HttpEvent, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { NgxSpinnerService } from 'ngx-spinner';
import { AlertService } from '../services/alert.service';
import { LocalStorageService } from '../services/local-storage.service';
import { TranslateService } from '@ngx-translate/core';

@Injectable()
export class HttpConfigInterceptor implements HttpInterceptor {
    count = 0;
    constructor(private spinner: NgxSpinnerService,
        private alertService: AlertService,
        private localStorageService: LocalStorageService,
        private translate: TranslateService) {
    }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        let handled: boolean = false;
        let token = this.localStorageService.getToken();
        const clonedReq = request.clone({
            headers: request.headers.set('Authorization', 'Bearer ' + token)
        });

        return next.handle(clonedReq)
            .pipe(
                catchError((returnedError: any) => {
                    let errorMessage;
                    if (returnedError.error instanceof ErrorEvent) {
                        errorMessage = `Error: ${returnedError.error.message}`;
                    } else if (returnedError instanceof HttpErrorResponse) {
                        errorMessage = `Error Status ${returnedError.status}: ${returnedError.error.error} - ${returnedError.error.message}`;
                        handled = this.handleServerSideError(returnedError);
                    }
                    if (!handled) {
                        if (errorMessage) {
                            return throwError(errorMessage);
                        } else {
                            return throwError("Unexpected problem occurred");
                        }
                    } else {
                        return of(returnedError);
                    }
                })
            )
    }

    private handleServerSideError(error: HttpErrorResponse): boolean {
        if (error.error?.errors) {
            let errorList: any = Object.entries(error.error?.errors)
            for (let error of errorList) {
                let transaltedErrorMessage = this.translate.instant(error[1][0]);
                this.alertService.showError(transaltedErrorMessage, this.translate.instant("General.Error"));
            }
        }
        else if (error.error?.Message) {
            let transaltedErrorMessage = this.translate.instant(error.error.Message);
            this.alertService.showError(transaltedErrorMessage, this.translate.instant("General.Error"));
        }

        return true;
        // let handled: boolean = false;
        // switch (error.status) {
        //     case 401:
        //         this.alertService.showError(transaltedErrorMessage, this.translate.instant("General.Error"));
        //         handled = true;
        //         break;
        //     case 403:
        //         this.alertService.showError(transaltedErrorMessage, this.translate.instant("General.Error"));
        //         handled = true;
        //         break;

        //     case 500:
        //         this.alertService.showError(transaltedErrorMessage, this.translate.instant("General.Error"));
        //         handled = true;
        //         break;
        // }
        // return handled;
    }

}