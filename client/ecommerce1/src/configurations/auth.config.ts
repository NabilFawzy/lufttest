import { AuthConfig } from 'angular-oauth2-oidc';
export const authConfig: AuthConfig = {
  issuer: 'http://localhost:5003',
  redirectUri: window.location.origin + '/callback',
  clientId: 'ecommerce',
  responseType: 'code',
  scope: 'openid profile api1',
  showDebugInformation: true,
  strictDiscoveryDocumentValidation: false,
  requireHttps: false, // allow http for dev
};
