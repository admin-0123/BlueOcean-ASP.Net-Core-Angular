import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Product } from 'src/app/_models/product';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.scss']
})
export class HomePageComponent implements OnInit {
  products: Product[] = [];

  constructor(
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {

  }

}
