import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RedactionService {
  private apiUrl = 'https://localhost:7084/api/Redaction/redact';

  constructor(private http: HttpClient) { }

  submitFiles(files: File[]): Observable<any> {
    const formData = new FormData();
    files.forEach(file => {
      formData.append('files', file, file.name);
    });
    return this.http.post(this.apiUrl, formData);
  }
}
