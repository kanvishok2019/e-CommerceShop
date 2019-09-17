import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-buyer',
  templateUrl: './buyer.component.html',
  styleUrls: ['./buyer.component.scss']
})
export class BuyerComponent implements OnInit {

  @Input() buyer: Buyer;
  constructor() { }

  ngOnInit() {
  }

}
