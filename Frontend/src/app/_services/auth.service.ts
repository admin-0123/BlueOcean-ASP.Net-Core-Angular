import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { of } from 'rxjs';
import { Observable } from 'rxjs/internal/Observable';
import { catchError, map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { AuthToken, User } from '../_models/user';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    baseUrl = environment.apiUrl + 'auth/';
    jwtHelper = new JwtHelperService();
    decodedToken: any;

    constructor(
        private http: HttpClient,
        private toastr: ToastrService
    ) { }

    login(user: User): Observable<void | null> {
        return this.http.post<AuthToken>(this.baseUrl + 'login', user)
            .pipe(
                map(
                    response => {
                        this.toastr.success('Successful Authentication');
                        console.log(response);
                        localStorage.setItem('token', response.token);
                        this.decodedToken = this.jwtHelper.decodeToken(response.token);
                    }
                ),
                catchError(error => {
                    this.toastr.error(error.error);
                    console.error(error);
                    return of(null);
                })
            );
    }

    register(user: User): Observable<void | null> {
        return this.http.post<AuthToken>(this.baseUrl + 'register', user)
            .pipe(
                map(
                    response => {
                        this.toastr.success('Successful Registration');
                        console.log(response);
                        localStorage.setItem('token', response.token);
                        this.decodedToken = this.jwtHelper.decodeToken(response.token);
                    }
                ),
                catchError(error => {
                    this.toastr.error(error.error);
                    console.error(error);
                    return of(null);
                })
            );
    }

    isLoggedIn(): boolean {
        const token = localStorage.getItem('token') || '';
        return !this.jwtHelper.isTokenExpired(token);
    }

    logOut(): void {
        localStorage.removeItem('token');
    }

    getDecodedToken() {
        const token = localStorage.getItem('token') || '';
        return JSON.parse(atob(token.split('.')[1]));
    }
}
