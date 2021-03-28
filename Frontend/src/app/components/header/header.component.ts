/* eslint-disable @typescript-eslint/no-empty-function */
import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Category } from 'src/app/_models/product';
import { EntryComponent } from '../entry/entry.component';
import { FormBuilder } from '@angular/forms';
import { AutoCompleteService } from 'src/app/_services/auto-complete.service';

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

  constructor(
    private dialog: MatDialog,
    private autoCompleteService: AutoCompleteService
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

  onSearchChange(event: any) : void {
    const category = event.target?.value;
    if(category && category.trim()) {
      this.autoCompleteService.search(category)
        .subscribe(response => {
          console.log(response);
          this.filteredCategories = response;
      });
    }
  }

  selectOption(category: Category) {
    this.searchInput = category.title;
    this.category = category;
    this.filteredCategories = [];
    this.search();
  }

  search() {
    console.log(this.category);
  }
}
