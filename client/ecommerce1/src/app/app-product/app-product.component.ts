import { Component } from '@angular/core';
import { ProductService } from '../product.service';

@Component({
  selector: 'app-app-product',
  templateUrl: './app-product.component.html',
  styleUrls: ['./app-product.component.css']
})
export class AppProductComponent {
  products: { id: number, name: string }[] = [];
  newProduct = '';

  constructor(private productService: ProductService) {}

  ngOnInit() {
    this.load();
  }

  load() {
    this.productService.getProducts().subscribe(data => this.products = data);
  }

  add() {
    this.productService.addProduct(this.newProduct).subscribe(() => this.load());
    this.newProduct = '';
  }

  delete(id: number) {
    this.productService.deleteProduct(id).subscribe(() => this.load());
  }
}
