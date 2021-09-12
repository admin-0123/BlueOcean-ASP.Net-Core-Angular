import {
    Component,
    ViewEncapsulation
} from '@angular/core';
import {
    ActivationStart,
    NavigationEnd,
    NavigationError,
    NavigationStart,
    Router,
    RouterOutlet
} from '@angular/router';
import { Store } from '@ngrx/store';
import { slider } from './animations';
import { AppStore } from './store/app.store';
import { selectLocation } from './store/general/general.selectors';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss'],
    encapsulation: ViewEncapsulation.None,
    animations: [
        slider
    ]
})
export class AppComponent {
    title = 'Virta';
    location$ = this.store.select(selectLocation);
    location: object|null = null;


    constructor(
        private store: Store<AppStore>,
        private router: Router
    ){
        this.location$.subscribe(
            d => {
                const left = d.x + 200;
                const top = d.y;
                this.location = {
                    top: `${top}px`,
                    left: `${left}px`,
                    top2: `${top ? top - 30 : top}px`,
                    left2: `${left - 30}px`,
                    padding: `${top ? '30px' : '0'} 30px`
                };
            }
        );

        this.router.events.subscribe(
            event => {
                if (event instanceof ActivationStart) {
                    if (event.snapshot.data.animation === 'PDP') {
                        window.scrollTo({ top: 0, behavior: 'smooth' });
                    }
                }

                if (event instanceof NavigationStart) {
                    // Start
                }

                if (event instanceof NavigationEnd) {
                    // End
                }

                if (event instanceof NavigationError) {
                    // Error
                }
            }
        );
    }

    prepareRoute(outlet: RouterOutlet) {
        if (!this.location) return false;

        return {
            value: outlet && outlet.activatedRouteData && outlet.activatedRouteData.animation,
            params: this.location
        };
    }
}
