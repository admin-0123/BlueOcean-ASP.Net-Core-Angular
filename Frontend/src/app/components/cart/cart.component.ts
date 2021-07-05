/* eslint-disable @typescript-eslint/no-empty-function */
import { Component, OnInit } from '@angular/core';
import { faShoppingCart, faPlusCircle, faMinusCircle } from '@fortawesome/free-solid-svg-icons';
import { Product, ProductInCart } from 'src/app/_models/product';
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
    cart: Array<ProductInCart> = [];

    constructor(
        private cartService: CartService
    ) {
        this.cartService.watchStorage().subscribe(
            () => {
                this.cart = this.cartService.getCart();
            }
        )
    }

    ngOnInit(): void {
        this.cart = this.cartService.getCart();
    }

    cartToggle(): void {
        this.isVisible = !this.isVisible;
    }

    decreaseQuality(item: ProductInCart): void {
        this.cartService.decreaseQuality(item)
    }

    increaseQuantity(item: ProductInCart): void {
        this.cartService.increaseQuantity(item)
    }
}
