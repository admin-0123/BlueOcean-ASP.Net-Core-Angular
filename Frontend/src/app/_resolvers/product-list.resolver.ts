import { Injectable } from '@angular/core';
import {
    ActivatedRouteSnapshot,
    Resolve
} from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import {
    Observable
} from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Product } from '../_models/product';
import { ProductService } from '../_services/product.service';

@Injectable({
    providedIn: 'root'
})
export class ProductListResolver implements Resolve<Product[]> {
    constructor(
        private productService: ProductService,
        private toastr: ToastrService
    ) { }

    resolve(route: ActivatedRouteSnapshot): Observable<Product[]> {
        return this.productService.getProducts(route.queryParams?.category, route.queryParams?.title, 20)
            .pipe(
                catchError(
                    error => {
                        this.toastr.error('Problem retrieving data');
                        console.error(error);
                        return [];
                    }
                )
            );
    }
}
