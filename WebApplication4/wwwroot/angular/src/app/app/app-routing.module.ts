import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateAccountComponent } from '../accounts/create-account/create-account.component';
import { AccountsComponent } from '../accounts/page/accounts.component';
import { MainComponent } from '../main/main.component';

const routes: Routes = [
  { path: '', redirectTo: '/angular', pathMatch: 'full' },
  { path: 'angular', component: MainComponent },
  { path: 'angular/accounts', component: AccountsComponent },
  { path: 'angular/accounts/create', component: CreateAccountComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
