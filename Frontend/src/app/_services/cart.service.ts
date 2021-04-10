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

  constructor(
    private toastr: ToastrService
  ) { }

  addItem(item: ProductInCart): void {
    const cart = JSON.parse(localStorage.getItem('cart') || '[]');
    if (!cart.find((i: Product)  => i.id === item.id)) {
      cart.push(item);
      localStorage.setItem('cart', JSON.stringify(cart));
      this.toastr.success('Successfully added');
      this.storageSub.next('changed');
    } else {
      this.increaseQuantity(item);
    }
  }

  removeItem(item: ProductInCart): void {
    const cart = JSON.parse(localStorage.getItem('cart') || '[]');

    if (cart.find((i: ProductInCart)  => i.id === item.id)) {
      const newCart = cart.filter((i: ProductInCart) => i.id !== item.id );
      localStorage.setItem('cart', JSON.stringify(newCart));
      this.toastr.success('Successfully Removed');
      this.storageSub.next('changed');
    } else {
      this.toastr.success('Error item isn\'t in cart');
    }
  }

  increaseQuantity(item: ProductInCart): void {
    const cart = JSON.parse(localStorage.getItem('cart') || '[]');
    if (cart === []) return;

    const index = cart.findIndex((i: Product)  => i.id === item.id);
    if (index !== -1) {
      cart[index] = {
        ...item,
        quantity: item.quantity + (cart[index].quantity == item.quantity ?  1 : cart[index].quantity)
      };
      localStorage.setItem('cart', JSON.stringify(cart));
      this.storageSub.next('changed');
    }
  }

  decreaseQuality(item: ProductInCart): void {
    const cart = JSON.parse(localStorage.getItem('cart') || '[]');
    if (cart === []) return;

    const index = cart.findIndex((i: Product)  => i.id === item.id);
    if (index !== -1) {
      if(cart[index].quantity == 1) {
        this.removeItem(item);
        return;
      }
      cart[index] = {...item, quantity: item.quantity - 1 };
      localStorage.setItem('cart', JSON.stringify(cart));
      this.storageSub.next('changed');
    }
  }

  getCart() : ProductInCart[] {
    return JSON.parse(localStorage.getItem('cart') || '[]');
  }

  watchStorage(): Observable<any> {
    return this.storageSub.asObservable();
  }
}
