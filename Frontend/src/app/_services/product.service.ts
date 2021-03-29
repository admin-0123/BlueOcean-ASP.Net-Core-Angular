import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { ProductPLP, ProductPDP } from '../_models/product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private baseUrl = environment.apiUrl + 'products/';

  constructor(private http: HttpClient) { }

  getProducts(category?: string | null): Observable<ProductPLP[]> {
    if (category) {
      return this.http.get<ProductPLP[]>(this.baseUrl + "categories?category=" + category);
    }
    return this.http.get<ProductPLP[]>(this.baseUrl);
  }

  getProduct(id: string): Observable<ProductPDP> {
    return this.http.get<ProductPDP>(this.baseUrl + id);
  }
}
