
import React, { useEffect, useState } from 'react';
import { httpGet } from '../services/api';
import { Account } from './../types/accounts';
import AccountDetails from './accountDetails';

const Accounts = () => {
    let [accounts, setAccounts] = useState<Account[]>([]);
    let [error, setError] = useState<string>("");

    useEffect(() => {
        httpGet<Account[]>("accountsApi").then(accounts => {
            setAccounts(accounts);
        }).catch((e: Error) => {
            debugger;
            setError(e.message);
        });
    })

    const deleteAccount = (id: string) => {
        setAccounts(accounts.filter(account => account.id != id));
    }

    return (
        <>
            {error && error}
            {accounts.map(account =>
                <AccountDetails data={account} delete={deleteAccount} />
            )}
        </>
    );
}

export default Accounts;