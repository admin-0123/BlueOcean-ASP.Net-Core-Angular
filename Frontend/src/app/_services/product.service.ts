import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Product } from '../_models/product';

@Injectable({
    providedIn: 'root'
})
export class ProductService {
    private baseUrl = environment.apiUrl + 'products/';

    constructor(private http: HttpClient) { }

    getProducts(category?: string | null): Observable<Product[]> {
        if (category) {
            return this.http.get<Product[]>(this.baseUrl + 'categories?category=' + category);
        }
        return this.http.get<Product[]>(this.baseUrl);
    }

    getProduct(id: string): Observable<Product> {
        return this.http.get<Product>(this.baseUrl + id);
    }
}
