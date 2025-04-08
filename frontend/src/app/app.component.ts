import { Component } from '@angular/core';
import { ReceiptFormComponent } from './receipt-form/receipt-form.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [ReceiptFormComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'frontend';
}
