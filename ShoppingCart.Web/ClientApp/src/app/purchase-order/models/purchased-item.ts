import { CatalogItemOrdered } from './catalog-item-ordered';

export interface PurchasedItem {
  catalogItemOrdered: CatalogItemOrdered;
  unitPrice: number;
  units: number;
}
