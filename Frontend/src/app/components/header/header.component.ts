/* eslint-disable @angular-eslint/no-empty-lifecycle-method */
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { faUser } from '@fortawesome/free-solid-svg-icons';
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
    faUser = faUser;

    constructor(
        private dialog: MatDialog,
        private autoCompleteService: AutoCompleteService,
        private router: Router
    ) {

    }

    ngOnInit(): void {
    }

    authDialog(): void {
        const dialogRef = this.dialog.open(EntryComponent, {
            height: '400px',
            width: '600px',
        });
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
