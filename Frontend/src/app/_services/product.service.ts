import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { ApiHelper } from '../_helper/api.service';
import { Product } from '../_models/product';

@Injectable({
    providedIn: 'root'
})
export class ProductService {
    private baseUrl = environment.apiUrl + 'products/';

    constructor(
        private http: HttpClient
    ) { }

    getProducts(category: string | string[] | null = null, amount: number = 10): Observable<Product[]> {
        const query = ApiHelper.queryBuilder([
            {
                name: 'category',
                value: category
            },
            {
                name: 'amount',
                value: amount
            }
        ]);

        return this.http.get<Product[]>(this.baseUrl + query);
    }

    getProduct(id: string): Observable<Product> {
        return this.http.get<Product>(this.baseUrl + id)
            .pipe(
                map(
                    response => ({
                        ...response,
                        attributes: response.attributes?.sort((a, b) => b.priority - a.priority)
                    })
                )
            );
    }
}
