export class BasketItemPostModel {
  basketId: string;
  catalogItemId: number;
  quantity: number;
  price: number;
  constructor(basketId: string, catalogItemId: number, quantity: number, price: number) {
    this.basketId = basketId;
    this.catalogItemId = catalogItemId;
    this.quantity = quantity;
    this.price = price;
  }
}
