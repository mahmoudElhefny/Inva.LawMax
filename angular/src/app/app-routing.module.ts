import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LawyersComponent } from './Components/Lawyer/lawyers/lawyers.component';
import { CaseListComponent } from './Components/Cases/case-list/case-list/case-list.component';
import { HearingsComponent } from './Components/Hearinges/hearings/hearings.component';
import { MAT_DATE_LOCALE } from '@angular/material/core';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    loadChildren: () => import('./home/home.module').then(m => m.HomeModule),
  },
  {
    path: 'account',
    loadChildren: () => import('@abp/ng.account').then(m => m.AccountModule.forLazy()),
  },
  {
    path: 'identity',
    loadChildren: () => import('@abp/ng.identity').then(m => m.IdentityModule.forLazy()),
  },
  {
    path: 'tenant-management',
    loadChildren: () =>
      import('@abp/ng.tenant-management').then(m => m.TenantManagementModule.forLazy()),
  },
  {
    path: 'setting-management',
    loadChildren: () =>
      import('@abp/ng.setting-management').then(m => m.SettingManagementModule.forLazy()),
  },
  { path: 'lawyers', component: LawyersComponent },
  { path: 'cases', component: CaseListComponent },
  { path: 'hearings', component: HearingsComponent },
  { path: '', redirectTo: '/cases', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {})],
  exports: [RouterModule],
  providers: [
    { provide: MAT_DATE_LOCALE, useValue: 'en-GB' } // or any other locale you prefer
  ]
})
export class AppRoutingModule {}
