import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { InventoryRecord } from '../models/inventory';

@Component({
  selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent {
    inventory: InventoryRecord[];

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        http.get<InventoryRecord[]>(baseUrl + 'inventory/get-inventory').subscribe(result => {
            this.inventory = result;
        }, error => console.error(error));
    }
}
