import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ReplaySubject } from 'rxjs';
import { of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Authorized, User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = environment.apiUrl + 'auth/';
  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(
    private http: HttpClient,
    private toastr: ToastrService
  ) { }

  login(user: User) {
    return this.http.post<Authorized>(this.baseUrl + 'login', user)
      .pipe(
        map(
          response => {
            this.toastr.success("Successful Authentication");
            console.log(response);
            localStorage.setItem('token', response.token)
          }
        ),
        catchError(error => {
          this.toastr.error(error.error);
          console.error(error);
          return of(null);
        })
      )
  }

  register(user: User) {
    return this.http.post<Authorized>(this.baseUrl + 'register', user)
      .pipe(
        map(
          response => {
            this.toastr.success("Successful Registration");
            console.log(response);
            localStorage.setItem('token', response['token'])
          }
        ),
        catchError(error => {
          this.toastr.error(error.error);
          console.error(error);
          return of(null);
        })
      )
  }

  isLoggedIn() : boolean {
    return true;
  }

  logOut() : void {
    localStorage.removeItem('token');
  }
}
