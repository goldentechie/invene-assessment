import { Component } from '@angular/core';
import { RedactionService } from './redaction.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  selectedFiles: File[] = [];
  title = "PHI Redaction";
  responses: string[] = [];  // Array to store response messages

  constructor(private redactionService: RedactionService, private snackBar: MatSnackBar) { }

  uploadFile(event: Event) {
    const element = event.target as HTMLInputElement;
    let files = element.files;
    if (files && files.length > 0) {
      this.selectedFiles = Array.from(files);
    } else {
      this.selectedFiles = [];
    }
  }

  submitFiles() {
    if (this.selectedFiles.length > 0) {
      this.redactionService.submitFiles(this.selectedFiles)
        .subscribe(
          (res: string[]) => {  // Ensure type is correctly defined based on the response
            this.responses = res;  // Store response messages
            this.snackBar.open('Files successfully processed', 'Close', {
              duration: 3000
            });
          },
          error => {
            console.error('Error:', error);
            this.snackBar.open('Failed to process files', 'Close', {
              duration: 3000
            });
          }
        );
    } else {
      this.snackBar.open('No files selected', 'Close', {
        duration: 3000
      });
    }
  }
}
