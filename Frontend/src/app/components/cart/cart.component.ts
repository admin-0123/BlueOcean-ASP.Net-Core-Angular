import {
    Component,
    OnInit
} from '@angular/core';
import {
    faMinusCircle,
    faPlusCircle,
    faShoppingCart
} from '@fortawesome/free-solid-svg-icons';
import { Store } from '@ngrx/store';
import { AppStore } from 'src/app/store/app.store';
import {
    decrement, reset
} from 'src/app/store/general/general.actions';
import { selectNumber } from 'src/app/store/general/general.selectors';
import { ProductInCart } from 'src/app/_models/product';
import { CartService } from 'src/app/_services/cart.service';

@Component({
    selector: 'app-cart',
    templateUrl: './cart.component.html',
    styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {
    faShoppingCart = faShoppingCart;
    faPlusCircle = faPlusCircle;
    faMinusCircle = faMinusCircle;
    isVisible = false;
    cart: ProductInCart[] = [];
    count$ = this.store.select(selectNumber);

    constructor(
        private cartService: CartService,
        private store: Store<AppStore>
    ) {
    }

    ngOnInit(): void {
        this.cart = this.cartService.cart;

        this.cartService.watchStorage().subscribe(
            () => {
                this.cart = this.cartService.cart;
            }
        );

        this.cartService.createHubConnection();
    }

    cartToggle(): void {
        this.isVisible = !this.isVisible;
        if (this.isVisible) {
            this.cartService.getRemoteCart().subscribe(
                data => {
                    this.cart = data;
                }
            );
        } else {
            this.cartService.SaveCart().subscribe();
        }
    }

    decreaseQuality(item: ProductInCart): void {
        this.cartService.decreaseQuality(item);
    }

    increaseQuantity(item: ProductInCart): void {
        this.cartService.increaseQuantity(item);
    }

    // increment() {
    //     this.store.dispatch(increment());
    // }

    decrement() {
        this.store.dispatch(decrement());
    }

    reset() {
        this.store.dispatch(reset());
    }
}
