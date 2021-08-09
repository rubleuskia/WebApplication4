export enum Currency {
    USD = 1,
    BYN = 2,
    EUR = 3,
}

export interface Account {
    id: string;
    amount: number;
    currencyCharCode: string,
    userId: string,
    rowVersion: string,
    createdAt: string,
    updatedAt: string,
}