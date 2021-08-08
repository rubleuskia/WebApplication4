import React, { useState } from 'react';
import { Currency, Account } from './../types/accounts';
import AccountDetails from './accountDetails';

const Accounts = (props: { accounts: Account[] }) => {
    let [accounts, setAccounts] = useState(props.accounts);

    const createNew = () => {
        setAccounts([...accounts, { amount: 100, currency: Currency.BYN, name: "New account " + (accounts.length + 1) }]);
    };

    const deleteAccount = (name: string) => {
        setAccounts(accounts.filter(account => account.name != name));
    }

    return (
        <>
            <button onClick={createNew}>Create new!</button>
            {accounts.map(account =>
                <AccountDetails data={account} delete={deleteAccount} />
            )}
        </>
    );
}

export default Accounts;