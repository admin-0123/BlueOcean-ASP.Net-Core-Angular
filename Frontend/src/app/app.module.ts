import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import {
    FormsModule,
    ReactiveFormsModule
} from '@angular/forms';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { JwtModule } from '@auth0/angular-jwt';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { ToastrModule } from 'ngx-toastr';
import { environment } from '../environments/environment';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CartComponent } from './components/cart/cart.component';
import { EntryComponent } from './components/entry/entry.component';
import { FooterComponent } from './components/footer/footer.component';
import { HeaderComponent } from './components/header/header.component';
import { LoadingScreenComponent } from './components/loading-screen/loading-screen.component';
import { ProductCardComponent } from './components/product-card/product-card.component';
import { ProductFilterComponent } from './components/product-filter/product-filter.component';
import { ProductsListComponent } from './components/products-list/products-list.component';
import { WishlistComponent } from './components/wishlist/wishlist.component';
import { AccountPageComponent } from './pages/account-page/account-page.component';
import { CheckoutPageComponent } from './pages/checkout-page/checkout-page.component';
import { HomePageComponent } from './pages/home-page/home-page.component';
import { ProductListPageComponent } from './pages/product-list-page/product-list-page.component';
import { ProductPageComponent } from './pages/product-page/product-page.component';
import { Reducers } from './store/app.store';
import { ClickOutsideDirective } from './_directives/click-outside.directive';

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
        CheckoutPageComponent,
        WishlistComponent,
        ClickOutsideDirective,
        ProductFilterComponent,
        LoadingScreenComponent,
        AccountPageComponent
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
                allowedDomains: [ environment.baseUrl ],
                disallowedRoutes: [ environment.baseUrl + '/api/auth' ]
            }
        }),
        StoreModule.forRoot(Reducers, {}),
        StoreDevtoolsModule.instrument({ maxAge: 25, logOnly: environment.production }),
        EffectsModule.forRoot([])
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule { }
