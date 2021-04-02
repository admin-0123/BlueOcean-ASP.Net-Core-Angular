/* eslint-disable @typescript-eslint/no-empty-function */
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Product } from '../_models/product';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  constructor(
    private toastr: ToastrService
  ) { }

  addItem(item: Product): void {
    const cart = JSON.parse(localStorage.getItem('cart') || '[]');

    if (!cart.isArray(item)) {
      cart.push(item);
      localStorage.setItem('cart', JSON.stringify(cart));
      this.toastr.success('Successful added');
    } else {
      this.toastr.success('Error already in cart');
    }
  }

  removeItem(item: Product): void {
    const cart = JSON.parse(localStorage.getItem('cart') || '[]');

    if (!cart.isArray(item)) {
      cart.push(item);
      const newCart = cart.filter((i: Product) => i !== item );
      localStorage.setItem('cart', JSON.stringify(newCart));
      this.toastr.success('Successful added');
    } else {
      this.toastr.success('Error already in cart');
    }
  }
}
