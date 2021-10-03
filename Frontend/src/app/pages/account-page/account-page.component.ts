import {
    Component,
    OnInit
} from '@angular/core';
import {
    ActivatedRoute,
    Router
} from '@angular/router';
import { Store } from '@ngrx/store';
import { AppStore } from 'src/app/store/app.store';
import { setLoadingScreen } from 'src/app/store/general/general.actions';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
    selector: 'app-account-page',
    templateUrl: './account-page.component.html',
    styleUrls: ['./account-page.component.scss']
})
export class AccountPageComponent implements OnInit {

    constructor(
        private route: ActivatedRoute,
        private store: Store<AppStore>,
        private authService: AuthService,
        private router: Router
    ) { }

    ngOnInit(): void {
        this.route.data.subscribe(
            data => {
                this.store.dispatch(setLoadingScreen({ loadingScreen: false }));
            }
        );
    }

    logOut(): void {
        this.authService.logOut();
        this.router.navigate(['/']);
    }
}
