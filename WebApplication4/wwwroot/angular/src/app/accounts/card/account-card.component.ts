import { Component, Input } from '@angular/core';
import { AccountDto } from '../types/account';

@Component({
  selector: 'account-card',
  templateUrl: './account-card.component.html',
  styleUrls: ['./account-card.component.scss']
})
export class AccountCardComponent {
  @Input() account: AccountDto;

}
