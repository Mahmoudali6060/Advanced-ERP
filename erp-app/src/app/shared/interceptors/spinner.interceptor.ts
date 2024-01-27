import { Injectable } from '@angular/core';
// import { ErrorDialogService } from '../error-dialog/errordialog.service';
import { HttpInterceptor, HttpRequest, HttpResponse, HttpHandler, HttpEvent, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { NgxSpinnerService } from 'ngx-spinner';
import { tap } from 'rxjs/internal/operators/tap';
import { finalize } from 'rxjs/internal/operators/finalize';

@Injectable()
export class SpinnerInterceptor implements HttpInterceptor {
    count = 0;

    constructor(private spinner: NgxSpinnerService) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> { 
        this.count++;
        this.spinner.show();
        return next.handle(req)
            .pipe(tap(
                event => {
                    this.spinner?.hide();
                  //  console.log(event)
                },
               // error => console.log(error)

            ), finalize(() => {
                this.count--;
                if (this.count == 0) this.spinner?.hide()
            })
            );
    }
}