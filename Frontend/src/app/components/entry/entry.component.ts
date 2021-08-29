/* eslint-disable @angular-eslint/no-empty-lifecycle-method */
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/_models/user';
import { AuthService } from 'src/app/_services/auth.service';
import { faUser } from '@fortawesome/free-solid-svg-icons';

@Component({
    selector: 'app-entry',
    templateUrl: './entry.component.html',
    styleUrls: ['./entry.component.scss']
})
export class EntryComponent implements OnInit {
    user: User = {} as User;
    newUser = false;
    faUser = faUser;
    isVisible = false;

    constructor(
        private authService: AuthService,
        private toastr: ToastrService,
        private dialog: MatDialog,
    ) { }

    ngOnInit(): void {
    }

    entryToggle(): void {
        this.isVisible = !this.isVisible;
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
