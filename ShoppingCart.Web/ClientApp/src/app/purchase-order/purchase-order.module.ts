import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { FlexLayoutModule } from '@angular/flex-layout';

import { PurchaseOrderRoutingModule } from './purchase-order-routing.module';
import { ProcessPurchaseOrderComponent } from './process-purchase-order/process-purchase-order.component';

@NgModule({
  declarations: [ProcessPurchaseOrderComponent],
  imports: [
    CommonModule,
    MatToolbarModule,
    MatIconModule,
    MatCardModule,
    MatButtonModule,
    MatProgressBarModule,
    FlexLayoutModule,
    PurchaseOrderRoutingModule
  ]
})
export class PurchaseOrderModule { }
