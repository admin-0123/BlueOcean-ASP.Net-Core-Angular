import {
    Component,
    OnInit
} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Product } from 'src/app/_models/product';

@Component({
    selector: 'app-product-list-page',
    templateUrl: './product-list-page.component.html',
    styleUrls: ['./product-list-page.component.scss']
})
export class ProductListPageComponent implements OnInit {
    products: Product[] = [];

    constructor(
        private route: ActivatedRoute
    ) { }

    ngOnInit(): void {
        this.route.data.subscribe(
            data => {
                this.products = data.products;
            }
        );
    }

}
