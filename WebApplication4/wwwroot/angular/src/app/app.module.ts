import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { AccountsComponent } from 'src/accounts/accounts.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppMaterialModule } from './app-material.module';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AccountCardComponent } from 'src/accounts/card/account-card.component';
import { MainComponent } from './main/main.component';
import { CreateAccountComponent } from './create-account/create-account.component';

@NgModule({
  declarations: [
    AppComponent,
    AccountsComponent,
    AccountCardComponent,
    MainComponent,
    CreateAccountComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AppMaterialModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
