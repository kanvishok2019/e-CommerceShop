import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable()
export class ApiService {

  constructor(private _httpClient: HttpClient) {

  }

  get<T>(url: string): Observable<T> {
    return this._httpClient.get<T>(url);
  }

  public add<T>(url: string, itemName: any): Observable<T> {
    return this._httpClient.post<T>(url, itemName);
  }
  public update<T>(url: string, id: string, itemName: any): Observable<T> {
    return this._httpClient.put<T>(url + id, itemName);
  }
}
