import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const authGuard: CanActivateFn = (route, state) => {

  const token = localStorage.getItem('token'); // Check if a token exists
  const router = inject(Router);
  if (token) {

    
  }

  if(token)
  {
    router.navigateByUrl('/profile');
    return true;
  }

  router.navigateByUrl('/login');
  return false;
};
