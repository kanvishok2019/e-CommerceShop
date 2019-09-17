import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

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

  purchaseOrderForm: FormGroup;
  shippingInvoice: ShippingInvoice;
  buyers: Array<Buyer>;
  isLoading: boolean;
  isTestDataAvailable: boolean;

  ngOnInit() {
    this.purchaseOrderForm = new FormGroup({
      purchaseOrderNo: new FormControl("", [Validators.required])
    });
    this.getBasketData();
  }


  async buildTestData() {
    this.isLoading = true;
    await this.getBuyersData();
    await this._testDataService.createTestData().toPromise();
    this.isLoading = false;
    this.isTestDataAvailable = true;
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.purchaseOrderForm.controls[controlName].hasError(errorName);
  }

  async getBasketData() {
    const basketData = await this._testDataService.getBasket(1).toPromise();
    if (basketData != null) {
      this.isTestDataAvailable = true;
      this.getBuyersData();
    }
  }

  async getBuyersData() {
    this.buyers = await this._testDataService.getAllBuyers().toPromise();
  }

  async processPurchaseOrder() {
    this.isLoading = true;
    await this._purchaseOrderService.processPurchaseOrder(this.purchaseOrderForm.controls['purchaseOrderNo'].value).subscribe(async (result) => {
      this.shippingInvoice = result as ShippingInvoice;
      console.log(this.shippingInvoice);
      await this.getBuyersData();
      this.isLoading = false;
    });
  }

}



