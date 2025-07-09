import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';

@Injectable({ providedIn: 'root' })
export class ProductService {
  private baseUrl = 'http://localhost:5000/api/product';

  constructor(private http: HttpClient, private oauthService: OAuthService) {}

  private get headers(): HttpHeaders {
    return new HttpHeaders({
      Authorization: 'Bearer ' + this.oauthService.getAccessToken()
    });
  }

  getProducts() {
    return this.http.get<{ id: number, name: string }[]>(this.baseUrl, { headers: this.headers });
  }

  addProduct(name: string) {
  return this.http.post<{ id: number, name: string }>(
    this.baseUrl,
    JSON.stringify(name),
    {
      headers: this.headers.set('Content-Type', 'application/json')
    }
  );
}

  deleteProduct(id: number) {
    return this.http.delete(this.baseUrl + '/' + id, { headers: this.headers });
  }
}
