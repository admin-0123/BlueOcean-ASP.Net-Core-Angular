import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { ToastrService } from 'ngx-toastr';
import { Observable, of, Subject } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { ProductInCart } from '../_models/product';
import { AuthService } from './auth.service';

@Injectable({
    providedIn: 'root'
})
export class CartService {
    private storageSub = new Subject<string>();
    private hubConnection: HubConnection | undefined;
    baseUrl = environment.apiUrl + 'customer/cart/';
    hubUrl = environment.hubUrl + 'customer';
    cart: ProductInCart[] = [];

    constructor(
        private toastr: ToastrService,
        private http: HttpClient,
        private auth: AuthService
    ) {
        this.getRemoteCart().subscribe(
            data => {
                this.updateCart(data as ProductInCart[]);
            },
            error => {
                this.cart = JSON.parse(this.getLocalCart());
                console.error(error);
            }
        );
    }

    createHubConnection() {
        this.hubConnection = new HubConnectionBuilder()
            .withUrl(this.hubUrl, {
                accessTokenFactory: () => this.auth.getLocalToken()
            })
            .withAutomaticReconnect()
            .build();

        this.hubConnection.start()
            .catch((error: any)  => console.error(error));

        this.hubConnection.on('OnCartUpdate',
            (data: any) => {
                console.log('Triggered');
                this.getRemoteCart().subscribe(
                    data => {
                        this.updateCart(data as ProductInCart[]);
                    },
                    error => {
                        console.error(error);
                    }
                );
            }
        );
    }

    stopHubConnection() {
        if (this.hubConnection) {
            this.hubConnection.stop();
        }
    }

    addItem(item: ProductInCart): void {
        if (!this.isItemInCart(item.id)) {
            this.cart.push(item);
            this.updateCart(this.cart);
            this.toastr.success('Successfully added');
            return;
        }

        this.increaseQuantity(item);
        this.storageSub.next('changed');
    }

    removeItem(id: string): void {
        if (this.isItemInCart(id)) {
            this.cart = this.cart.filter((i: ProductInCart) => i.id !== id);
            this.updateCart(this.cart);
            this.toastr.success('Successfully Removed');
            return;
        }

        this.toastr.warning('Error item isn\'t in cart');
    }

    increaseQuantity(item: ProductInCart): void {
        if (this.cart === []) return;

        const index = this.cart.findIndex((i: ProductInCart) => i.id === item.id);

        if (index !== -1) {
            this.cart[index] = {
                ...item,
                quantity: item.quantity + (this.cart[index].quantity == item.quantity ? 1 : this.cart[index].quantity)
            };
            this.updateCart(this.cart);
        }
    }

    decreaseQuality(item: ProductInCart): void {
        if (this.cart === []) return;

        const index = this.cart.findIndex((i: ProductInCart) => i.id === item.id);
        if (index !== -1) {
            if (this.cart[index].quantity == 1) {
                this.removeItem(item.id);
                return;
            }
            this.cart[index] = { ...item, quantity: item.quantity - 1 };
            this.updateCart(this.cart);
        }
    }

    getRemoteCart(): Observable<ProductInCart[]> {
        return this.http.get<{products: ProductInCart[]}>(this.baseUrl)
            .pipe(
                map(
                    response => response.products
                )
            );
    }

    watchStorage(): Observable<any> {
        return this.storageSub.asObservable();
    }

    isItemInCart(id: string): boolean {
        if (this.cart.find((i: ProductInCart) => i.id === id)) {
            return true;
        }

        return false;
    }

    updateCart(newCart: ProductInCart[] | []): void {
        this.cart = newCart;
        localStorage.setItem('cart', JSON.stringify(newCart));
        this.storageSub.next('changed');
    }

    SaveCart(): Observable<any> {
        return this.http.post(this.baseUrl, { products: this.cart })
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
