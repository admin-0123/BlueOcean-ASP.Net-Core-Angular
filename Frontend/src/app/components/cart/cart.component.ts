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

    constructor(
        private cartService: CartService,
        private store: Store<AppStore>
    ) {
    }

    ngOnInit(): void {
        // this.cartService.cartSub.subscribe(
        //     (cart) => this.cart = cart
        // );
        this.cart = this.cartService.cartSub.getValue();

        this.cartService.watchStorage().subscribe(
            () => {
                this.cart = this.cartService.cartSub.getValue();
            }
        );

        // this.cartService.createHubConnection();
    }

    cartToggle(): void {
        this.isVisible = !this.isVisible;
    }

    decreaseQuality(item: ProductInCart): void {
        this.cartService.decreaseQuality(item);
    }

    increaseQuantity(item: ProductInCart): void {
        this.cartService.increaseQuantity(item);
    }
}
