import {
    Component,
    ViewEncapsulation
} from '@angular/core';
import { RouterOutlet } from '@angular/router';
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
    location: object|null = { top: '0px', left: '200px', top2: '-30px', left2: '170px', padding: '30px 30px' };


    constructor(
        private store: Store<AppStore>
    ){
        this.location$.subscribe(
            d => {
                if (d.y) {
                    const top = d.y - 199;
                    this.location = {
                        top: `${top}px`,
                        left: `${d.x - 232}px`,
                        top2: top ? `${d.y - 229}px` : `${top}px`,
                        left2: `${d.x - 262}px`,
                        padding: `${ top ? '30px' : '0'} 30px`
                    };
                } else {
                    this.location = null;
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
