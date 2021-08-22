import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AccountsService } from '../account-service';
import { AccountDto } from '../types/account';

@Component({
  selector: 'accounts',
  templateUrl: './accounts.component.html',
  styleUrls: ['./accounts.component.scss'],
  providers: [AccountsService]
})
export class AccountsComponent {
  public accounts: AccountDto[] = [];

  constructor(
    private service: AccountsService,
    private router: Router
  ) { }

  ngOnInit() {
    this.service.getAccounts().subscribe(accounts => {
      this.accounts = accounts;
    });
  }

  public createAccount() {
    this.router.navigateByUrl("/angular/accounts/create")
  }
}
