import { Injectable } from '@angular/core';
import {
  Resolve
} from '@angular/router';
import { Observable, of } from 'rxjs';
import { ProductPLP } from '../_models/product';
import { ProductService } from '../_services/product.service';
import { catchError } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class ProductListResolver implements Resolve<ProductPLP[] | null> {
  constructor(
    private productService: ProductService,
    private toastr: ToastrService
  ) {}

  resolve(): Observable<ProductPLP[] | null> {
    return this.productService.getProducts()
      .pipe(
        catchError(
          error => {
            this.toastr.error('Problem retrieving data');
            console.error(error);
            return of(null);
          }
        )
      )
  }
}
