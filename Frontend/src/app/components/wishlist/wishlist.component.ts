import { Component, OnInit } from '@angular/core';
import { faHeart } from '@fortawesome/free-solid-svg-icons';
import { ProductInWishlist } from 'src/app/_models/product';
import { WishlistService } from 'src/app/_services/wishlist.service';

@Component({
    selector: 'app-wishlist',
    templateUrl: './wishlist.component.html',
    styleUrls: ['./wishlist.component.scss']
})
export class WishlistComponent implements OnInit {
    faHeart = faHeart;
    isVisible = false;
    wishlist: ProductInWishlist[] = [];

    constructor(
        private wishlistService: WishlistService
    ) { }

    ngOnInit(): void {
        this.wishlistService.wishlistSub.subscribe(
            (wishlist) => this.wishlist = wishlist
        );
    }

    onWishlistClick(): void {
        this.isVisible = !this.isVisible;
    }
}
