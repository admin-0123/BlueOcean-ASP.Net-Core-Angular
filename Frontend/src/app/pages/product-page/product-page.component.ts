import {
    Component,
    OnInit
} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Product } from 'src/app/_models/product';
import { CartService } from 'src/app/_services/cart.service';
import { WishlistService } from 'src/app/_services/wishlist.service';

@Component({
    selector: 'app-product-page',
    templateUrl: './product-page.component.html',
    styleUrls: ['./product-page.component.scss']
})
export class ProductPageComponent implements OnInit {
    product!: Product;
    isInCart = false;
    isInWishlist = false;
    quantity = 1;
    columnsToDisplay = ['name', 'value'];

    constructor(
        private route: ActivatedRoute,
        private cartService: CartService,
        private wishlistService: WishlistService
    ) { }

    ngOnInit(): void {
        this.route.data.subscribe(
            data => {
                this.product = data.product;
            }
        );

        this.cartService.watchStorage().subscribe(
            () => {
                this.isInCart = this.cartService.isItemInCart(this.product.id);
            }
        );

        this.isInCart = this.cartService.isItemInCart(this.product.id);

        this.wishlistService.wishlistSub.subscribe(
            () => {
                this.isInWishlist = this.wishlistService.isItemInWishlist(this.product.id);
            }
        );

        this.isInWishlist = this.wishlistService.isItemInWishlist(this.product.id);
    }

    CartAction(): void {
        if (this.isInCart) {
            this.cartService.removeItem(this.product.id);
            return;
        }

        this.cartService.addItem({
            id: this.product.id,
            title: this.product.title,
            price: this.product.price,
            images: this.product.images.map(i => i.url),
            quantity: this.quantity
        });
    }

    WishlistAction(): void {
        if (this.isInWishlist) {
            this.wishlistService.removeItem(this.product.id);
            return;
        }

        this.wishlistService.addItem({
            id: this.product.id,
            title: this.product.title,
            price: this.product.price,
            images: this.product.images.map(i => i.url)
        });
    }
}
