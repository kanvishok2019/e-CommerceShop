import { PurchasedItem } from './purchased-item';
import { Address } from './address';

export interface ShippingInvoice {
  purchaseOrderNo: number;
  buyerId: number;
  purchasedItems: Array<PurchasedItem>;
  address: Address
  total: number;
}
