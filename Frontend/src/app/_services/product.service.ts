import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ProductPLP, ProductPDP } from '../_models/product';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private baseUrl = environment.apiUrl + 'products/';

  constructor(private http: HttpClient) { }

  getProducts(): Observable<ProductPLP[]> {
    return this.http.get<ProductPLP[]>(this.baseUrl)
      .pipe(
        map(response => {
          console.log(response);
          return response;
        })
      )
  }

  getProduct(id: string): Observable<ProductPDP> {
    return this.http.get<ProductPDP>(this.baseUrl + id)
      .pipe(
        map(product => {
          return product;
        })
      )
  }
}
