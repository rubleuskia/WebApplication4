import { useEffect, useState } from 'react';
import { httpGet, httpPost } from '../../services/api';
import { Account } from '../../types/accounts';
import Header from './header';
import Details from './details';
import Create from './create';
import { Alert } from 'react-bootstrap';

const Accounts = () => {
    const [accounts, setAccounts] = useState<Account[]>([]);
    const [error, setError] = useState<string>("");
    const [showCreate, setShowCreate] = useState<boolean>(false);

    useEffect(() => {
        loadAccounts();
    }, [])

    const loadAccounts = async () => {
        try {
            const accounts = await httpGet<Account[]>("accountsApi");
            setAccounts(accounts)
        } catch (error) {
            setError(error.message);
        }
    };

    const createAccount = async (amount: number, currencyCharCode: string) => {
        try {
            await httpPost("accountsApi", { body: { amount, currencyCharCode } });
            await loadAccounts();
            setShowCreate(false);
            setError("");
        } catch (error) {
            setError(error.message);
        }
    };

    const deleteAccount = (id: string) => {
        setAccounts(accounts.filter(account => account.id !== id));
    }

    return (
        <>
            {error &&
                <Alert variant='danger'>
                    {error}
                </Alert>}
            <Header create={() => setShowCreate(true)} />
            {showCreate && <Create onSave={createAccount} />}

            {!showCreate &&
                <div className="row row-cols-1 row-cols-sm-2 row-cols-md-3 g-3 p-0 m-0">
                    {accounts.map((account) => <Details key={account.id} account={account} remove={deleteAccount} />)}
                </div>}
        </>
    );
}

export default Accounts;