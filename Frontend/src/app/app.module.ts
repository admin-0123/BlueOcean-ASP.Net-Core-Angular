import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule } from '@angular/material/dialog';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { JwtModule } from '@auth0/angular-jwt';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomePageComponent } from './pages/home-page/home-page.component';
import { ProductsListComponent } from './components/products-list/products-list.component';
import { ProductCardComponent } from './components/product-card/product-card.component';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { ProductPageComponent } from './pages/product-page/product-page.component';
import { EntryComponent } from './components/entry/entry.component';
import { ProductListPageComponent } from './pages/product-list-page/product-list-page.component';
import { CartComponent } from './components/cart/cart.component';
import { CheckoutPageComponent } from './pages/checkout-page/checkout-page.component';

export function tokenGetter(): string | null {
  return localStorage.getItem('token');
}

@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,
    ProductsListComponent,
    ProductCardComponent,
    HeaderComponent,
    FooterComponent,
    ProductPageComponent,
    EntryComponent,
    ProductListPageComponent,
    CartComponent,
    CheckoutPageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    FormsModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatInputModule,
    MatAutocompleteModule,
    MatFormFieldModule,
    FontAwesomeModule,
    JwtModule.forRoot({
      config: {
         tokenGetter,
         allowedDomains: ['localhost:5001'],
         disallowedRoutes: ['localhost:5001/api/auth']
      }
    }),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
