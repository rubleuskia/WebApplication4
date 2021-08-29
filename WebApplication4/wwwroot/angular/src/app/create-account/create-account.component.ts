import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountsService } from 'src/accounts/accounts.service';
import { CreateAccountDto } from 'src/types/account';

@Component({
  selector: 'app-create-account',
  templateUrl: './create-account.component.html',
  styleUrls: ['./create-account.component.scss']
})
export class CreateAccountComponent {
  private _account: CreateAccountDto = { amount: 10, currency: "" };

  public currencies = [
    { text: "US Dollar", value: "USD" },
    { text: "Euro", value: "EUR" },
    { text: "Russian ruble", value: "RUB" },
  ];

  constructor(private service: AccountsService, private router: Router) {
  }

  onSubmit() {
    this.service.create(this._account).toPromise()
      .then(() => {
        this.router.navigateByUrl("/angular/accounts");
      });
  }

  public get currency(): string {
    console.log("get currency");
    return this._account.currency;
  }

  public set currency(value: string) {
    this._account.currency = value;
  }

  public get amount(): number {
    console.log("get amount");
    return this._account.amount;
  }

  public set amount(value: number) {
    this._account.amount = value;
  }
}
