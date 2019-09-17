import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-buyers',
  templateUrl: './buyers.component.html',
  styleUrls: ['./buyers.component.scss']
})
export class BuyersComponent implements OnInit {

  @Input() buyers: Array<Buyer>;
  
  constructor() { }

  ngOnInit() {
  }

}
