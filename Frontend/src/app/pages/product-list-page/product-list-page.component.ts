import {
    Component,
    OnInit
} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Filters } from 'src/app/_models/filters';
import { Product } from 'src/app/_models/product';
import { ProductService } from 'src/app/_services/product.service';

@Component({
    selector: 'app-product-list-page',
    templateUrl: './product-list-page.component.html',
    styleUrls: ['./product-list-page.component.scss']
})
export class ProductListPageComponent implements OnInit {
    products: Product[] = [];
    filters: Filters = { categories: [], attributes: [] };

    constructor(
        private route: ActivatedRoute,
        private productService: ProductService
    ) {
        this.route.queryParams.subscribe(
            params => {
                this.products = [];
            }
        );
    }

    ngOnInit(): void {

        this.route.data.subscribe(
            data => {
                this.products = data.products;
                this.filters = data.filters;
            }
        );
    }

}
