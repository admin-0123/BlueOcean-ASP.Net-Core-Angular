import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomePageComponent } from './pages/home-page/home-page.component';
import { ProductListResolver } from './_resolvers/product-list.resolver';

const routes: Routes = [
  { path: '', component: HomePageComponent, resolve: { products: ProductListResolver } }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
