export enum Currency {
    USD = 1,
    BYN = 2,
    EUR = 3,
}

export interface Account {
    name: string;
    amount: number;
    currency: Currency,
}