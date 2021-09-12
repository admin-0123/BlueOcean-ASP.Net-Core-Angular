/* eslint-disable @angular-eslint/no-empty-lifecycle-method */
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

    constructor(
        private store: Store<AppStore>,
        private elementRef: ElementRef
    ) { }

    ngOnInit(): void {
    }

    onClick(event: any): void {
        console.log(event.target);
        console.log(this.getPosition(this.elementRef));
        this.store.dispatch(setProductCardLocation({ location:
            {
                // x: this.elementRef.nativeElement.offsetLeft,
                // y: this.elementRef.nativeElement.offsetTop
                x: this.getPosition(this.elementRef).offsetLeft,
                y: this.getPosition(this.elementRef).offsetTop
            }
        }));
    }

    getPosition(event: any){
        let offsetLeft = 0;
        let offsetTop = 0;

        let el = event.nativeElement;

        while (el){
            offsetLeft += el.offsetLeft;
            offsetTop += el.offsetTop;
            el = el.parentElement;
        }
        return { offsetTop:offsetTop, offsetLeft:offsetLeft };
    }
}
