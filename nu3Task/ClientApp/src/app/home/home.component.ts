import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { InventoryRecord } from '../models/inventory';
import { Observable } from 'rxjs';
import { FileUploadService } from '../Services/FileUploadService';
import { Product } from '../models/products';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent {
    inventory: InventoryRecord[] = [];
    products: Product[] = [];

    constructor(private http: HttpClient,
        @Inject('BASE_URL') private baseUrl: string,
        private fileUploadService: FileUploadService) {
        this.getInventory();
    }

    getInventory() {
        this.http.get<InventoryRecord[]>(`${this.baseUrl}inventory/get-inventory`).subscribe(result => {
            this.inventory = result;
        }, error => { console.error(error); });
    }

    getProducts() {
        this.http.get<Product[]>(`${this.baseUrl}products/get-products`).subscribe(result => {
            this.products = result;
        }, error => { console.error(error); });
    }

    updateInventory(file: any) {
        const actionUrl = `${this.baseUrl}inventory/update-inventory`;
        this.fileUploadService.PostFile(file, actionUrl)
            .subscribe(result => { this.getInventory() },
                error => { console.error(error); });
    }

    updateProducts(file: any) {
        const actionUrl = `${this.baseUrl}products/update-products`;
        this.fileUploadService.PostFile(file, actionUrl)
            .subscribe(result => { this.getProducts() },
                error => { console.error(error); });
    }
}
