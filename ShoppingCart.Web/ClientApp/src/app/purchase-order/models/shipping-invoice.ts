import { PurchasedItem } from './purchased-item';

export interface ShippingInvoice {
  purchaseOrderNo: number;
  buyerId: number;
  purchasedItems: Array<PurchasedItem>;
 }
