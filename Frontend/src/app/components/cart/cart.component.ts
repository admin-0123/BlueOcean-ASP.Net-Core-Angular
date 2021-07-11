/* eslint-disable @typescript-eslint/no-empty-function */
import { Component, OnInit } from '@angular/core';
import { faShoppingCart, faPlusCircle, faMinusCircle } from '@fortawesome/free-solid-svg-icons';
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
        private cartService: CartService
    ) { }

    ngOnInit(): void {
        this.cart = this.cartService.cart;

        this.cartService.watchStorage().subscribe(
            () => {
                this.cart = this.cartService.cart
            }
        )

        this.cartService.createHubConnection()
    }

    cartToggle(): void {
        this.isVisible = !this.isVisible;
        if (this.isVisible) {
            this.cartService.getRemoteCart().subscribe(
                data => {
                    this.cart = data
                }
            );
        } else {
            this.cartService.SaveCart().subscribe();
        }
    }

    decreaseQuality(item: ProductInCart): void {
        this.cartService.decreaseQuality(item)
    }

    increaseQuantity(item: ProductInCart): void {
        this.cartService.increaseQuantity(item)
    }
}
