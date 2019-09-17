import { Component, OnInit } from '@angular/core';
import { TestDataService } from '../test-data-service';
import { Basket } from '../models/basket';
 import { forEach } from '@angular/router/src/utils/collection';
import { promise } from 'protractor';
import { PurchaseOrderService } from '../purchase-order.service';
import { ShippingInvoice } from '../models/shipping-invoice';

@Component({
  selector: 'app-process-purchase-order',
  templateUrl: './process-purchase-order.component.html',
  styleUrls: ['./process-purchase-order.component.scss']
})
export class ProcessPurchaseOrderComponent implements OnInit {

  constructor(private _testDataService: TestDataService,
    private _purchaseOrderService: PurchaseOrderService) { }

  shippingInvoice: ShippingInvoice;
  buyers: Array<Buyer>;
  isLoading: boolean;
  ngOnInit() {

  }

  async buildTestData() {
    this.isLoading = true;
    await this.getBuyersData();
    await this._testDataService.createTestData().toPromise();
    this.isLoading = false;
  }
   
  async getBuyersData() {
    this.buyers = await this._testDataService.getAllBuyers().toPromise();
  }

  async processPurchaseOrder(purchaseOrderNumber: number) {
    await this._purchaseOrderService.processPurchaseOrder(purchaseOrderNumber).subscribe(result => {
      this.shippingInvoice = result as ShippingInvoice;
      console.log(this.shippingInvoice);
    });
    
    await this.getBuyersData();
  }



  
}



