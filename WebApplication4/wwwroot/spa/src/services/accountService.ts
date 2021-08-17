import { AccountDto } from "../types/accounts";
import { httpGet, httpPost } from "./requestApi";

// export const accountsData: AccountDto[] = [
//     { id: "1", amount: 100, currencyName: "US dollar" },
//     { id: "2", amount: 200, currencyName: "Euro" },
//     { id: "3", amount: 300, currencyName: "Russian Ruble" },
//     { id: "4", amount: 400, currencyName: "US dollar" },
//     { id: "5", amount: 500, currencyName: "US dollar" },
// ];

export const fetchAccounts = () => {
    return httpGet<AccountDto[]>("accountsApi");
}

export const createAccount = (amount: number, currency: string) => {
    return httpPost("accountsApi/create", {
        body: { amount, currency },
    });
}