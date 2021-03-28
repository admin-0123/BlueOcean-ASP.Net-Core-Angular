import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomePageComponent } from './pages/home-page/home-page.component';
import { ProductPageComponent } from './pages/product-page/product-page.component';
import { ProductListResolver } from './_resolvers/product-list.resolver';
import { ProductResolver } from './_resolvers/product.resolver';

const routes: Routes = [
  { path: '', component: HomePageComponent, resolve: { products: ProductListResolver } },
  { path: 'products/:id', component: ProductPageComponent, resolve: { product: ProductResolver}}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
