import {
    AfterViewInit,
    Component,
    OnInit
} from '@angular/core';
import {
    ActivatedRoute,
    Router
} from '@angular/router';
import { faHeart as farHeart } from '@fortawesome/free-regular-svg-icons';
import { faHeart as fasHeart } from '@fortawesome/free-solid-svg-icons';
import { Store } from '@ngrx/store';
import Splide from '@splidejs/splide';
import { AppStore } from 'src/app/store/app.store';
import { setLoadingScreen } from 'src/app/store/general/general.actions';
import { Product } from 'src/app/_models/product';
import { CartService } from 'src/app/_services/cart.service';
import { WishlistService } from 'src/app/_services/wishlist.service';

@Component({
    selector: 'app-product-page',
    templateUrl: './product-page.component.html',
    styleUrls: ['./product-page.component.scss']
})
export class ProductPageComponent implements OnInit, AfterViewInit {
    fasHeart = fasHeart;
    farHeart = farHeart;
    product!: Product;
    isInCart = false;
    isInWishlist = false;
    quantity = 1;
    splied: any = null;
    spliedThumbnail: any = null;

    constructor(
        private route: ActivatedRoute,
        private cartService: CartService,
        private wishlistService: WishlistService,
        private store: Store<AppStore>,
        private router: Router
    ) { }

    ngAfterViewInit(): void {
        this.splied = new Splide('#splide', {
            pagination : false,
            autoHeight: true,
            type: 'loop',
            arrows: false,
            gap: '30px'
        } );

        this.spliedThumbnail = new Splide('#splide-thumbnail', {
            arrows: false,
            pagination : false,
            perPage: 3,
            direction: 'ttb',
            gap: '20px',
            height: '600px',
            autoHeight: true,
            focus: 'center'
        } ).mount();

        this.splied.sync(this.spliedThumbnail).mount();
    }

    ngOnInit(): void {
        this.route.data.subscribe(
            data => {
                this.product = data.product;
                this.store.dispatch(setLoadingScreen({ loadingScreen: false }));
            }
        );

        this.cartService.cartSub.subscribe(
            () => {
                this.isInCart = this.cartService.isItemInCart(this.product.id);
            }
        );

        this.wishlistService.wishlistSub.subscribe(
            () => {
                this.isInWishlist = this.wishlistService.isItemInWishlist(this.product.id);
            }
        );
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
            images: [ this.product.images.map(i => i.url)[0] ],
            quantity: this.quantity,
            url: this.router.url
        });
    }

    WishlistAction(): void {
        console.log(this.router.url);
        if (this.isInWishlist) {
            this.wishlistService.removeItem(this.product.id);
            return;
        }

        this.wishlistService.addItem({
            id: this.product.id,
            title: this.product.title,
            price: this.product.price,
            images: [ this.product.images.map(i => i.url)[0] ],
            url: this.router.url
        });
    }
}
