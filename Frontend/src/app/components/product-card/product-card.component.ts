import {
    Component,
    ElementRef,
    Input,
    OnInit
} from '@angular/core';
import { Store } from '@ngrx/store';
import { AppStore } from 'src/app/store/app.store';
import { setProductCardLocation } from 'src/app/store/general/general.actions';
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
    text: any = [];

    constructor(
        private store: Store<AppStore>,
        private elementRef: ElementRef
    ) { }

    ngOnInit(): void {
    }

    onClick(): void {
        const rect = this.elementRef.nativeElement.getBoundingClientRect();
        this.store.dispatch(setProductCardLocation({
            location:
                {
                    offsetLeft: rect.left + window.pageXOffset,
                    offsetTop: rect.top + window.pageYOffset
                }
        }));
    }
}

