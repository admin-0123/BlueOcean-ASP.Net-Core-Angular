import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import { Observable, of } from 'rxjs';
import { ProductPLP } from '../_models/product';
import { ProductService } from '../_services/product.service';
import { catchError, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ProductListResolver implements Resolve<ProductPLP[]> {
  constructor(private productService: ProductService) {}

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<ProductPLP[]> {
    return this.productService.getProducts()
      .pipe(
        // map(response => {
        //   console.log(response);
        //   return response;
        // })
        // catchError(
        //   error => {
        //     console.log(error);
        //     return of(null);
        //   }
        // )
      );
  }
}
