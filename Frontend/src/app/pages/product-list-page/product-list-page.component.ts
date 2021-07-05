import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-product-list-page',
    templateUrl: './product-list-page.component.html',
    styleUrls: ['./product-list-page.component.scss']
})
export class ProductListPageComponent implements OnInit {

    constructor(
        private route: ActivatedRoute
    ) { }

    ngOnInit(): void {
        this.route.params.subscribe(
            data => {
                console.log(data.category);
            }
        );
    }

}
