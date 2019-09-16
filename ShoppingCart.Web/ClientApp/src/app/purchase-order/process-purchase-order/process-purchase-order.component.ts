import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-process-purchase-order',
  templateUrl: './process-purchase-order.component.html',
  styleUrls: ['./process-purchase-order.component.scss']
})
export class ProcessPurchaseOrderComponent implements OnInit {

  constructor() { }

  ngOnInit() {
    this.buildTestData();
  }


  /**
   * This method to build test data
   * */
  buildTestData() {

  }
}
