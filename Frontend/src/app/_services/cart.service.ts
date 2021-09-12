import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import {
    BehaviorSubject,
    Observable,
    of
} from 'rxjs';
import {
    catchError,
    map
} from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { ProductInCart } from '../_models/product';
import { AuthService } from './auth.service';

@Injectable({
    providedIn: 'root'
})
export class CartService {
    // private hubConnection: HubConnection | undefined;
    // hubUrl = environment.hubUrl + 'customer';
    baseUrl = environment.apiUrl + 'customer/cart/';
    cartSub = new BehaviorSubject<ProductInCart[]>(JSON.parse(this.getLocalCart()));

    constructor(
        private toastr: ToastrService,
        private http: HttpClient,
        private auth: AuthService
    ) {
        this.auth.isLoggedInSub.subscribe(
            isLoggedIn => {
                if (isLoggedIn) {
                    this.getRemoteCart().subscribe(
                        data => this.updateCart(data),
                        error => console.error(error)
                    );

                    this.cartSub.subscribe(
                        () => this.SaveCartToDb().subscribe()
                    );
                }
            }
        );
    }

    // createHubConnection() {
    //     this.hubConnection = new HubConnectionBuilder()
    //         .withUrl(this.hubUrl, {
    //             accessTokenFactory: () => this.auth.getLocalTokenString()
    //         })
    //         .withAutomaticReconnect()
    //         .build();

    //     this.hubConnection.start()
    //         .catch((error: any)  => console.error(error));

    //     this.hubConnection.on('OnCartUpdate',
    //         (data: any) => {
    //             console.log('Triggered');
    //             this.getRemoteCart().subscribe(
    //                 data => {
    //                     this.updateCart(data as ProductInCart[]);
    //                 },
    //                 error => {
    //                     console.error(error);
    //                 }
    //             );
    //         }
    //     );
    // }

    // stopHubConnection() {
    //     if (this.hubConnection) {
    //         this.hubConnection.stop();
    //     }
    // }

    watchStorage(): Observable<any> {
        return this.cartSub.asObservable();
    }

    addItem(item: ProductInCart): void {
        if (!this.isItemInCart(item.id)) {
            const newCart = this.cartSub.getValue();
            newCart.push(item);
            this.updateCart(newCart);
            this.toastr.success('Successfully added');
            return;
        }

        this.increaseQuantity(item);
    }

    removeItem(id: string): void {
        this.updateCart(
            this.cartSub.getValue().filter((i: ProductInCart) => i.id !== id)
        );
    }

    increaseQuantity(item: ProductInCart): void {
        const cart = this.cartSub.getValue();
        const index = cart.findIndex((i: ProductInCart) => i.id === item.id);

        if (index !== -1) {
            cart[index] = {
                ...item,
                quantity: item.quantity + (cart[index].quantity === item.quantity ? 1 : cart[index].quantity)
            };
            this.updateCart(cart);
        }
    }

    decreaseQuality(item: ProductInCart): void {
        const cart = this.cartSub.getValue();
        const index = cart.findIndex((i: ProductInCart) => i.id === item.id);

        if (index !== -1) {
            if (cart[index].quantity === 1) {
                this.removeItem(item.id);
                return;
            }
            cart[index] = { ...item, quantity: item.quantity - 1 };
            this.updateCart(cart);
        }
    }

    getRemoteCart(): Observable<ProductInCart[]> {
        return this.http.get<{products: ProductInCart[]}>(this.baseUrl)
            .pipe(map(response => response.products));
    }

    isItemInCart(id: string): boolean {
        return !!this.cartSub.getValue().find((i) => i.id === id);
    }

    updateCart(newCart: ProductInCart[] | []): void {
        this.cartSub.next(newCart);
        localStorage.setItem('cart', JSON.stringify(newCart));
    }

    SaveCartToDb(): Observable<any> {
        return this.http.post(this.baseUrl, { products: this.cartSub.getValue() })
            .pipe(
                catchError(
                    (error) => {
                        console.error(error);
                        return of(null);
                    }
                )
            );
    }

    getLocalCart(): string {
        return localStorage.getItem('cart') || '[]';
    }
}
