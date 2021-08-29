import { Component, Input } from '@angular/core';
import { AccountDto } from 'src/types/account';

@Component({
  selector: 'account-card',
  templateUrl: './account-card.component.html',
  styleUrls: ['./account-card.component.scss']
})
export class AccountCardComponent {
  @Input() account: AccountDto;

  public handleAcquire(id: string) {
    console.log("Acquire: " + id);
  }

  public handleWithdraw(id: string) {
    console.log("Withdraw: " + id);
  }
}