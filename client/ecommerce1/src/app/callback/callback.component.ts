import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { OAuthService } from 'angular-oauth2-oidc';

@Component({
  selector: 'app-callback',
  templateUrl: './callback.component.html',
  styleUrls: ['./callback.component.css']
})
export class CallbackComponent {
  constructor(private oauthService: OAuthService, private router: Router) {}

  ngOnInit(): void {
    this.oauthService.loadDiscoveryDocumentAndTryLogin().then(() => {
      if (this.oauthService.hasValidAccessToken()) {
        console.log("✅ Login successful");
        this.router.navigate(['/']);
      } else {
        console.warn("❌ Login failed or token missing");
      }
    });
  }
}
