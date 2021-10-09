import { Injectable } from '@angular/core';
import {
    CanActivate,
    Router
} from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../_services/auth.service';

@Injectable({
    providedIn: 'root'
})
export class AuthGuard implements CanActivate {
    constructor(
        private authService: AuthService,
        private toastr: ToastrService,
        private router: Router
    ) { }

    canActivate(): boolean {
        if (this.authService.isLoggedInSub.value) {
            return true;
        }

        this.toastr.error('Please Login!');
        this.router.navigate(['/home']);
        return false;
    }
}
