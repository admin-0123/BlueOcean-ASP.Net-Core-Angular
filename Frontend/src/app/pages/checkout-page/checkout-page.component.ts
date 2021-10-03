import {
    Component,
    OnInit
} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import {
    faMinusCircle,
    faPlusCircle
} from '@fortawesome/free-solid-svg-icons';
import { Store } from '@ngrx/store';
import { AppStore } from 'src/app/store/app.store';
import { setLoadingScreen } from 'src/app/store/general/general.actions';
import { ProductInCart } from 'src/app/_models/product';
import { CartService } from 'src/app/_services/cart.service';
import { OrderService } from 'src/app/_services/order.service';

@Component({
    selector: 'app-checkout-page',
    templateUrl: './checkout-page.component.html',
    styleUrls: ['./checkout-page.component.scss']
})
export class CheckoutPageComponent implements OnInit {
    faPlusCircle = faPlusCircle;
    faMinusCircle = faMinusCircle;
    totalPrice = 0;
    cart: ProductInCart[] = [];

    constructor(
        private cartService: CartService,
        private orderService: OrderService,
        private route: ActivatedRoute,
        private store: Store<AppStore>
    ) { }

    ngOnInit(): void {
        this.route.data.subscribe(
            data => {
                this.store.dispatch(setLoadingScreen({ loadingScreen: false }));
            }
        );

        this.cartService.cartSub.subscribe(
            (cart) => {
                this.cart = cart;
                this.totalPrice = this.cart.reduce(
                    (total, product) => total + product.price, 0
                );
            }
        );

        this.totalPrice = this.cart?.reduce(
            (total, product) => total + product.price, 0
        );
    }

    decreaseQuality(item: ProductInCart): void {
        this.cartService.decreaseQuality(item);
    }

    increaseQuantity(item: ProductInCart): void {
        this.cartService.increaseQuantity(item);
    }

    submit() {
        this.orderService.placeOrder(this.cart, this.totalPrice).subscribe();
    }
}
