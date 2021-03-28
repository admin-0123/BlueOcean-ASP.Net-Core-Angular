/* eslint-disable @typescript-eslint/no-empty-function */
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/_models/user';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-entry',
  templateUrl: './entry.component.html',
  styleUrls: ['./entry.component.scss']
})
export class EntryComponent implements OnInit {
  user: User = {} as User;
  newUser = false;

  constructor(
    private authService: AuthService,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
  }

  toggleForm(): void {
    this.user.password = '';
    this.newUser = !this.newUser;
  }

  login(): void {
    this.authService.login(this.user).subscribe();
  }

  register(): void {
    this.authService.register(this.user).subscribe();
  }
}
