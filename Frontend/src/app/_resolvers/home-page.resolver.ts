import { Injectable } from '@angular/core';
import {
    Router, Resolve,
    RouterStateSnapshot,
    ActivatedRouteSnapshot
} from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Product } from '../_models/product';
import { ProductService } from '../_services/product.service';

@Injectable({
    providedIn: 'root'
})
export class HomePageResolver implements Resolve<Product[]> {

    constructor(
    private toastr: ToastrService,
    private productService: ProductService
    ) {}

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<Product[]> {
        return this.productService.getProducts(null, 5)
            .pipe(
                catchError(error => {
                    this.toastr.error('Problem retrieving data');
                    console.error(error);
                    return [];
                })
            );
    }
}
