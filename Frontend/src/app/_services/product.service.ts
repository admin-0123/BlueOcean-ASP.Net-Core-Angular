import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ProductPLP, ProductPDP } from '../_models/product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private baseUrl = environment.apiUrl + 'products/';

  constructor(private http: HttpClient) { }

  getProducts(): Observable<ProductPLP[]> {
    return this.http.get<ProductPLP[]>(this.baseUrl);
  }

  getProduct(id: string): Observable<ProductPDP> {
    return this.http.get<ProductPDP>(this.baseUrl + id);
  }
}
