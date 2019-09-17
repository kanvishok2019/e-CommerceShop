import { Injectable } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { environment } from '../../environments/environment';

@Injectable()
export class PurchaseOrderService {

  constructor(private _apiService: ApiService) {

  }

  processPurchaseOrder(purchaseOrderNumber: number) {
    var url = environment.purchaseOrderApi.process;
    return this._apiService.add(url + purchaseOrderNumber + "/process", "");
  }
}
