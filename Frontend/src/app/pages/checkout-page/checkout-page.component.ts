import { Component, OnInit } from '@angular/core';
import { faMinusCircle, faPlusCircle } from '@fortawesome/free-solid-svg-icons';
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
        private orderService: OrderService
    ) {
        this.cartService.watchStorage().subscribe(
            () => {
                this.cart = this.cartService.cart;
                this.totalPrice = this.cart.reduce(
                    (total, product) => total + product.price, 0
                );
            }
        );
    }


    ngOnInit(): void {
        this.cart = this.cartService.cart;

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
