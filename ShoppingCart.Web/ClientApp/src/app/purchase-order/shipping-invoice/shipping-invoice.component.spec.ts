import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ShippingInvoiceComponent } from './shipping-invoice.component';

describe('ShippingInvoiceComponent', () => {
  let component: ShippingInvoiceComponent;
  let fixture: ComponentFixture<ShippingInvoiceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ShippingInvoiceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ShippingInvoiceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
