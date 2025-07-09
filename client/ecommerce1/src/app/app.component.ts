import { Component, OnInit } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { authConfig } from 'src/configurations/auth.config';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'ecommerce';
    constructor(private oauthService: OAuthService) {}
   ngOnInit(): void {
    this.oauthService.configure(authConfig);
    this.oauthService.loadDiscoveryDocumentAndTryLogin().then(() => {
      debugger;
    if (this.oauthService.hasValidAccessToken()) {
      const claims: any = this.oauthService.getIdentityClaims();
      console.log("User logged in. Claims:", claims);
      console.log("User name:", claims?.name);
    } else {
      console.log("No valid access token found.");
    }
  });
  }
 login() { 
  console.log("Login initiated");
  this.oauthService.initCodeFlow(); }
  logout() { this.oauthService.logOut(); }
  get token() { return this.oauthService.getAccessToken(); }

  get name(): string | null {

    const claims: any = this.oauthService.getIdentityClaims();
    return claims ? claims["name"] : null;
  }
}
