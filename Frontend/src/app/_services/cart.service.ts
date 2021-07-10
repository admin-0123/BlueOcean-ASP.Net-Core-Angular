/* eslint-disable @typescript-eslint/no-empty-function */
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Observable, Subject } from 'rxjs';
import { Product, ProductInCart } from '../_models/product';

@Injectable({
    providedIn: 'root'
})
export class CartService {
    private storageSub = new Subject<string>();
    cart: ProductInCart[];

    constructor(
        private toastr: ToastrService
    ) {
        this.cart = JSON.parse(localStorage.getItem('cart') || '[]');
    }

    addItem(item: ProductInCart): void {
        if (!this.isItemInCart(item.id)) {
            this.cart.push(item);
            this.updateCart(this.cart)
            this.toastr.success('Successfully added');
            return;
        }

        this.increaseQuantity(item);
        this.storageSub.next('changed');
    }

    removeItem(id: string): void {
        if (this.isItemInCart(id)) {
            this.cart = this.cart.filter((i: ProductInCart) => i.id !== id);
            this.updateCart(this.cart)
            this.toastr.success('Successfully Removed');
            return;
        }

        this.toastr.success('Error item isn\'t in cart');
    }

    increaseQuantity(item: ProductInCart): void {
        if (this.cart === []) return;

        const index = this.cart.findIndex((i: Product) => i.id === item.id);

        if (index !== -1) {
            this.cart[index] = {
                ...item,
                quantity: item.quantity + (this.cart[index].quantity == item.quantity ? 1 : this.cart[index].quantity)
            };
            this.updateCart(this.cart)
        }
    }

    decreaseQuality(item: ProductInCart): void {
        if (this.cart === []) return;

        const index = this.cart.findIndex((i: Product) => i.id === item.id);
        if (index !== -1) {
            if (this.cart[index].quantity == 1) {
                this.removeItem(item.id);
                return;
            }
            this.cart[index] = { ...item, quantity: item.quantity - 1 };
            this.updateCart(this.cart)
        }
    }

    getCart(): ProductInCart[] {
        return JSON.parse(localStorage.getItem('cart') || '[]');
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
        localStorage.setItem('cart', JSON.stringify(newCart));
        this.storageSub.next('changed');
    }
}
