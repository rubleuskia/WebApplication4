import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AccountDto } from 'src/types/account';
import { AccountsService } from './accounts.service';

@Component({
  selector: 'accounts-component',
  templateUrl: './accounts.component.html',
  styleUrls: ['./accounts.component.scss']
})
export class AccountsComponent {
  public accounts: AccountDto[] = [];

  constructor(private service: AccountsService, private router: Router) { }

  ngOnInit() {
    this.service.getAccounts().subscribe(data => {
      this.accounts = data;
    })
  }

  public createAccount() {
    this.router.navigateByUrl("/angular/accounts/new")
  }
}
