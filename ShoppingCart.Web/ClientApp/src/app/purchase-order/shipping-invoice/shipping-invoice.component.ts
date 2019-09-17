import { Component, OnInit, Input } from '@angular/core';
import { ShippingInvoice } from '../models/shipping-invoice';

@Component({
  selector: 'app-shipping-invoice',
  templateUrl: './shipping-invoice.component.html',
  styleUrls: ['./shipping-invoice.component.scss']
})
export class ShippingInvoiceComponent implements OnInit {

  @Input() shippingInvoice: ShippingInvoice;

  constructor() { }

  ngOnInit() {
  }

}
