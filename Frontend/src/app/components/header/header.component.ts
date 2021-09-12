/* eslint-disable @angular-eslint/no-empty-lifecycle-method */
import {
    Component,
    HostListener,
    OnInit
} from '@angular/core';
import { Router } from '@angular/router';
import { Category } from 'src/app/_models/filters';
import { CategoryService } from 'src/app/_services/category.service';

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
    filteredCategories: Category[] = [];
    category!: Category;
    categories: Category[] = [];
    isLoading = false;
    applyShadows = false;
    searchInput: string = '';

    @HostListener('window:scroll', ['$event'])
    onScroll() {
        const verticalOffset = window.pageYOffset
            || document.documentElement.scrollTop
            || document.body.scrollTop
            || 0;

        this.applyShadows = verticalOffset >= 36;
    }

    constructor(
        // private autoCompleteService: AutoCompleteService,
        private router: Router,
        private categoryService: CategoryService
    ) {

    }

    ngOnInit(): void {
        this.categoryService.getCategories().subscribe(
            categories => this.categories = categories
        );
    }

    onSearchChange(event: any): void {
        this.searchInput = event.target?.value;
        // if (category && category.trim()) {
        //     this.autoCompleteService.search(category)
        //         .subscribe(response => {
        //             console.log(response);
        //             this.filteredCategories = response;
        //         });
        // }
        // this.filteredCategories = [];
    }

    selectOption(category: Category): void {
        // this.searchInput = category.title;
        // this.category = category;
        // this.filteredCategories = [];
        // this.search();
    }

    search(): void {
        this.router.navigate(['/products'], { queryParams: { title: this.searchInput } });
    }

    goToCategory(category: string = ''): void {
        this.router.navigate(['/products/' + category]);
    }
}
