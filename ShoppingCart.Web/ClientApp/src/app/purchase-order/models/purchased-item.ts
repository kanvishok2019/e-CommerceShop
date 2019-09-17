import { CatalogItemOrdered } from './catalog-item-ordered';

export interface PurchasedItem {
  itemOrdered: CatalogItemOrdered;
  unitPrice: number;
  units: number;
}
