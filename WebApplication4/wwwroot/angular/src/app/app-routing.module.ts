import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountsComponent } from 'src/accounts/accounts.component';
import { CreateAccountComponent } from './create-account/create-account.component';
import { MainComponent } from './main/main.component';

const routes: Routes = [
  { path: '', redirectTo: '/angular', pathMatch: 'full' },
  { path: 'angular', component: MainComponent },
  { path: 'angular/accounts', component: AccountsComponent },
  { path: 'angular/accounts/new', component: CreateAccountComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
