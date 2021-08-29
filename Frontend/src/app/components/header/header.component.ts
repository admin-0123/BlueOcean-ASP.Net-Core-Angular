/* eslint-disable @angular-eslint/no-empty-lifecycle-method */
import { Component, HostListener, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Category } from 'src/app/_models/product';
import { AutoCompleteService } from 'src/app/_services/auto-complete.service';
import { EntryComponent } from '../entry/entry.component';

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
    filteredCategories: Category[] = [];
    searchInput = '';
    category!: Category;
    isLoading = false;
    applyShadows = false;

    @HostListener('window:scroll', ['$event'])
    onScroll() {
        const verticalOffset = window.pageYOffset
            || document.documentElement.scrollTop
            || document.body.scrollTop
            || 0;

        this.applyShadows = verticalOffset >= 36;
    }

    constructor(
        private autoCompleteService: AutoCompleteService,
        private router: Router
    ) {

    }

    ngOnInit(): void {
    }

    onSearchChange(event: any): void {
        const category = event.target?.value;
        if (category && category.trim()) {
            this.autoCompleteService.search(category)
                .subscribe(response => {
                    console.log(response);
                    this.filteredCategories = response;
                });
        }
        this.filteredCategories = [];
    }

    selectOption(category: Category): void {
        this.searchInput = category.title;
        this.category = category;
        this.filteredCategories = [];
        this.search();
    }

    search(): void {
        this.router.navigate(['/products/' + this.category.name]);
    }
}
