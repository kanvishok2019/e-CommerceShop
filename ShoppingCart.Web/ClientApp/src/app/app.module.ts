import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http'; 
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ApiService } from '../services/api.service';
import { TestDataService } from './purchase-order/test-data-service';
import { PurchaseOrderService } from './purchase-order/purchase-order.service';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule ,
    AppRoutingModule,
    BrowserAnimationsModule,
  ],
  providers: [
    ApiService,
    TestDataService,
    PurchaseOrderService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
