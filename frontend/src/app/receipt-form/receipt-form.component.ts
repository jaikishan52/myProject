import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-receipt-form',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './receipt-form.component.html',
  styleUrls: ['./receipt-form.component.css'],
})
export class ReceiptFormComponent {
  formData: any = {
    employeeEmail: '',
    date: '',
    amount: null,
    description: '',
  };

  selectedFile: File | null = null;
  responseMessage: string = '';

  constructor(private http: HttpClient) {}

  onFileChange(event: any) {
    this.selectedFile = event.target.files[0];
  }

  onSubmit() {
    if (!this.selectedFile) {
      this.responseMessage = 'Please upload a receipt file.';
      return;
    }

    const formData = new FormData();
    formData.append('employeeEmail', this.formData.employeeEmail);
    formData.append('date', this.formData.date);
    formData.append('amount', this.formData.amount);
    formData.append('description', this.formData.description);
    formData.append('file', this.selectedFile);

    this.http
      .post<any>('http://localhost:5199/api/Receipts/submit', formData)
      .subscribe({
        next: (response) => {
          console.log('Success response:', response);
          this.responseMessage = response.message;
        },
        error: (error) => {
          console.error('Error response:', error);
          this.responseMessage =
            error.error?.detail ||
            'An error occurred while submitting the receipt.';
        },
      });
  }
}
