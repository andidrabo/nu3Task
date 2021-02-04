import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { InventoryRecord } from '../models/inventory';
import { Observable } from 'rxjs';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent {
    inventory: InventoryRecord[];
    httpClient: HttpClient;
    baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.httpClient = http;
        this.baseUrl = baseUrl;

        http.get<InventoryRecord[]>(`${baseUrl}inventory/get-inventory`).subscribe(result => {
            this.inventory = result;
        }, error => console.error(error));
    }

    updateInventory(event: any) {
        let fi = event.srcElement;
        if (fi.files && fi.files[0]) {
            let fileToUpload = fi.files[0];

            let formData: FormData = new FormData();
            formData.append(fileToUpload.name, fileToUpload);

            this.httpClient.post(`${this.baseUrl}inventory/update-inventory`, formData)
                .subscribe(r => console.log(r));
        }
    }
}
