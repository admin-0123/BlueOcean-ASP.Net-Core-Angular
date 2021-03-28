import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import { Observable, of } from 'rxjs';
import { ProductPDP } from '../_models/product';
import { ProductService } from '../_services/product.service';
import { catchError, map } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class ProductResolver implements Resolve<ProductPDP | null> {
  constructor(
    private productService: ProductService,
    private toastr: ToastrService
  ) {}

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<ProductPDP | null> {
    return this.productService.getProduct(route.params['id'])
      .pipe(
        catchError(error => {
          this.toastr.error('Problem retrieving data: Post-List');
          // this.router.navigate(['/']);
          return of(null);
        })
      );
  }
}
