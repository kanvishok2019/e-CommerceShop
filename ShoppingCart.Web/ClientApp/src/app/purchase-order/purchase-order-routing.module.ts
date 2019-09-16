import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProcessPurchaseOrderComponent } from './process-purchase-order/process-purchase-order.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'process',
    pathMatch: 'full'
  },
  {
    path: 'process',
    component: ProcessPurchaseOrderComponent,
    pathMatch: 'full'
  }
  
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PurchaseOrderRoutingModule { }
