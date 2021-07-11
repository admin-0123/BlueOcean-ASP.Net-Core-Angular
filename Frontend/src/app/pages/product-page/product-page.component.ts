import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Product, ProductInCart } from 'src/app/_models/product';
import { CartService } from 'src/app/_services/cart.service';

@Component({
    selector: 'app-product-page',
    templateUrl: './product-page.component.html',
    styleUrls: ['./product-page.component.scss']
})
export class ProductPageComponent implements OnInit {
    product!: Product;
    isInCart = false;
    quantity = 1;
    columnsToDisplay = ['name', 'value'];

    constructor(
        private route: ActivatedRoute,
        private cart: CartService
    ) { }

    ngOnInit(): void {
        this.route.data.subscribe(
            data => {
                this.product = data.product;
            }
        );

        this.cart.watchStorage().subscribe(
            () => {
                this.isInCart = this.cart.isItemInCart(this.product.id);
            }
        );

        this.isInCart = this.cart.isItemInCart(this.product.id);
    }

    CartAction(): void {
        if(this.isInCart) {
            this.cart.removeItem(this.product.id);
            return;
        }

        this.cart.addItem({
            id: this.product.id,
            title: this.product.title,
            price: this.product.price,
            images: this.product.images.map(i => i.url),
            quantity: this.quantity
        } as ProductInCart);
    }
}
