import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { ProductInCart } from '../_models/product';
import { AuthService } from './auth.service';

@Injectable({
    providedIn: 'root'
})
export class OrderService {
    private baseUrl = environment.apiUrl + 'orders/';

    constructor(
        private http: HttpClient,
        private toastr: ToastrService,
        private authService: AuthService
    ) { }

    placeOrder(products: ProductInCart[], totalPrice: number): Observable<void | null> {
        const productForOrder = products.map(
            p => {
                return {
                    id: p.id,
                    price: p.price,
                    quantity: p.quantity
                }
            }
        );

        return this.http.post(
            this.baseUrl, {
            products: productForOrder,
            totalPrice,
            userEmail: this.authService.getDecodedToken().unique_name
        }
        ).pipe(
            map(
                response => {
                    this.toastr.success('Successful!');
                    console.log(response);
                }
            ),
            catchError(
                error => {
                    this.toastr.error(error.message);
                    console.error(error);
                    return of(null);
                }
            )
        );
    }
}
