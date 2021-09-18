import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
    Observable,
    of
} from 'rxjs';
import {
    catchError,
    map
} from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { ApiHelper } from '../_helper/api.service';
import {
    Product,
    ProductImage
} from '../_models/product';

@Injectable({
    providedIn: 'root'
})
export class ProductService {
    private baseUrl = environment.apiUrl + 'products/';
    private defaultImage: ProductImage = {
        id: 0,
        primary: true,
        url: 'https://picsum.photos/450/600'
    }

    constructor(
        private http: HttpClient
    ) { }

    getProducts(category: string | string[] | null = null, title: string | null = null, amount: number = 10): Observable<Product[]> {
        const query = ApiHelper.queryBuilder([
            {
                name: 'category',
                value: category
            },
            {
                name: 'title',
                value: title
            },
            {
                name: 'amount',
                value: amount
            }
        ]);

        return this.http.get<Product[]>(this.baseUrl + query)
            .pipe(
                catchError(
                    error => {
                        console.error(error);
                        return [];
                    }
                )
            );
    }

    getProduct(id: string): Observable<Product | null> {
        return this.http.get<Product>(this.baseUrl + id)
            .pipe(
                map(
                    response => ({
                        ...response,
                        attributes: response.attributes?.sort((a, b) => b.priority - a.priority),
                        images: response.images.length > 0 ? response.images : [
                            this.defaultImage,
                            this.defaultImage,
                            this.defaultImage,
                            this.defaultImage,
                            this.defaultImage,
                            this.defaultImage,
                            this.defaultImage
                        ]
                    })
                ),
                catchError(
                    error => {
                        console.error(error);
                        return of(null);
                    }
                )
            );
    }
}
