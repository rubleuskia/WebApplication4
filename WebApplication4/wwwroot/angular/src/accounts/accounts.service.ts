import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AccountDto, CreateAccountDto } from 'src/types/account';

@Injectable({ providedIn: 'root' })
export class AccountsService {
    private httpClient: HttpClient;

    constructor(private http: HttpClient) {
        this.httpClient = http;
    }

    public getAccounts(): Observable<AccountDto[]> {
        return this.httpClient.get<AccountDto[]>("/api/accountsApi");
    }

    public create(account: CreateAccountDto) {
        return this.httpClient.post("/api/accountsApi/create", account);
    }
}