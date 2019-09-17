export interface BasketItems {
  catalogItemId: number;
  catalogType: string;
}

export interface Basket {
  basketId: string;
  buyerId: number;
  basketItems: Array<BasketItems>;
}
