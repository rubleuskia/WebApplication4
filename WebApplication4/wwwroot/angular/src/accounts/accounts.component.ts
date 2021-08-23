import { Component } from '@angular/core';
import { AccountDto } from 'src/types/account';
import { AccountsService } from './accounts.service';

@Component({
  selector: 'accounts-component',
  templateUrl: './accounts.component.html',
  styleUrls: ['./accounts.component.scss']
})
export class AccountsComponent {
  public accounts: AccountDto[] = [];

  constructor(private service: AccountsService) { }

  ngOnInit() {
    this.service.getAccounts().subscribe(data => {
      this.accounts = data;
    })
  }
}
