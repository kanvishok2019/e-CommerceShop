import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';

import { PurchaseOrderRoutingModule } from './purchase-order-routing.module';
import { ProcessPurchaseOrderComponent } from './process-purchase-order/process-purchase-order.component';
import { BuyersComponent } from './process-purchase-order/buyers/buyers.component';
import { BuyerComponent } from './process-purchase-order/buyers/buyer/buyer.component';
import { ShippingInvoiceComponent } from './shipping-invoice/shipping-invoice.component';

@NgModule({
  declarations: [ProcessPurchaseOrderComponent, BuyersComponent, BuyerComponent, ShippingInvoiceComponent],
  imports: [
    CommonModule,
    MatToolbarModule,
    MatIconModule,
    MatCardModule,
    MatButtonModule,
    MatProgressBarModule,
    FlexLayoutModule,
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    ReactiveFormsModule,
    PurchaseOrderRoutingModule
  ]
})
export class PurchaseOrderModule { }
