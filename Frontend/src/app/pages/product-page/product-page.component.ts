import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Product } from 'src/app/_models/product';
import { CartService } from 'src/app/_services/cart.service';

@Component({
    selector: 'app-product-page',
    templateUrl: './product-page.component.html',
    styleUrls: ['./product-page.component.scss']
})
export class ProductPageComponent implements OnInit {
    product!: Product;
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
    }

    addToCart() {
        this.cart.addItem({ ...this.product, quantity: +this.quantity });
    }
}
