/* eslint-disable @angular-eslint/no-empty-lifecycle-method */
import {
    Component,
    Input,
    OnInit
} from '@angular/core';
import { Store } from '@ngrx/store';
import { AppStore } from 'src/app/store/app.store';
import { increment } from 'src/app/store/general/general.actions';
import { selectLocation } from 'src/app/store/general/general.selectors';
import { Product } from 'src/app/_models/product';

@Component({
    selector: 'app-product-card',
    templateUrl: './product-card.component.html',
    styleUrls: ['./product-card.component.scss']
})
export class ProductCardComponent implements OnInit {
    @Input() product!: Product;
    location$ = this.store.select(selectLocation);


    constructor(
        private store: Store<AppStore>
    ) { }

    ngOnInit(): void {
    }

    onClick(event: any): void {
        // console.log(event.target.x, event.target.y);
        this.store.dispatch(increment({ location: { x: event.target.x, y: event.target.y } }));
    }
}
