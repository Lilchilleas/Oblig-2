import {
    HTTP_INTERCEPTORS,
    HttpErrorResponse,
    HttpEvent,
    HttpHandler,
    HttpInterceptor,
    HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';


//The error handling logic is created based on the implemntation from:
//"Build an app with ASPNET Core and Angular from scratch" by Neil Cummings.
@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
        catchError(error=>{
            if(error.status === 401){
                return throwError(error.statusText);
            }
            if(error instanceof HttpErrorResponse){
                const applicationError = error.headers.get('Application-Error');
                if(applicationError){
                    return throwError(applicationError);
                }
                const serverError = error.error;
                let modelStateErrors = '';

                if(serverError.errors && typeof serverError.errors === 'object'){
                    for(const key in serverError.errors){
                        if(serverError.errors[key]){
                            modelStateErrors += serverError.errors[key] + '\n';
                        }
                    }
                }
                return throwError(modelStateErrors || serverError || 'Server Error');
            }
            return throwError('Unknown error');
        })
    )
  }
  
}

export const ErrorInterceptorProvider = {
    provide: HTTP_INTERCEPTORS,
    useClass: ErrorInterceptor,
    multi: true
}

// Source Reference:
// ------------------------------------------------------------------------
// - [Title: Build an app with ASPNET Core and Angular from scratch ]
// - Author: [Neil Cummings]
// - URL: [https://www.udemy.com/course/build-an-app-with-aspnet-core-and-angular-from-scratch/#instructor-1]