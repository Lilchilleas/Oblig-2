import { inject } from '@angular/core';
import {  CanActivateFn, Router } from '@angular/router';
import { AuthService } from './auth.service';
 

 
 

export const AuthGuardService: CanActivateFn = (route,state) => {
    const authService = inject(AuthService);
    const router = inject(Router);
    console.log('Is logged in:', authService.loggedIn());
    if (!authService.loggedIn()) {
        router.navigate(['/login']);
        
        return false;
    }
    return true;
}



 