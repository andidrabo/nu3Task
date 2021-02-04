import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        http.get<any>(baseUrl + 'inventory/update-inventory').subscribe(result => {
        }, error => console.error(error));
    }
}
