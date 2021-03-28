import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Product } from '../_models/product';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private baseUrl = environment.apiUrl + 'products';

  constructor(private http: HttpClient) { }

  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(this.baseUrl)
      .pipe(
        map(response => {
          response.forEach(
            i => {
              i.images = JSON.parse(i.images);
            }
          )
          return response;
        })
      )
  }
}
