/* eslint-disable @typescript-eslint/no-empty-function */
import { Component, OnInit } from '@angular/core';
import { faShoppingCart, faPlusCircle, faMinusCircle } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {
  faShoppingCart = faShoppingCart;
  faPlusCircle = faPlusCircle;
  faMinusCircle = faMinusCircle;
  cart = false;

  constructor() { }

  ngOnInit(): void {
  }

  cartToggle(): void {
    this.cart = !this.cart;
  }
}
