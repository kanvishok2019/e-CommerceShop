import { Injectable } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { environment } from '../../environments/environment';
import { Basket } from './models/basket';
 
@Injectable()
export class TestDataService {

  constructor(private _apiService: ApiService) {

  }

  createTestData() {
    var url = environment.TestDataApi.create;
    return this._apiService.add(url, null);
  }

  getAllBuyers() {
    var url = environment.buyerApi.buyers;
    return this._apiService.get<Array<Buyer>>(url);
  }

  //createBasket(buyerId: number) {
  //  var url = environment.basketApi.basket;
  //  return this._apiService.add(url, { 'buyerId': buyerId });
  //}

  getBasket(buyerId: number) {
    var url = environment.basketApi.basket;
    return this._apiService.get<Basket>(url + buyerId);
  }

  //addItemToBasket(basketItemModel: BasketItemPostModel) {
  //  var url = environment.basketApi.basket;
  //  return this._apiService.update(url, basketItemModel.basketId, basketItemModel);
  //}

  //createPurchaseOrder(basketId: string) {
  //  var url = environment.purchaseOrderApi.create;
  //  return this._apiService.add(url, { 'basketId': basketId });
  //}

  
}
