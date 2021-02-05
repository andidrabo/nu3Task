"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var FileUploadService = /** @class */ (function () {
    function FileUploadService(http) {
        this.http = http;
    }
    FileUploadService.prototype.PostFile = function (file, url) {
        var fi = file.srcElement;
        if (fi.files && fi.files[0]) {
            var fileToUpload = fi.files[0];
            var formData = new FormData();
            formData.append(fileToUpload.name, fileToUpload);
            return this.http.post(url, formData);
        }
    };
    return FileUploadService;
}());
exports.FileUploadService = FileUploadService;
//# sourceMappingURL=FileUploadService.js.map