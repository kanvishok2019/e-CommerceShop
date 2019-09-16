import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProcessPurchaseOrderComponent } from './process-purchase-order.component';

describe('ProcessPurchaseOrderComponent', () => {
  let component: ProcessPurchaseOrderComponent;
  let fixture: ComponentFixture<ProcessPurchaseOrderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProcessPurchaseOrderComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProcessPurchaseOrderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
