import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
    providedIn: 'root',
})
export class FileUploadService {

    constructor(private http: HttpClient) { }

    PostFile(file: any, url: string): Observable<any> {
        const fi = file.srcElement;
        if (fi.files && fi.files[0]) {
            let fileToUpload = fi.files[0];

            let formData: FormData = new FormData();
            formData.append(fileToUpload.name, fileToUpload);

            return this.http.post(url, formData);
        }
    }
}
