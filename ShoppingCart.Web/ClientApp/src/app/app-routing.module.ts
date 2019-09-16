import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'purchase-orders',
    pathMatch: 'full'
  }, {
    path: 'purchase-orders',
    loadChildren: './purchase-order/purchase-order.module#PurchaseOrderModule'
  },];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
