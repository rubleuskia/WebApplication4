export interface AccountDto {
    id: string;
    amount: number;
    currencyName: string;
}

export interface CreateAccountDto {
    amount: number;
    currency: string;
}