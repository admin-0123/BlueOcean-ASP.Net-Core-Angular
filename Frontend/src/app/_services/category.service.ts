import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Category } from '../_models/product';

@Injectable({
    providedIn: 'root'
})
export class CategoryService {
    private baseUrl = environment.apiUrl + 'categories/';

    constructor(
        private http: HttpClient
    ) { }

    getCategories(length: number = 5): Observable<Category[]> {
        return this.http.get<Category[]>(this.baseUrl + length);
    }
}
