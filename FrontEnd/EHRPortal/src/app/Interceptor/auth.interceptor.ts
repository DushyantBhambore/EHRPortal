import { HttpInterceptorFn } from '@angular/common/http';
import { LoaderService } from '../Service/loader.service';
import { inject } from '@angular/core';
import { finalize } from 'rxjs';

export const authInterceptor: HttpInterceptorFn = (req, next) => {


  const loaderService = inject(LoaderService);
  loaderService.show();

  const token = localStorage.getItem('token');
  const authReq = token ? req.clone({
    setHeaders: {
      Authorization: `Bearer ${token}`
    }
  }) : req;

  return next(authReq).pipe(
    finalize(() => loaderService.hide())
  );
}
