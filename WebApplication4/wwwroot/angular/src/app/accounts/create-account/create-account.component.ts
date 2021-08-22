import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AccountsService } from '../account-service';
import { AccountDto, CreateAccountDto } from '../types/account';

@Component({
  selector: 'create-account',
  templateUrl: './create-account.component.html',
  styleUrls: ['./create-account.component.scss']
})
export class CreateAccountComponent {
  public currencies = [
    { value: "USD", text: "US dollar" },
    { value: "BYN", text: "Belarussian ruble" },
    { value: "RUB", text: "Russian ruble" },
  ];

  public account: CreateAccountDto = { amount: 0, currency: "" };

  constructor(
    private service: AccountsService,
    private router: Router,
  ) {
  }

  onSubmit() {
    this.service.create(this.account).subscribe(() => {
      this.router.navigateByUrl("/angular/accounts")
    });
  }

  newAccount() {
    this.account = { amount: 0, currency: "" };
  }
}
