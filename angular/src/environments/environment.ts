import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'LawMax',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44330/',
    redirectUri: baseUrl,
    clientId: 'LawMax_App',
    responseType: 'code',
    scope: 'offline_access LawMax',
    requireHttps: true,
  },
  apis: {
    default: {
      url: 'https://localhost:44330',
      rootNamespace: 'Inva.LawMax',
    },
  },
} as Environment;
