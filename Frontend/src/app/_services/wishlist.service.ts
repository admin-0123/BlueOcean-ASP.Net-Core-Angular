import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { ProductInWishlist } from '../_models/product';

@Injectable({
    providedIn: 'root'
})
export class WishlistService {
    baseUrl = environment.apiUrl + 'customer/wishlist/';
    wishlistSub = new BehaviorSubject<ProductInWishlist[]>(JSON.parse(this.getLocalWishlist()));

    constructor(
      private toastr: ToastrService,
      private http: HttpClient
    ) {
        this.getRemoteCart().subscribe(
            data => this.updateWishlist(data),
            error => console.error(error)
        );

        this.wishlistSub.subscribe(
            () => this.SaveWishlistToDb().subscribe()
        );
    }

    getRemoteCart(): Observable<ProductInWishlist[]> {
        return this.http.get<{products: ProductInWishlist[]}>(this.baseUrl)
            .pipe(map(response => response.products));
    }

    addItem(item: ProductInWishlist): void {
        if (this.isItemInWishlist(item.id)) return;
        const newWishlist = this.wishlistSub.getValue();
        newWishlist.push(item);
        this.updateWishlist(newWishlist);
        this.toastr.success('Successfully added to the wishlist');
    }

    removeItem(id: string): void {
        this.updateWishlist(
            this.wishlistSub.getValue().filter((i: ProductInWishlist) => i.id !== id)
        );
    }

    isItemInWishlist(id: string): boolean {
        if (this.wishlistSub.getValue().find((i) => i.id === id)) {
            return true;
        }

        return false;
    }

    updateWishlist(wishlist: ProductInWishlist[] | []): void {
        this.wishlistSub.next(wishlist);
        localStorage.setItem('wishlist', JSON.stringify(wishlist));
    }

    getLocalWishlist(): string {
        return localStorage.getItem('wishlist') || '[]';
    }

    SaveWishlistToDb(): Observable<any> {
        return this.http.post(this.baseUrl, { products: this.wishlistSub.getValue() })
            .pipe(
                catchError(
                    (error) => {
                        console.error(error);
                        return of(null);
                    }
                )
            );
    }
}
